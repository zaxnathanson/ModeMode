using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Upgrades/FlowerUpgrade")]
public class FlowerUpgrade : Upgrade
{
    int currentNumUpgrades = 0;
    public float AttackSpeedIncrease = 0.8f;
    public float eAttackSpeedIncrease = 0.5f;

    public override void Setup(UpgradeHandler ctx)
    {
        currentNumUpgrades = 0;

        base.Setup(ctx);
        Debug.Log("lol");
        statsRef = upgradeHandler.gameObject.GetComponent<Stats>();

        statsRef.shootingStats.attackSpeed += AttackSpeedIncrease;
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
        statsRef.shootingStats.attackSpeed += eAttackSpeedIncrease;
    }
}
