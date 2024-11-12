using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CapsulePickup : MonoBehaviour
{
    bool collected = false;
    GameObject targetPlayer;
    [SerializeField] float movementSpeedIncrease;
    [SerializeField] float movementSpeed;
    [SerializeField] Rigidbody rb;
    [SerializeField] float collectionDistance;
    [SerializeField] float upForceMax, upForceMin;
    [SerializeField] float sideForce;
    [SerializeField] float lifetime, warningLifetime;
    float lifetimeTimer = 0;
    [SerializeField] Animator animator;


    private void Awake()
    {
        targetPlayer = GameObject.FindWithTag("Player");
        rb.AddForce(transform.up * Random.Range(upForceMin, upForceMax), ForceMode.Impulse);
        Vector3 direction = new Vector3(Random.Range(0, 360), 0, Random.Range(0, 360)).normalized;
        rb.AddForce(direction * Random.Range(-sideForce, sideForce), ForceMode.Impulse);
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Collect(other.gameObject);
        }
    }

    void Collect(GameObject player)
    {
        player.GetComponent<Stats>().playerSpecific.capsules++;
        transform.DOScale(0, 0.1f);
        Destroy(gameObject,0.1f);

    }

    private void Update()
    {
        if (Mathf.Abs(Vector3.Distance(transform.position, targetPlayer.transform.position)) <= collectionDistance)
        {

            collected = true;   

        }

        if (collected == true)
        {
            rb.isKinematic = true;
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(targetPlayer.transform.position.x, 0.4f, targetPlayer.transform.position.z), Time.deltaTime * movementSpeed);
            movementSpeed += Time.deltaTime * movementSpeedIncrease;
        }
        else
        {
            lifetimeTimer += Time.deltaTime;
            if (lifetimeTimer > warningLifetime)
            {
                animator.SetBool("isBlinking", true);
            }
            if (lifetimeTimer > lifetime)
            {
                Destroy(gameObject);
            }
        }


    }
}
