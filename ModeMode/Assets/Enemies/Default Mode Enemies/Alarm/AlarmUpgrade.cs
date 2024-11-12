using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AlarmUpgrade : Upgrade
{
    [SerializeField] public float AttackSpeedBonusDuration = 0.4f;
    [SerializeField] public float AttackSpeedBonusIncrease = 1.5f;
    [SerializeField] public float eAttackSpeedBonusIncrease = 0.3f;
    bool buffActive;
    bool buffTriggered;
    float buffTimer = 0;
    float attackSpeedChange;
    void OnEnable()
    {
        statsRef = GetComponent<Stats>();
    }

    private void Update()
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
                statsRef.shootingStats.attackSpeed *= AttackSpeedBonusIncrease + (eAttackSpeedBonusIncrease * numOfUpgrade);
                attackSpeedChange = statsRef.shootingStats.attackSpeed - initialAS;
            }

            buffTimer += Time.deltaTime;
            if (buffTimer >= AttackSpeedBonusDuration)
            {
                statsRef.shootingStats.attackSpeed -= attackSpeedChange;
                buffTriggered = false;
                buffActive = false;
            }
        }
    }

}
