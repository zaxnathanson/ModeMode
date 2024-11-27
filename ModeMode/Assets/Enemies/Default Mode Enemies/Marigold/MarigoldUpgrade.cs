using DamageNumbersPro;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Upgrades/MarigoldUpgrade")]
public class MarigoldUpgrade : Upgrade
{
    PlayerShooting playerShooting;
    [SerializeField] float refundChance = 10;

    public override void Setup(UpgradeHandler ctx)
    {

        base.Setup(ctx);

        statsRef = upgradeHandler.gameObject.GetComponent<Stats>();

        playerShooting = upgradeHandler.gameObject.GetComponent<PlayerShooting>();
        playerShooting.playerShoot += ChanceBullet;
    }


    void ChanceBullet()
    {
        for (int i = 0; i <= upgradeHandler.GetUpgradeOfType(this).amount; i++)
        {
            if (Random.Range(0f, 100f) / statsRef.luck <= refundChance)
            {
                Vector3 spawnPos = upgradeHandler.gameObject.transform.position + new Vector3(0, 1, 0);
                DamageNumber newDNP = effectText.Spawn(spawnPos, upgradeHandler.gameObject.transform);
                newDNP.leftText = "Refund!";
                if (statsRef.shootingStats.ammo < statsRef.shootingStats.maxAmmo)
                {
                    statsRef.shootingStats.ammo++;
                }
            }
        }

    }
}
