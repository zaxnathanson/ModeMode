using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ExtraUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI timerText;
    [SerializeField] TextMeshProUGUI waveText;
    [SerializeField] TextMeshProUGUI speed, damage, attackSpeed, range, reloadSpeed, critChance, luck;
    Stats playerStats;
    void Awake()
    {
        playerStats =GameObject.FindWithTag("Player").GetComponent<Stats>();
    }

    // Update is called once per frame
    void Update()
    {
        timerText.text = GameController.instance.gameTime.ToString("F2");
        waveText.text = "Wave " + (WaveManager.Instance.wave +1).ToString();
        speed.text = playerStats.movingStats.maxSpeed.ToString("F1");
        damage.text = playerStats.shootingStats.damage.ToString("F1");
        attackSpeed.text = playerStats.shootingStats.attackSpeed.ToString("F1") + "/s"; ;
        range.text = playerStats.shootingStats.range.ToString("F1");
        reloadSpeed.text = playerStats.shootingStats.reloadSpeed.ToString("F1")+"/s";
        critChance.text = playerStats.shootingStats.critChance.ToString("F1") + "%";
        luck.text = "x" + playerStats.luck.ToString("F1");
    }
}
