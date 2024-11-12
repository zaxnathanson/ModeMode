using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KingRuntUpgrade : Upgrade
{
    int currentNumUpgrades = 0;
    [SerializeField] public float rangeIncrease = 1.5f;
    [SerializeField] public float eRangeIncrease = 1f;

    [SerializeField] public float spreadDecrease = 1.5f;
    [SerializeField] public float eSpreadDecrease = 1;


    void OnEnable()
    {
        statsRef = GetComponent<Stats>();

        statsRef.shootingStats.range += rangeIncrease;
        if (statsRef.shootingStats.spread > 0)
        {
            statsRef.shootingStats.spread -= spreadDecrease;
            if (statsRef.shootingStats.spread < 0)
            {
                statsRef.shootingStats.spread = 0;
            }
        }
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
        statsRef.shootingStats.range += eRangeIncrease;
        if (statsRef.shootingStats.spread > 0)
        {
            statsRef.shootingStats.spread -= eSpreadDecrease;
            if (statsRef.shootingStats.spread < 0)
            {
                statsRef.shootingStats.spread = 0;
            }

        }
    }
}
