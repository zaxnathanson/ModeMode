using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SprayUpgrade : Upgrade
{
    int currentNumUpgrades = 0;
    [SerializeField] public float reloadSpeedIncrease = 0.3f;
    [SerializeField] public float eReloadSpeedIncrease = 0.3f;

    void OnEnable()
    {
        statsRef = GetComponent<Stats>();

        statsRef.shootingStats.reloadSpeed += reloadSpeedIncrease;

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
        statsRef.shootingStats.reloadSpeed += eReloadSpeedIncrease;


    }
}
