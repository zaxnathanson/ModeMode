using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Upgrades/GoldKeyUpgrade")]
public class GoldKeyUpgradeTest : Upgrade
{
    int currentNumUpgrades = 0;
    public float AttackMultiplier = 1.2f;
    public float eAttackMultiplier = 1.1f;

    public override void Setup(UpgradeHandler ctx)
    {
        currentNumUpgrades = 0;
        base.Setup(ctx);
        statsRef = upgradeHandler.gameObject.GetComponent<Stats>();
        statsRef.shootingStats.damage *= AttackMultiplier;
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
        statsRef.shootingStats.damage *= eAttackMultiplier;
    }
}
