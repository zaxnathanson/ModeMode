using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;
using Unity.VisualScripting;
using static Stats;
using DamageNumbersPro;

public class Stats : MonoBehaviour
{
    public enum UnitType { enemy, player }
    public UnitType unitType;
    public bool doesMove;
    public bool doesShoot;
    public bool doesDash;
    [HideInInspector] bool isDead = false;
    public GameObject deathParticle;


    //Player Health Stats
    [ShowIf(nameof(unitType), UnitType.enemy)]
    public EnemyHealth enemyHealth;
    [System.Serializable]
    public struct EnemyHealth
    {
        public float maxHealth;
        public float currentHealth;
        public DamageNumber damageNumber;
        public DamageNumber critNumber;
    }

    //Enemy Health Stats
    [ShowIf(nameof(unitType), UnitType.player)]
    public PlayerHealth playerHealth;
    [System.Serializable]
    public struct PlayerHealth
    {
        public int maxHealth;
        public int currentHealth;
    }


    //Movement Stats
    [ShowIf(nameof(doesMove))]
    public MovementStats movingStats;
    [System.Serializable]
    public struct MovementStats
    {
        public float maxSpeed;
        public float acceleration;
        public float decceleration;

        [Header("Enemy Specific")]
        public Vector2 targetOffset;
        public float stopDistance;
    }

    //Shooting Stats
    [ShowIf(nameof(doesShoot))]
    public ShootingStats shootingStats;
    [System.Serializable]
    public struct ShootingStats
    {
        [Header("General")]
        [HorizontalLine(color: EColor.Gray)]
        public GameObject bulletPrefab;
        public enum ProjectileType { enemy, player }
        public ProjectileType projectileType;
        public float damage;
        public float attackSpeed;
        public float range;
        public float shotSpeed;
        public float spread;
        public float size;
        public int pierceAmount;
        public float sinMovementAmount;
        public float sinMovementSpeed;
        public GameObject deathGameObject;

        [Header("Aesthetics")]
        [HorizontalLine(color: EColor.Blue)]
        public GameObject deathParticle;
        public GameObject shootParticle;
        public string animationTrigger;
        public Color insideColor;
        public Color outsideColor;

        [Header("Enemy Specific")]
        [HorizontalLine(color: EColor.Red)]
        public bool shootAtPlayer;
        public int burstAmount;
        public float burstSpeed;
        public int bulletsPerShot;
        public float[] bulletAngles;
        public float homingAmount;
        public enum ShootType { normal, shootWhileMoving, shootWhileStopped }
        public ShootType type;

        [Header("Player Specific")]
        [HorizontalLine(color: EColor.Green)]
        public float reloadSpeed;
        public int ammo;
        public int maxAmmo;
        public float critChance;
        public float critDamage;
        public float multiShot;

    }

    //Dashing Stats
    [ShowIf(nameof(doesDash))]
    public DashingStats dashingStats;
    [System.Serializable]
    public struct DashingStats
    {

        [InfoBox("If dashToPlayer, distance is the offset from the player's position. If dashAtPlayer, then distance is the distance dashed towards the player", EInfoBoxType.Normal)]
        public float Distance;
        public float Damage;
        public float TimeToComplete;
        public bool isInvincible;

        [Header("Enemy Specific")]
        public float spread;
        public enum DashType { dashToPlayer, dashAtPlayer }
        public DashType type;
        public float burstAmount;
        public float burstSpeed;
    }


    private void Update()
    {
        CheckDeath();
        
    }

    public void TakeDamage(float damage, bool isCritical)
    {
        switch (unitType)
        {
            case UnitType.enemy:
                enemyHealth.currentHealth -= damage;
                if (isCritical)
                {
                    enemyHealth.critNumber.Spawn(transform.position, damage);
                }
                else
                {
                    enemyHealth.damageNumber.Spawn(transform.position, damage);
                }
                break;

            case UnitType.player:
                playerHealth.currentHealth -= (int)damage;
                break;
        }
    }

    void CheckDeath()
    {
        switch (unitType)
        {
            case UnitType.enemy:
                if (enemyHealth.currentHealth <= 0 && !isDead)
                {
                    StartCoroutine(Death());
                }
                break;

            case UnitType.player:
                if (playerHealth.currentHealth <= 0 && !isDead)
                {
                    StartCoroutine(Death());
                }
                break;
        }
    }

    IEnumerator Death()
    {
        isDead = true;
        if (deathParticle != null)
        {
            Instantiate(deathParticle, transform.position, Quaternion.identity);
        }
        yield return null;

        Destroy(gameObject);
    }
}
