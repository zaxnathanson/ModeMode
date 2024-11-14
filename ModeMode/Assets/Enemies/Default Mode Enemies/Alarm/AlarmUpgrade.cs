using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(menuName = "Upgrades/AlarmUpgrade")]
public class AlarmUpgrade : Upgrade
{
    [SerializeField] public float AttackSpeedBonusDuration = 0.4f;
    [SerializeField] public float AttackSpeedBonusIncrease = 1.5f;
    [SerializeField] public float eAttackSpeedBonusIncrease = 0.3f;
    bool buffActive;
    bool buffTriggered;
    float buffTimer = 0;
    float attackSpeedChange;

    public override void Setup(UpgradeHandler ctx)
    {
        base.Setup(ctx);
        buffTimer = 0;
        statsRef = upgradeHandler.gameObject.GetComponent<Stats>();
        attackSpeedChange = 0;
        buffActive = false;
        buffTriggered = false;
    }

    public override void CallUpdate(float deltaTime)
    {
        if (statsRef.shootingStats.ammo == statsRef.shootingStats.maxAmmo && !buffActive)
        {
            buffActive = true;
            buffTimer = 0;
        }


        if (buffActive)
        {
            if (!buffTriggered)
            {
                buffTriggered = true;
                float initialAS = statsRef.shootingStats.attackSpeed;
                statsRef.shootingStats.attackSpeed *= AttackSpeedBonusIncrease + (eAttackSpeedBonusIncrease * upgradeHandler.GetUpgradeOfType(this).amount);
                attackSpeedChange = statsRef.shootingStats.attackSpeed - initialAS;
            }

            buffTimer += deltaTime;
            if (buffTimer >= AttackSpeedBonusDuration)
            {
                statsRef.shootingStats.attackSpeed -= attackSpeedChange;
                buffTriggered = false;
                buffActive = false;
            }
        }
    }

}
