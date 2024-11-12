using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarigoldUpgrade : Upgrade
{
    PlayerShooting playerShooting;
    float refundChance = 10;
     
    void OnEnable()
    {
        playerShooting = GetComponent<PlayerShooting>();
        statsRef = GetComponent<Stats>();
    }

    // Update is called once per frame
    void Update()
    {
        playerShooting.playerShoot += ChanceBullet;
    }

    void ChanceBullet()
    {
        Debug.Log("chancebullet");
        if (Random.Range(0f,100f)/statsRef.luck <= refundChance)
        {
            effectText.Spawn(transform.position);
            statsRef.shootingStats.ammo++;
        }
    }
}
