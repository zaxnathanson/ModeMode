using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Upgrades/StarUpgrade")]
public class StarUpgrade : Upgrade
{
    int currentNumUpgrades = 0;
    public float expIncrease = 0.1f;
    public float eExpIncrease = 0.1f;

    public override void Setup(UpgradeHandler ctx)
    {
        currentNumUpgrades = 0;

        base.Setup(ctx);
        statsRef = upgradeHandler.gameObject.GetComponent<Stats>();

        statsRef.playerSpecific.expMultiplier += expIncrease;
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
        statsRef.playerSpecific.expMultiplier += eExpIncrease;
    }
}
