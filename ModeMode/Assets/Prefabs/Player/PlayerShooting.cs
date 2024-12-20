using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public class PlayerShooting : MonoBehaviour
{
    [SerializeField] Stats statsRef;
    [SerializeField] GameObject player;

    [SerializeField] SpriteRenderer weaponSprite;
    float gunStartPosX;
    [SerializeField] GameObject weapon;
    float rotationZ;

    [HideInInspector] public float reloadTimer = 0;
    float attackSpeedTimer = 0;

    public delegate void ShootEvent();
    public ShootEvent playerShoot;

    void Start()
    {
        gunStartPosX = weapon.transform.position.x;
    }

    
    void Update()
    {
        RotateGun();
        Shoot();
    }

    void RotateGun()
    {
        Vector3 screenPosition = Camera.main.ScreenToViewportPoint(new Vector3(Input.mousePosition.x - Screen.width/2, Input.mousePosition.y - Screen.height / 2, 0));
        rotationZ = Mathf.Atan2(screenPosition.y, screenPosition.x) * Mathf.Rad2Deg;
        weapon.transform.localRotation = Quaternion.Euler(0, 0, rotationZ);

        if (screenPosition.x > 0)
        {
            weapon.transform.localScale = new Vector3(1,1,1);
            weapon.transform.localPosition = new Vector3(gunStartPosX, weapon.transform.localPosition.y, weapon.transform.localPosition.z);
        }
        else
        {
            weapon.transform.localScale = new Vector3(1, -1, 1);
            weapon.transform.localPosition = new Vector3(-gunStartPosX, weapon.transform.localPosition.y, weapon.transform.localPosition.z);
        }
    }

    void Shoot()
    {
        attackSpeedTimer += Time.deltaTime;

        if (Input.GetMouseButton(0) && statsRef.shootingStats.ammo > 0)
        {
            if (attackSpeedTimer > 1 / statsRef.shootingStats.attackSpeed)
            {
                reloadTimer = 0;
                attackSpeedTimer = 0;
                statsRef.shootingStats.ammo--;


                playerShoot?.Invoke();
                for (int i = 0; i < statsRef.shootingStats.bulletsPerShot; i++)
                {
                    SpawnBullet(i);
                }
            }

        }
        else if (statsRef.shootingStats.ammo < statsRef.shootingStats.maxAmmo)
        {
            reloadTimer += Time.deltaTime;
        }

        if (reloadTimer >= 1/statsRef.shootingStats.reloadSpeed)
        {
            statsRef.shootingStats.ammo = statsRef.shootingStats.maxAmmo;
            reloadTimer = 0;
        }
    }

    public void SpawnBullet(int shotNum)
    {
        Vector3 spawnPos = new Vector3(statsRef.shootingStats.projectileSpawn[shotNum].transform.position.x, 0, statsRef.shootingStats.projectileSpawn[shotNum].transform.position.z);
        
        if (statsRef.shootingStats.shootParticle != null)
        {
            Instantiate(statsRef.shootingStats.shootParticle, statsRef.shootingStats.projectileSpawn[shotNum].transform.position, statsRef.shootingStats.projectileSpawn[shotNum].transform.rotation, transform);
        }
        
        GameObject newProjectile = Instantiate(statsRef.shootingStats.bulletPrefab, spawnPos, Quaternion.identity);
        Projectile projectileScript = newProjectile.GetComponent<Projectile>();

        projectileScript.SetupBullet(statsRef.shootingStats, rotationZ, shotNum, Vector3.zero);
    }
}
