using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Upgrades/BronzeKey")]
public class BronzeKeyUpgrade : Upgrade
{
    int currentNumUpgrades = 0;
    public float critDamageIncrease = 1;
    public float eCritDamageIncrease = 1;


    public override void Setup(UpgradeHandler ctx)
    {
        currentNumUpgrades = 0;
        base.Setup(ctx);
        statsRef.shootingStats.critDamage += critDamageIncrease;

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
        statsRef.shootingStats.critDamage += eCritDamageIncrease;

    }
}
