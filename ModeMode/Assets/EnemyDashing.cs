using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using DG.Tweening;
using static WaveManager;

public class EnemyDashing : MonoBehaviour
{
    GameObject player;
    [SerializeField] Stats statsRef;
    [SerializeField] Animator animator;
    [SerializeField] NavMeshAgent agent;
    [SerializeField] Transform sprites;
    float rotationZ;
    float burstTimer = 0;
    float dashSpeedTimer = 0;
    int dashesBursted = 0;
    bool canDash = true;
    public bool isDashing = false;



    void Awake()
    {
        player = GameObject.FindWithTag("Player");
    }

    // CallUpdate is called once per frame
    void Update()
    {
        Dashing();
    }

    void Dashing()
    {
        burstTimer += Time.deltaTime;



        if (dashSpeedTimer > 1 / statsRef.dashingStats.dashCooldown)
        {
            if (dashesBursted < statsRef.dashingStats.burstAmount)
            {
                if (burstTimer > 1 / statsRef.dashingStats.burstSpeed && canDash)
                {
                    burstTimer = 0;
                    dashesBursted++;

                    StartCoroutine(Dash());
                }
                else
                {
                    burstTimer += Time.deltaTime;
                }
            }
            else
            {
                dashSpeedTimer = 0;
                dashesBursted = 0;
            }
        }
        else
        {
            dashSpeedTimer += Time.deltaTime;
        }
    }

    IEnumerator Dash()
    {
        canDash = false;

        agent.enabled = false;
        animator.speed = 1 / statsRef.dashingStats.TimeToComplete;

        rotationZ = Mathf.Atan2(player.transform.position.z - transform.position.z, player.transform.position.x - transform.position.x) * Mathf.Rad2Deg;
        Vector3 targetPosition = (transform.position + statsRef.dashingStats.Distance * (player.transform.position - transform.position).normalized);

        Tween dashTween;
        
        if (statsRef.dashingStats.does360)
        {
            dashTween = sprites.transform.DOLocalRotate(new Vector3(45, 0, rotationZ + statsRef.dashingStats.forwardRotationOffset + 360 ), statsRef.dashingStats.startupTime, RotateMode.FastBeyond360);
        }
        else
        {
            dashTween = sprites.transform.DOLocalRotate(new Vector3(45, 0, rotationZ + statsRef.dashingStats.forwardRotationOffset ), statsRef.dashingStats.startupTime, RotateMode.FastBeyond360);
        }
        yield return dashTween.WaitForCompletion();

        isDashing = true;
        dashTween = transform.DOMove(targetPosition, statsRef.dashingStats.TimeToComplete);
        yield return dashTween.WaitForCompletion();
        isDashing = false;

        dashTween = sprites.transform.DOLocalRotate(new Vector3(45, 0, 0), statsRef.dashingStats.endTime, RotateMode.Fast);
        yield return dashTween.WaitForCompletion();

        agent.enabled = true;
        animator.speed = 1;
        canDash = true;
    }
}
