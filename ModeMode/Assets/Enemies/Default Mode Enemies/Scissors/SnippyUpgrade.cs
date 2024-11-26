using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

[CreateAssetMenu(menuName = "Upgrades/Snippy")]
public class SnippyUpgrade : Upgrade
{
    int currentNumUpgrades = 0;
    PlayerMovement playerMovement;
    public float spawnFrequency;
    Vector3 previousPos;

    public float startLifetime;

    float lifetime;

    public float eStartLifetime;

    public override void Setup(UpgradeHandler ctx)
    {
        currentNumUpgrades = 0;
        lifetime = startLifetime;
        base.Setup(ctx);
        playerMovement = upgradeHandler.gameObject.GetComponent<PlayerMovement>(); 
    }

    public override void CallUpdate(float deltaTime)
    {
        if (currentNumUpgrades != upgradeHandler.GetUpgradeOfType(this).amount)
        {
            AddStat();
            currentNumUpgrades++;
        }

        if (!playerMovement.canRoll)
        {
            if (Mathf.Abs((previousPos - upgradeHandler.gameObject.transform.position).magnitude) > spawnFrequency)
            {
                SpawnBullet();
                previousPos = upgradeHandler.gameObject.transform.position;
            }
        }
    }
    void SpawnBullet()
    {
        Vector3 spawnPos = new Vector3(upgradeHandler.gameObject.transform.position.x, 0, upgradeHandler.gameObject.transform.position.z);


        GameObject newProjectile = Instantiate(statsRef.shootingStats.bulletPrefab, spawnPos, Quaternion.identity);
        Projectile projectileScript = newProjectile.GetComponent<Projectile>();

        Vector3 addedForce = Vector3.zero;


        projectileScript.SetupBullet(statsRef.shootingStats, 0, 0, addedForce, true, lifetime);

    }
    void AddStat()
    {
        lifetime += eStartLifetime;
    }
}
