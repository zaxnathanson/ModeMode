using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Upgrades/Shirt")]
public class ShirtUpgrade : Upgrade
{
    int currentNumUpgrades = 0;
    public float damageIncrease = 1;
    public float edamageIncrease = 1;
    public Upgrade pantsUpgrade;
    bool hasPants = false;

    public override void Setup(UpgradeHandler ctx)
    {
        currentNumUpgrades = 0;
        base.Setup(ctx);
        statsRef.shootingStats.damage += damageIncrease;
        hasPants = false;
    }

    public override void CallUpdate(float deltaTime)
    {
        if (currentNumUpgrades != upgradeHandler.GetUpgradeOfType(this).amount)
        {
            AddStat();
            currentNumUpgrades++;
        }
        if (!hasPants)
        {
            if (upgradeHandler.CheckContainerForType(pantsUpgrade))
            {
                statsRef.playerHealth.maxHealth++;
                statsRef.playerHealth.currentHealth++;
                hasPants=true;
            }
        }

    }
    void AddStat()
    {
        statsRef.shootingStats.damage += edamageIncrease;
    }
}
