using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudUpgrade : Upgrade
{
    int currentNumUpgrades = 0;
    [SerializeField] public float shotSpeedIncrease = 2f;
    [SerializeField] public float eShotSpeedIncrease = 2f;
    [SerializeField] public float movementSpeedIncrease = 0.4f;
    [SerializeField] public float eMovementSpeedIncrease = 0;
    void OnEnable()
    {
        statsRef = GetComponent<Stats>();

        statsRef.shootingStats.shotSpeedMax += shotSpeedIncrease;
        statsRef.shootingStats.shotSpeedMin += shotSpeedIncrease;
        statsRef.movingStats.maxSpeed += movementSpeedIncrease;

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
        statsRef.shootingStats.shotSpeedMax += eShotSpeedIncrease;
        statsRef.shootingStats.shotSpeedMin += eShotSpeedIncrease;
        statsRef.movingStats.maxSpeed += eMovementSpeedIncrease;

    }
}
