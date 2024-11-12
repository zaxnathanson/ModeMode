using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarUpgrade : Upgrade
{
    int currentNumUpgrades = 0;
    [SerializeField] public float expMultipler = 0.1f;
    [SerializeField] public float eExpMultipler = 0.1f;
    void OnEnable()
    {
        statsRef = GetComponent<Stats>();

        statsRef.playerSpecific.expMultiplier += expMultipler;
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
        statsRef.playerSpecific.expMultiplier += eExpMultipler;

    }
}
