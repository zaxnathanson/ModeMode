using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Upgrades/Pants")]
public class PantsUpgrade : Upgrade
{
    int currentNumUpgrades = 0;
    public float movementIncrease = 1;
    public float eMovementIncrease = 1;
    public Upgrade shirtUpgrade;

    public override void Setup(UpgradeHandler ctx)
    {
        currentNumUpgrades = 0;
        base.Setup(ctx);
        statsRef.movingStats.maxSpeed += movementIncrease;

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
        statsRef.movingStats.maxSpeed += eMovementIncrease;

    }
}
