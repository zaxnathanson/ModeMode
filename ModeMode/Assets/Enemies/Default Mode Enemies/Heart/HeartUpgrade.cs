using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Upgrades/HeartUpgrade")]
public class HeartUpgrade : Upgrade
{
    int currentNumUpgrades = 0;
    [SerializeField] public int HealingAmount = 3;
    public override void Setup(UpgradeHandler ctx)
    {
        currentNumUpgrades = 0;

        base.Setup(ctx);

        statsRef = upgradeHandler.gameObject.GetComponent<Stats>();
        Heal();
    }


    public override void CallUpdate(float deltaTime)
    {
        if (currentNumUpgrades != upgradeHandler.GetUpgradeOfType(this).amount)
        {
            Heal();
            currentNumUpgrades++;
        }
    }

    void Heal()
    {
        Debug.Log("Heal");
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
