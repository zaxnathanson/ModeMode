using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KiteUpgrade : Upgrade
{
    int currentNumUpgrades = 0;
    [SerializeField] public float speedBonusIncrease = 0.4f;
    [SerializeField] public float eSpeedBonusIncrease = 0.25f;
    void OnEnable()
    {
        statsRef = GetComponent<Stats>();

        statsRef.movingStats.maxSpeed += speedBonusIncrease;
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
        statsRef.shootingStats.attackSpeed += eSpeedBonusIncrease;

    }
}
