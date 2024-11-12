using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyShooting : MonoBehaviour
{
    GameObject player;
    [SerializeField] Stats statsRef;
    [SerializeField] GameObject projectileSpawn;
    [SerializeField] Animator animator;
    [SerializeField] NavMeshAgent agent;

    float burstTimer = 0;
    float attackSpeedTimer = 0;
    int projectilesBursted =0;
    float rotationZ;
    bool hasStopAttackAnimation = false;
    void Awake()
    {
        player = GameObject.FindWithTag("Player");
    }

    
    void Update()
    {
        if (statsRef.doesShoot)
        {
            switch (statsRef.shootingStats.type)
            {
                case Stats.ShootingStats.ShootType.normal:
                    Shooting();
                    break;
                case Stats.ShootingStats.ShootType.shootWhileStopped:
                    if (!statsRef.movingStats.isMoving)
                    {
                        Shooting();
                    }
                    else
                    {
                        projectilesBursted = 0;
                    }
                    break;
                case Stats.ShootingStats.ShootType.shootWhileMoving:
                    if (statsRef.movingStats.isMoving)
                    {
                        Shooting();
                    }
                    else
                    {
                        projectilesBursted = 0;
                    }
                    break;
            }
        }

    }

    void StopWhileShooting()
    {
        if (attackSpeedTimer > 1 / statsRef.shootingStats.attackSpeed - statsRef.shootingStats.timeBeforeStopShooting)
        {
            statsRef.movingStats.canMove = false;
            if (statsRef.shootingStats.animationTrigger != "" && !hasStopAttackAnimation)
            {
                animator.SetTrigger(statsRef.shootingStats.animationTrigger);
                hasStopAttackAnimation = true;
            }
        }
        else
        {
            statsRef.movingStats.canMove = true;
            hasStopAttackAnimation = false;

        }
    }

    void Shooting()
    {

        burstTimer += Time.deltaTime;

        if (statsRef.shootingStats.stopWhileShooting)
        {
            StopWhileShooting();
        }

        if (attackSpeedTimer > 1 / statsRef.shootingStats.attackSpeed)
        {
            if (projectilesBursted < statsRef.shootingStats.burstAmount)
            {
                if (burstTimer > 1 / statsRef.shootingStats.burstSpeed)
                {
                    burstTimer = 0;
                    projectilesBursted++;

                    if (statsRef.shootingStats.animationTrigger != "" && !statsRef.shootingStats.stopWhileShooting)
                    {
                        animator.SetTrigger(statsRef.shootingStats.animationTrigger);
                    }
                    for (int i = 0; i < statsRef.shootingStats.bulletsPerShot; i++)
                    {
                        SpawnBullet(i);
                    }
                }
                else
                {
                    burstTimer += Time.deltaTime;
                }
            }
            else
            {
                attackSpeedTimer = 0;
                projectilesBursted = 0;
            }
        }
        else
        {
            attackSpeedTimer += Time.deltaTime;
        }
    }

    void SpawnBullet(int shotNum)
    {
        Debug.Log("SpawnCapsule");
        Vector3 spawnPos = new Vector3(statsRef.shootingStats.projectileSpawn[shotNum].transform.position.x, 0, statsRef.shootingStats.projectileSpawn[shotNum].transform.position.z);

        if (statsRef.shootingStats.fixedBurst )
        {
            if (projectilesBursted == 1)
            {
                rotationZ = Mathf.Atan2(player.transform.position.z - transform.position.z, player.transform.position.x - transform.position.x) * Mathf.Rad2Deg;
            }
        }
        else
        {
            rotationZ = Mathf.Atan2(player.transform.position.z - transform.position.z, player.transform.position.x - transform.position.x) * Mathf.Rad2Deg;
        }

        if (statsRef.shootingStats.shootParticle != null)
        {
            Instantiate(statsRef.shootingStats.shootParticle, projectileSpawn.transform.position, Quaternion.identity, transform);
        }

        GameObject newProjectile = Instantiate(statsRef.shootingStats.bulletPrefab, spawnPos, Quaternion.identity);
        Projectile projectileScript = newProjectile.GetComponent<Projectile>();

        Vector3 addedForce = Vector3.zero;
        if (statsRef.shootingStats.doesMotionEffectVelocity)
        {
            addedForce = agent.velocity;
        }

        projectileScript.SetupBullet(statsRef.shootingStats, rotationZ, shotNum, addedForce);

    }
}
