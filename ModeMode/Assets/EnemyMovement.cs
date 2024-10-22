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
    }

    // Update is called once per frame
    void Update()
    {
        agent.SetDestination(player.transform.position + new Vector3(statsRef.movingStats.targetOffset.x, 0, statsRef.movingStats.targetOffset.y));
        SetAgentSettings();
        Animation();
    }

    void Animation()
    {
        animator.SetBool("isMoving", agent.velocity.magnitude > 0.1);
    }

    void SetAgentSettings()
    {
        agent.speed = statsRef.movingStats.maxSpeed;
        agent.acceleration = statsRef.movingStats.acceleration;
        agent.stoppingDistance = statsRef.movingStats.stopDistance;
    }
}
