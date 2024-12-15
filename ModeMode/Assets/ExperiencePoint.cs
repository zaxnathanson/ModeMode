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
    [SerializeField] float lifetime, warningLifetime;
    float lifetimeTimer = 0;
    public float Amount;
    [SerializeField] Sprite large, medium, small;
    [SerializeField] SpriteRenderer spriteRenderer;
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

    public void SetAmount(float value)
    {
        Amount = value;
        if (Amount >= 15)
        {
            spriteRenderer.sprite = large;
        }
        else if (Amount >= 3)
        {
            spriteRenderer.sprite = medium;
        }
        else
        {
            spriteRenderer.sprite = small;
        }
        return;
    }

    void Collect(GameObject player)
    {
        Instantiate(expGet, player.transform.position, Quaternion.identity);
        player.GetComponent<Stats>().GetEXP(Amount);
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
