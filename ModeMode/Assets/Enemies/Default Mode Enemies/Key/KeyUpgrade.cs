using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Upgrades/Key")]
public class KeyUpgrade : Upgrade
{
    int currentNumUpgrades = 0;
    public float damageIncrease = 0.3f;
    public float eDamageIncrease = 0.2f;


    public override void Setup(UpgradeHandler ctx)
    {
        currentNumUpgrades = 0;
        base.Setup(ctx);
        statsRef.shootingStats.damage += damageIncrease;

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
        statsRef.shootingStats.damage += eDamageIncrease;
    }
}
