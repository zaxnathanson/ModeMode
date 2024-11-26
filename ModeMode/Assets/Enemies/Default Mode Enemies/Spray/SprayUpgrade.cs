using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Upgrades/Spray")]
public class SprayUpgrade : Upgrade
{
    int currentNumUpgrades = 0;
    public float reloadSpeedIncrease = 0.5f;
    public float eReloadSpeedIncrease = 0.5f;


    public override void Setup(UpgradeHandler ctx)
    {
        currentNumUpgrades = 0;
        base.Setup(ctx);
        statsRef.shootingStats.reloadSpeed += reloadSpeedIncrease;

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
        statsRef.shootingStats.reloadSpeed += eReloadSpeedIncrease;
    }
}
