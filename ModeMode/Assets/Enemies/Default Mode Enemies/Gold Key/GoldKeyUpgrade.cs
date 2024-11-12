using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldKeyUpgrade : Upgrade
{
    int currentNumUpgrades = 0;
    [SerializeField] public float AttackMultiplier = 1.2f;
    [SerializeField] public float eAttackMultiplier = 1.1f;
    void OnEnable()
    {
        statsRef = GetComponent<Stats>();

        statsRef.shootingStats.damage *= AttackMultiplier;
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
        statsRef.shootingStats.damage *= eAttackMultiplier;

    }
}
