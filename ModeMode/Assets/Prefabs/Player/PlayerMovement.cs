using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] Stats stats;
    [SerializeField] Rigidbody body;
    [SerializeField] Animator animator;
    [SerializeField] SpriteRenderer bodySprite;
    Vector2 direction = new Vector2(0,0);
    public bool isMoving;
    float startSpeed;

    private void Start()
    {
        startSpeed = stats.movingStats.maxSpeed;
    }

    void Update()
    {
        Movement();
        AnimatePlayer();
    }

    void AnimatePlayer()
    {
        isMoving = direction.magnitude > 0.1f;
        animator.SetBool("isMoving", isMoving);
        animator.speed = stats.movingStats.maxSpeed/startSpeed;
        if (direction.x < 0)
        {
            bodySprite.flipX = true;
        }
        if (direction.x > 0)
        {
            bodySprite.flipX = false;
        }
    }

    void Movement()
    {



        if (Input.GetAxis("Vertical") != 0)
        {
            direction.y = Mathf.MoveTowards(direction.y, Input.GetAxis("Vertical"), stats.movingStats.acceleration * Time.deltaTime);
        }
        else
        {
            direction.y = Mathf.MoveTowards(direction.y, 0, stats.movingStats.decceleration * Time.deltaTime);
        }
        if (Input.GetAxis("Horizontal") != 0)
        {
            direction.x = Mathf.MoveTowards(direction.x, Input.GetAxis("Horizontal"), stats.movingStats.acceleration * Time.deltaTime);
        }
        else
        {
            direction.x = Mathf.MoveTowards(direction.x, 0, stats.movingStats.decceleration * Time.deltaTime);
        }

        if (direction.magnitude >= 1)
        {
            body.velocity = new Vector3(direction.normalized.x * stats.movingStats.maxSpeed, body.velocity.y, direction.normalized.y * stats.movingStats.maxSpeed) ;
        }
        else
        {
            body.velocity = new Vector3(direction.x * stats.movingStats.maxSpeed, body.velocity.y, direction.y * stats.movingStats.maxSpeed);
        }
    }
}
