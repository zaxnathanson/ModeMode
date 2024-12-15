using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ExtraUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI timerText;
    [SerializeField] TextMeshProUGUI waveText;
    [SerializeField] RectTransform upgradeHolder;
    [SerializeField] RectTransform statsHolder;
    Vector2 timerTextStartPos, waveTextStartPos, upgradeHolderStartPos, statsHolderStartPos;
    [SerializeField] float moveDuration;
    [SerializeField] TextMeshProUGUI speed, damage, attackSpeed, range, reloadSpeed, critChance, luck;
    Stats playerStats;
    bool isActive = false;
    bool isUIMoving = false;
    void Awake()
    {
        playerStats = GameObject.FindWithTag("Player").GetComponent<Stats>();
        timerTextStartPos = timerText.rectTransform.anchoredPosition;
        waveTextStartPos = waveText.rectTransform.anchoredPosition;
        upgradeHolderStartPos = upgradeHolder.anchoredPosition;
        statsHolderStartPos = statsHolder.anchoredPosition;
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

        if(Input.GetKeyDown(KeyCode.Tab))
        {
            if (!isUIMoving)
            {
                if (isActive)
                {
                    isActive = false;
                    isUIMoving = true;
                    StartCoroutine(DisableExtraUI());
                }
                else
                {
                    isActive = true;
                    isUIMoving = true;
                    StartCoroutine(ActivateExtraUI());
                }
            }
        }
    }

    IEnumerator ActivateExtraUI()
    {
        
        timerText.rectTransform.DOAnchorPos(timerTextStartPos, moveDuration);
        waveText.rectTransform.DOAnchorPos(waveTextStartPos, moveDuration);
        upgradeHolder.DOAnchorPos(upgradeHolderStartPos, moveDuration);
        statsHolder.DOAnchorPos(statsHolderStartPos, moveDuration);
        yield return new WaitForSeconds(moveDuration);
        isUIMoving = false;
    }
    IEnumerator DisableExtraUI()
    {
        timerText.rectTransform.DOAnchorPos(-timerTextStartPos, moveDuration);
        waveText.rectTransform.DOAnchorPos(-waveTextStartPos, moveDuration);
        upgradeHolder.DOAnchorPos(new Vector2(upgradeHolderStartPos.x, -upgradeHolderStartPos.y), moveDuration);
        statsHolder.DOAnchorPos(new Vector2(-statsHolderStartPos.x, statsHolderStartPos.y), moveDuration);
        yield return new WaitForSeconds(moveDuration);
        isUIMoving = false;
    }
}
