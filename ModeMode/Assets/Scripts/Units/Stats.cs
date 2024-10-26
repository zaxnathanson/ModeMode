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
    public Material normalMaterial;
    public Material hitMaterial;
    public GameObject hitParticle;
    public SpriteRenderer spriteRenderer;
    public bool isInvincible;

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
        public float cameraShakeDuration;
        public float cameraShakeStrength;
        public int cameraShakeVibrato;
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
        [HideInInspector] public bool isMoving;
        [HideInInspector] public bool canMove;

        [Header("Enemy Specific")]
        public bool doesCollisionDamage;
        public Vector2 targetOffset;
        public float stopDistance;
        public float damage;
        public bool doesFly;
        public bool doesFlip;


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
        public float lifetime;
        public float shotSpeed;
        public float spread;
        public float size;
        public int pierceAmount;
        public float acceleration;
        public bool doesMotionEffectVelocity;
        //public float knockBack;
        //public float sinMovementAmount;
        //public float sinMovementSpeed;
        public GameObject deathGameObject;
        public GameObject[] projectileSpawn;
        public int bulletsPerShot;
        public float[] bulletAngles;

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
        public float homingAmount;
        public bool fixedBurst;
        public bool stopWhileShooting;
        public float timeBeforeStopShooting;
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

    public IEnumerator TakeDamage(float damage, bool isCritical)
    {
        if (!isInvincible)
        {
            switch (unitType)
            {
                case UnitType.enemy:

                    enemyHealth.currentHealth -= damage;

                    if (hitParticle != null)
                    {
                        Instantiate(hitParticle, transform.position, Quaternion.identity);
                    }

                    spriteRenderer.material = hitMaterial;
                    yield return new WaitForSeconds(0.1f);
                    spriteRenderer.material = normalMaterial;

                    if (isCritical)
                    {
                        Transform num = enemyHealth.critNumber.Spawn(transform.localPosition, damage).transform;
                        num.position = new Vector3(num.position.x, 0, num.position.z);
                    }
                    else
                    {
                        Transform num = enemyHealth.damageNumber.Spawn(transform.localPosition, damage).transform;
                        num.position = new Vector3(num.position.x, 0, num.position.z);
                    }

                    break;

                case UnitType.player:

                    playerHealth.currentHealth -= (int)damage;
                    
                    isInvincible = true;
                    
                    if (hitParticle != null)
                    {
                        Instantiate(hitParticle, transform.position, Quaternion.identity);
                    }
                    spriteRenderer.material = hitMaterial;

                    Time.timeScale = 0;
                    float elapsed = 0;
                    while (elapsed < 1)
                    {
                        if (elapsed > 0.1f)
                        {
                            spriteRenderer.material = normalMaterial;
                        }
                        elapsed += Time.unscaledDeltaTime;
                        Time.timeScale = Mathf.Lerp(0, 1, elapsed);
                        yield return null;
                    }
                    Time.timeScale = 1;

                    isInvincible = false;

                    break;
            }


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
        EffectManager.instance.CameraShake(enemyHealth.cameraShakeDuration, enemyHealth.cameraShakeStrength, enemyHealth.cameraShakeVibrato);
        yield return null;

        Destroy(gameObject);
    }
}
