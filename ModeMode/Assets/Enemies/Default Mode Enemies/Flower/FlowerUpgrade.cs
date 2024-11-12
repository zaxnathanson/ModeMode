using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowerUpgrade : Upgrade
{
    int currentNumUpgrades = 0;
    [SerializeField] public float AttackSpeedBonusIncrease = 0.8f;
    [SerializeField] public float eAttackSpeedBonusIncrease = 0.4f;
    void OnEnable()
    {
        statsRef = GetComponent<Stats>();

        statsRef.shootingStats.attackSpeed += AttackSpeedBonusIncrease;
    }

    private void Update()
    {
        if (currentNumUpgrades != numOfUpgrade)
        {
            AddStat();
            currentNumUpgrades++;
        }
    }

    void AddStat()
    {
        statsRef.shootingStats.attackSpeed += eAttackSpeedBonusIncrease;

    }
}
