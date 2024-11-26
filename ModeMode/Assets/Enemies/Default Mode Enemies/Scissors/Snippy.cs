using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snippy : MonoBehaviour
{
    EnemyDashing dashingScript;
    Stats statsRef;
    Vector3 previousPos;
    [SerializeField] float bulletSpawnFrequency;
    void Awake()
    {
        dashingScript = GetComponent<EnemyDashing>();
        statsRef = GetComponent<Stats>();
    }

    // Update is called once per frame
    void Update()
    {
        if (dashingScript.isDashing)
        {
            if (Mathf.Abs((previousPos - transform.position).magnitude) > bulletSpawnFrequency)
            {
                SpawnBullet();
                previousPos = transform.position;
            }
        }
    }

    void SpawnBullet()
    {
        Vector3 spawnPos = new Vector3(transform.position.x, 0, transform.position.z);


        if (statsRef.shootingStats.shootParticle != null)
        {
            Instantiate(statsRef.shootingStats.shootParticle, transform.position, Quaternion.identity, transform);
        }

        GameObject newProjectile = Instantiate(statsRef.shootingStats.bulletPrefab, spawnPos, Quaternion.identity);
        Projectile projectileScript = newProjectile.GetComponent<Projectile>();

        Vector3 addedForce = Vector3.zero;


        projectileScript.SetupBullet(statsRef.shootingStats, 0, 0, addedForce);

    }
}
