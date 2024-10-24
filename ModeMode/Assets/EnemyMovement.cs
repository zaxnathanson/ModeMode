using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] NavMeshAgent agent;
    [SerializeField] Stats statsRef;
    [SerializeField] Animator animator;
    GameObject player;
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        statsRef.movingStats.canMove = true;
    }

    // Update is called once per frame
    void Update()
    {
        SetAgentSettings();
        Animation();


        if (agent.remainingDistance <= agent.stoppingDistance || agent.isStopped)
        {
            agent.acceleration = statsRef.movingStats.decceleration;
            statsRef.movingStats.isMoving = false;
        }
        else
        {
            agent.acceleration = statsRef.movingStats.acceleration;
            statsRef.movingStats.isMoving = true;

        }


    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && statsRef.movingStats.doesCollisionDamage)
        {
            other.GetComponent<Stats>().TakeDamage(statsRef.movingStats.damage, false);
        }
    }

    void Animation()
    {
        animator.SetBool("isMoving", statsRef.movingStats.isMoving);
    }

    void SetAgentSettings()
    {
        if (statsRef.movingStats.canMove)
        {
            agent.isStopped = false;
        }
        else
        {
            agent.isStopped = true;
        }
        agent.SetDestination(player.transform.position + new Vector3(statsRef.movingStats.targetOffset.x, 0, statsRef.movingStats.targetOffset.y));
        agent.speed = statsRef.movingStats.maxSpeed;
        agent.stoppingDistance = statsRef.movingStats.stopDistance;
    }
}
