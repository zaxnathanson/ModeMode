using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExperiencePoint : MonoBehaviour
{
    bool collected = false;
    GameObject targetPlayer;
    [SerializeField] float movementSpeedIncrease;
    [SerializeField] float movementSpeed;
    [SerializeField] Rigidbody rb;
    [SerializeField] float collectionDistance;
    [SerializeField] GameObject expGet, expCollected;
    [SerializeField] float upForceMax, upForceMin;
    [SerializeField] float sideForce;
    public float Amount;
    [SerializeField] Sprite large, medium, small;
    [SerializeField] SpriteRenderer spriteRenderer;


    private void Awake()
    {
        targetPlayer = GameObject.FindWithTag("Player");
        rb.AddForce(transform.up * Random.Range(upForceMin, upForceMax), ForceMode.Impulse);
        Vector3 direction = new Vector3(Random.Range(0, 360), 0, Random.Range(0, 360)).normalized;
        Debug.Log(direction);
        rb.AddForce(direction * Random.Range(-sideForce, sideForce), ForceMode.Impulse);
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            StartCoroutine(Collect());
        }
    }

    public void SetAmount(float value)
    {
        Amount = value;
        if (Amount >= 20)
        {
            spriteRenderer.sprite = large;
        }
        else if (Amount >= 5)
        {
            spriteRenderer.sprite = medium;
        }
        else
        {
            spriteRenderer.sprite = small;
        }
        return;
    }

    IEnumerator Collect()
    {
        yield return null;
        Instantiate(expGet, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    private void Update()
    {
        if (Mathf.Abs(Vector3.Distance(transform.position, targetPlayer.transform.position)) <= collectionDistance)
        {
            if (!collected)
            {
                Instantiate(expCollected, new Vector3(transform.position.x, -0.4f, transform.position.z), Quaternion.identity);
            }
            collected = true;

        }

        if (collected == true)
        {
            rb.isKinematic = true;
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(targetPlayer.transform.position.x, 0.4f, targetPlayer.transform.position.z), Time.deltaTime * movementSpeed);
            movementSpeed += Time.deltaTime * movementSpeedIncrease;
        }

    }
}
