using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoseUpgrade : Upgrade
{
    int currentNumUpgrades = 0;
    [SerializeField] public float criticalIncrease = 10;
    [SerializeField] public float eCriticalIncrease = 10;
    
    void OnEnable()
    {
        statsRef = GetComponent<Stats>();

        statsRef.shootingStats.critChance += criticalIncrease;
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
        statsRef.shootingStats.critChance += eCriticalIncrease;

    }
}
