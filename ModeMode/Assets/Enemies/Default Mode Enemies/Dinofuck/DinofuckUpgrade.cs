using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Upgrades/Dinofuck")]
public class DinofuckUpgrade : Upgrade
{
    int currentNumUpgrades = 0;
    public int ammoIncrease = 2;
    public int eAmmoIncrease = 1;

    public override void Setup(UpgradeHandler ctx)
    {
        currentNumUpgrades = 0;
        base.Setup(ctx);
        statsRef.shootingStats.maxAmmo += ammoIncrease;
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
        statsRef.shootingStats.maxAmmo += eAmmoIncrease;
    }
}
