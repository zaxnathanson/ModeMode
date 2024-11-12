using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DinofuckUpgrade : Upgrade
{
    int currentNumUpgrades = 0;
    void OnEnable()
    {
        statsRef = GetComponent<Stats>();
        AddStat();
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
        statsRef.shootingStats.maxAmmo++;
        statsRef.shootingStats.ammo++;
    }
}
