using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooting : MonoBehaviour
{
    GameObject player;
    [SerializeField] Stats statsRef;
    [SerializeField] GameObject projectileSpawn;
    float burstTimer = 0;
    float attackSpeedTimer = 0;
    int projectilesBursted =0;
    float rotationZ;

    void Start()
    {
        player = GameObject.FindWithTag("Player");
    }

    
    void Update()
    {
        burstTimer += Time.deltaTime;

        if (attackSpeedTimer > 1 / statsRef.shootingStats.attackSpeed )
        {W
            if (projectilesBursted < statsRef.shootingStats.burstAmount)
            {
                if (burstTimer > 1 / statsRef.shootingStats.burstSpeed)
                {
                    burstTimer = 0;
                    projectilesBursted++;

                    SpawnBullet();
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

    void SpawnBullet()
    {
        Vector3 spawnPos = new Vector3(projectileSpawn.transform.position.x, 0, projectileSpawn.transform.position.z);
        rotationZ = Mathf.Atan2(player.transform.position.z, player.transform.position.x) * Mathf.Rad2Deg;
        projectileSpawn.transform.localRotation = Quaternion.Euler(0, 0, rotationZ);

        if (statsRef.shootingStats.shootParticle != null)
        {
            Instantiate(statsRef.shootingStats.shootParticle, projectileSpawn.transform.position, projectileSpawn.transform.rotation, transform);
        }

        GameObject newProjectile = Instantiate(statsRef.shootingStats.bulletPrefab, spawnPos, Quaternion.identity);
        Projectile projectileScript = newProjectile.GetComponent<Projectile>();
        projectileScript.SetupBullet(statsRef.shootingStats, rotationZ);

    }
}
