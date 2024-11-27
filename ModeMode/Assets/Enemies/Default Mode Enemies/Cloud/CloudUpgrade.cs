using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Upgrades/Cloud")]
public class CloudUpgrade : Upgrade
{
    int currentNumUpgrades = 0;
    public float shotSpeedIncrease = 1;
    public float eShotSpeedIncrease = 1;


    public override void Setup(UpgradeHandler ctx)
    {
        currentNumUpgrades = 0;
        base.Setup(ctx);
        statsRef.shootingStats.shotSpeedMax += shotSpeedIncrease;
        statsRef.shootingStats.shotSpeedMin += shotSpeedIncrease;

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
        statsRef.shootingStats.shotSpeedMax += eShotSpeedIncrease;
        statsRef.shootingStats.shotSpeedMin += eShotSpeedIncrease;

    }
}
