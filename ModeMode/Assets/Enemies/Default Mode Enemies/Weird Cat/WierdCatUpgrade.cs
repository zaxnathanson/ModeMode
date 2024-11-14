using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Upgrades/WeirdCatUpgrade")]
public class WierdCatUpgrade : Upgrade
{
    int currentNumUpgrades = 0;
    public float luckIncrease = 0.5f;
    public float eLuckIncrease = 0.5f;

    public override void Setup(UpgradeHandler ctx)
    {
        currentNumUpgrades = 0;
        base.Setup(ctx);
        statsRef = upgradeHandler.gameObject.GetComponent<Stats>();
        statsRef.luck += luckIncrease;
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
        statsRef.luck += eLuckIncrease;
    }
}
