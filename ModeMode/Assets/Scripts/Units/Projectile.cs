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

    public void SetupBullet(Stats.ShootingStats shootingStats, float rotation)
    {
        statsRef = shootingStats;
        transform.localScale = Vector3.one * shootingStats.size;
        transform.rotation = Quaternion.Euler(0, rotation + Random.Range(-statsRef.spread, statsRef.spread), 0);
        shadow.size = new Vector3(transform.localScale.x, transform.localScale.y, shadow.size.z);
        startPos = transform.position;
        
        //calculate critical strike
        if (Random.Range(0f,100f) <= statsRef.critChance)
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

        //launches bullet
        rb.AddForce(statsRef.shotSpeed * new Vector3(transform.right.x, 0, -transform.right.z), ForceMode.Impulse);
    }

    // Update is called once per frame
    void Update()
    {
        shadow.size = new Vector3(transform.localScale.x, transform.localScale.y, shadow.size.z);
        if (Mathf.Abs((startPos - transform.position).magnitude) > statsRef.range)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy" && statsRef.projectileType == Stats.ShootingStats.ProjectileType.player)
        {
            other.GetComponent<Stats>().TakeDamage(statsRef.damage, isCritical);
            StartCoroutine(ProjectileHit());
        }
        if (other.tag == "Player" && statsRef.projectileType == Stats.ShootingStats.ProjectileType.enemy)
        {
            other.GetComponent<Stats>().TakeDamage(statsRef.damage, isCritical);
            StartCoroutine(ProjectileHit());
        }
    }

    IEnumerator ProjectileHit()
    {
        yield return null;
        Destroy(gameObject) ;
    }
}
