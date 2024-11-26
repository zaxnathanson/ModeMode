using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Upgrades/KiteUpgrade")]
public class KiteUpgrade : Upgrade
{
    int currentNumUpgrades = 0;
    public float speedIncrease = 1.2f;
    public float eSpeedIncrease = 1.1f;

    public override void Setup(UpgradeHandler ctx)
    {
        currentNumUpgrades = 0;
        base.Setup(ctx);
        statsRef = upgradeHandler.gameObject.GetComponent<Stats>();
        statsRef.movingStats.maxSpeed += speedIncrease;
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
        statsRef.movingStats.maxSpeed += eSpeedIncrease;
    }
}

