using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;

public class Capsule : MonoBehaviour
{
    [HideInInspector] public Stats.PlayerSpecific statsRef;
    [SerializeField] Rigidbody rb;
    Vector3 startPos;
    [SerializeField] GameObject deathParticle;
    [SerializeField] GameObject capsulePickup;

    public void SetupBullet(Stats.PlayerSpecific shootingStats, float rotation)
    {
        statsRef = shootingStats;

        transform.rotation = Quaternion.Euler(0, rotation, 0);

        startPos = transform.position;


        //launches bullet
        rb.AddForce(statsRef.capsuleSpeed * new Vector3(transform.right.x, 0, -transform.right.z), ForceMode.Impulse);
        transform.rotation = Quaternion.Euler(0, 0, 0);

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            other.GetComponent<Stats>().Captured();
            Destroy(other.gameObject);
            StartCoroutine(DestroyCapsule(false));

        }
    }
    private void Update()
    {

        if (Mathf.Abs((startPos - transform.position).magnitude) > statsRef.capsuleRange)
        {
            StartCoroutine(DestroyCapsule(true));

        }
    }

    IEnumerator DestroyCapsule(bool doSpawnPickup)
    {
        yield return null;
        if (deathParticle != null)
        {
            GameObject newParticle = Instantiate(deathParticle, transform.position, Quaternion.identity);
            newParticle.transform.localScale *= transform.localScale.x;
        }
        if (doSpawnPickup)
        {
            Instantiate(capsulePickup, transform.position, Quaternion.identity);
        }
        Destroy(gameObject);
    }
}
