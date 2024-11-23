using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Upgrades/KingRuntUpgrade")]
public class KingRuntUpgrade : Upgrade
{
    int currentNumUpgrades = 0;
    public float spreadDecrease = 4;
    public float rangeIncrease = 1.2f;
    public float eRangeIncrease = 1.2f;


    public override void Setup(UpgradeHandler ctx)
    {
        currentNumUpgrades = 0;

        base.Setup(ctx);
        statsRef = upgradeHandler.gameObject.GetComponent<Stats>();

        statsRef.shootingStats.spread -= spreadDecrease;
        statsRef.shootingStats.range += rangeIncrease;

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
        statsRef.shootingStats.range += eRangeIncrease;
    }
}
