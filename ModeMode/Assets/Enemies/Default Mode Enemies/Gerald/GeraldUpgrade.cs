using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Upgrades/Gerald")]
public class GeraldUpgrade : Upgrade
{
    int currentNumUpgrades = 0;
    public float newAngles;

    public override void Setup(UpgradeHandler ctx)
    {
        currentNumUpgrades = 0;
        base.Setup(ctx);
        statsRef = upgradeHandler.gameObject.GetComponent<Stats>();

        statsRef.shootingStats.bulletAngles = new float[2];
        statsRef.shootingStats.bulletAngles[0] = newAngles;
        statsRef.shootingStats.bulletAngles[1] = -newAngles;

        GameObject originalProjectileSpawn = statsRef.shootingStats.projectileSpawn[0];
        statsRef.shootingStats.projectileSpawn = new GameObject[2];
        statsRef.shootingStats.projectileSpawn[0] = originalProjectileSpawn;
        statsRef.shootingStats.projectileSpawn[1] = originalProjectileSpawn;

        statsRef.shootingStats.bulletsPerShot = 2;
    }

    public override void CallUpdate(float deltaTime)
    {
        if (currentNumUpgrades != upgradeHandler.GetUpgradeOfType(this).amount)
        {
            AddStat();
            currentNumUpgrades++;
        }
    }

    void AddStat()
    {
        return;
    }
}
