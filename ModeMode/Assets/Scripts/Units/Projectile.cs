using DamageNumbersPro;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;

public class Projectile : MonoBehaviour
{
    [HideInInspector] public Stats.ShootingStats statsRef;
    [SerializeField] Rigidbody rb;
    [SerializeField] DecalProjector shadow;
    Vector3 startPos;
    [SerializeField] MeshRenderer mr;
    public bool isCritical = false;
    int timesPierced = 0;
    float lifeTimer = 0;
    public void SetupBullet(Stats.ShootingStats shootingStats, float rotation, int shotNum, Vector3 addedForce)
    {
        statsRef = shootingStats;
        transform.localScale = Vector3.one * shootingStats.size;

        if (shootingStats.shootAtPlayer)
        {
            transform.rotation = Quaternion.Euler(0, rotation + Random.Range(-statsRef.spread, statsRef.spread) + statsRef.bulletAngles[shotNum], 0);
        }
        else
        {
            transform.rotation = Quaternion.Euler(0, Random.Range(-statsRef.spread, statsRef.spread) + statsRef.bulletAngles[shotNum], 0);
        }

        startPos = transform.position;

        //calculate critical strike
        if (Random.Range(0f, 100f) <= statsRef.critChance)
        {
            isCritical = true;
        }
        if (isCritical)
        {
            statsRef.damage *= statsRef.critDamage / 100;
        }

        //set color of materials
        MaterialPropertyBlock propertyBlock = new MaterialPropertyBlock();
        propertyBlock.SetColor("_Color", statsRef.outsideColor);
        propertyBlock.SetColor("_BaseColor", statsRef.insideColor);
        mr.SetPropertyBlock(propertyBlock);

        shadow.size = new Vector3(transform.localScale.x, transform.localScale.y, shadow.size.z);

        //launches bullet
        rb.AddForce(statsRef.shotSpeed * new Vector3(transform.right.x, 0, -transform.right.z) + addedForce, ForceMode.Impulse);
    }

    private void LateUpdate()
    {
        
    }
    // Update is called once per frame
    void Update()
    {


        shadow.size = new Vector3(transform.localScale.x, transform.localScale.y, shadow.size.z);
        if (Mathf.Abs((startPos - transform.position).magnitude) > statsRef.range)
        {
            StartCoroutine(Destroy());

        }

        if (statsRef.lifetime > 0)
        {
            lifeTimer += Time.deltaTime;
            if (lifeTimer > statsRef.lifetime)
            {
                StartCoroutine(Destroy());
            }
        }

        Acceleration();
        SinMovement();
    }

    void SinMovement()
    {

    }

    void Acceleration()
    {
        rb.velocity += rb.velocity.normalized * statsRef.acceleration * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy" && statsRef.projectileType == Stats.ShootingStats.ProjectileType.player)
        {
            Stats otherStats = other.GetComponent<Stats>();
            otherStats.TakeDamage(statsRef.damage, isCritical);

            timesPierced++;
            if (timesPierced > statsRef.pierceAmount)
            {
                StartCoroutine(Destroy());
            }
        }
        if (other.tag == "Player" && statsRef.projectileType == Stats.ShootingStats.ProjectileType.enemy)
        {
            other.GetComponent<Stats>().TakeDamage(statsRef.damage, isCritical);
            timesPierced++;
            if (timesPierced > statsRef.pierceAmount)
            {
                StartCoroutine(Destroy());
            }
        }
    }

    IEnumerator Destroy()
    {
        yield return null;
        if (statsRef.deathParticle != null)
        {
            Instantiate(statsRef.deathParticle, transform.position, transform.rotation);
        }
        if (statsRef.deathGameObject != null)
        {
            Instantiate(statsRef.deathGameObject, transform.position, transform.rotation);
        }
        Destroy(gameObject) ;
    }
}
