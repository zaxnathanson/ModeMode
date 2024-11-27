using DamageNumbersPro;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Upgrades/Book")]
public class BookUpgrade : Upgrade
{
    int currentNumUpgrades = 0;
    public float startDuration = 0.3f;
    public float durationIncrease = 0.2f;
    float duration = 0.2f;
    public float triggerChance;
    float timer;
    public GameObject shield;
    ParticleSystem createdShield;
    public override void Setup(UpgradeHandler ctx)
    {
        currentNumUpgrades = 0;
        timer = 0;
        base.Setup(ctx);
        duration = startDuration;
        statsRef.myGotHitEvent += BlockDamage;
        createdShield = Instantiate(shield, statsRef.gameObject.transform.position, shield.transform.rotation, statsRef.gameObject.transform).GetComponent<ParticleSystem>();
    }

    public override void CallUpdate(float deltaTime)
    {
        if (currentNumUpgrades != upgradeHandler.GetUpgradeOfType(this).amount)
        {
            AddStat();
            currentNumUpgrades++;
        }

        if (timer < duration)
        {
            timer += deltaTime;
            statsRef.isInvincible = true;
            createdShield.Play();
        }
        else
        {
            statsRef.isInvincible = false;
            createdShield.Stop();

        }
    }

    void BlockDamage()
    {
        if (Random.Range(0f, 100f) / statsRef.luck <= triggerChance)
        {
            statsRef.isInvincible = true;
            timer = 0;
            Vector3 spawnPos = upgradeHandler.gameObject.transform.position + new Vector3(0, 1, 0);
            DamageNumber newDNP = effectText.Spawn(spawnPos, upgradeHandler.gameObject.transform);
            newDNP.leftText = "Blocked!";
        }
    }

    void AddStat()
    {
        duration += durationIncrease;
    }
}

