using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyUpgrade : Upgrade
{
    int currentNumUpgrades = 0;
    [SerializeField] public float AttackIncrease = 0.5f;
    [SerializeField] public float eAttackIncrease = 0.25f;
    void OnEnable()
    {
        statsRef = GetComponent<Stats>();

        statsRef.shootingStats.damage += AttackIncrease;
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
        statsRef.shootingStats.damage += eAttackIncrease;

    }
}
