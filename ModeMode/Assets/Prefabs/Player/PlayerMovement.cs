using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] Stats stats;
    [SerializeField] Rigidbody body;
    [SerializeField] Animator animator;
    [SerializeField] SpriteRenderer bodySprite;
    Vector2 direction = new Vector2(0,0);
    public bool isMoving;
    float startSpeed;
    bool canRoll = true;
    bool canMove = true;
    private void Start()
    {
        startSpeed = stats.movingStats.maxSpeed;
    }

    void Update()
    {

        Movement();
        AnimatePlayer();
        RollInput();
    }

    void RollInput()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (canRoll && isMoving)
            {
                StartCoroutine(Roll());
            }

        }
    }

    IEnumerator Roll()
    {
        canRoll = false;
        canMove = false;
        animator.speed = 1 / stats.dashingStats.TimeToComplete;
        if (direction.x < 0)
        {
            animator.SetTrigger("RollLeft");

        }
        if (direction.x >= 0)
        {
            animator.SetTrigger("RollRight");
        }
        Vector2 fakeDirection = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        float elapsed = 0;
        Vector3 startPos = transform.position;
        while (elapsed < stats.dashingStats.TimeToComplete)
        {
            elapsed += Time.deltaTime;
            transform.position = Vector3.Lerp(startPos, startPos + (stats.dashingStats.Distance *  new Vector3(fakeDirection.x, 0, fakeDirection.y).normalized), elapsed/ stats.dashingStats.TimeToComplete);
            yield return null;
        }
        transform.position = startPos + (stats.dashingStats.Distance * new Vector3(fakeDirection.x, 0, fakeDirection.y).normalized);
        canMove = true;
        yield return new WaitForSeconds(stats.dashingStats.dashCooldown);
        canRoll = true;

    }

    void AnimatePlayer()
    {
        isMoving = direction.magnitude > 0.1f;
        animator.SetBool("isMoving", isMoving);
        if (canMove)
        {
            animator.speed = stats.movingStats.maxSpeed / startSpeed;
        }
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
        if (Input.GetAxisRaw("Vertical") != 0)
        {
            direction.y = Mathf.MoveTowards(direction.y, Input.GetAxisRaw("Vertical"), stats.movingStats.acceleration * Time.deltaTime);
        }
        else
        {
            direction.y = Mathf.MoveTowards(direction.y, 0, stats.movingStats.decceleration * Time.deltaTime);
        }
        if (Input.GetAxisRaw("Horizontal") != 0)
        {
            direction.x = Mathf.MoveTowards(direction.x, Input.GetAxisRaw("Horizontal"), stats.movingStats.acceleration * Time.deltaTime);
        }
        else
        {
            direction.x = Mathf.MoveTowards(direction.x, 0, stats.movingStats.decceleration * Time.deltaTime);
        }

        if (canMove)
        {
            if (direction.magnitude >= 1)
            {
                body.velocity = new Vector3(direction.normalized.x * stats.movingStats.maxSpeed, body.velocity.y, direction.normalized.y * stats.movingStats.maxSpeed);
            }
            else
            {
                body.velocity = new Vector3(direction.x * stats.movingStats.maxSpeed, body.velocity.y, direction.y * stats.movingStats.maxSpeed);
            }
        }
    }
}
