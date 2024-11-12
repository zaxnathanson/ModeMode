using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BronzeKeyUpgrade : Upgrade
{
    int currentNumUpgrades = 0;
    [SerializeField] public float critDamageIncrease = 50f;
    [SerializeField] public float eCritDamageIncrease = 50f;
    void OnEnable()
    {
        statsRef = GetComponent<Stats>();

        statsRef.shootingStats.critDamage += critDamageIncrease;
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
        statsRef.shootingStats.critDamage += eCritDamageIncrease;

    }
}
