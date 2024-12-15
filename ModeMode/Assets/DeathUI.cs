using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DeathUI : MonoBehaviour
{
    Stats playerStats;
    [SerializeField] RectTransform rectTransform;
    [SerializeField] TextMeshProUGUI waveStat, enemiesStat, upgradesStat, timeStat;
    [SerializeField] Transform deathUpgradeContainer;
    [SerializeField] Transform upgradeContainer;

    void Awake()
    {
        playerStats = GameObject.FindWithTag("Player").GetComponent<Stats>();
        rectTransform.anchoredPosition = new Vector3(10000, 0, 0);
    }

    private void OnEnable()
    {
        playerStats.playerDiedEvent += SetStats;
    }
    private void OnDisable()
    {
        playerStats.playerDiedEvent -= SetStats;
    }

    void SetStats()
    {
        waveStat.text = GameController.instance.waveNumber.ToString();
        enemiesStat.text = GameController.instance.enemiesKilled.ToString();
        upgradesStat.text = GameController.instance.totalUpgrades.ToString();
        timeStat.text = GameController.instance.gameTime.ToString("F1");
        foreach (Transform child in upgradeContainer)
        {
            Instantiate(child.gameObject, deathUpgradeContainer);
        }
        rectTransform.anchoredPosition = Vector3.zero;
        rectTransform.DOShakeAnchorPos(0.2f);
    }

    

    void Update()
    {
        
    }
}
