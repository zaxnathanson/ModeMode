using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Upgrades/Rose")]
public class RoseUpgrade : Upgrade
{
    int currentNumUpgrades = 0;
    public float sizeIncrease = 1.3f;
    public float critChanceIncrease = 1;
    public float eCritChanceIncrease = 1;


    public override void Setup(UpgradeHandler ctx)
    {
        currentNumUpgrades = 0;
        base.Setup(ctx);
        statsRef.shootingStats.critChance += critChanceIncrease;
        statsRef.shootingStats.size *= sizeIncrease;
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
        statsRef.shootingStats.critChance += eCritChanceIncrease;
    }
}
