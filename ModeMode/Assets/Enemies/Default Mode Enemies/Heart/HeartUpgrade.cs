using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartUpgrade : Upgrade
{
    int currentNumUpgrades = 0;
    [SerializeField] public int HealingAmount = 3;
    void OnEnable()
    {
        statsRef = GetComponent<Stats>();

        Heal();
    }

    private void Update()
    {
        if (currentNumUpgrades != numOfUpgrade)
        {
            Heal();
            currentNumUpgrades++;
        }
    }

    void Heal()
    {
        int i = 0;

        while (statsRef.playerHealth.currentHealth < statsRef.playerHealth.maxHealth)
        {
            statsRef.playerHealth.currentHealth++;
            i++;
            if (i >= HealingAmount)
            {
                break;
            }
        }

    }
}
