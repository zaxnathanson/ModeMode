using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Upgrades/Pencil")]
public class PencilUpgrade : Upgrade
{
    int currentNumUpgrades = 0;
    public int PierceIncrease = 1;
    public int ePierceIncrease = 1;


    public override void Setup(UpgradeHandler ctx)
    {
        currentNumUpgrades = 0;
        base.Setup(ctx);
        statsRef.shootingStats.pierceAmount += PierceIncrease;
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
        statsRef.shootingStats.pierceAmount += ePierceIncrease;
    }
}