using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SliderUI : MonoBehaviour
{
    Stats playerStats;
    [SerializeField] Slider slider;
    [SerializeField] TextMeshProUGUI levelText;
    void Awake()
    {
        playerStats = GameObject.FindWithTag("Player").GetComponent<Stats>();
    }

    // CallUpdate is called once per frame
    void Update()
    {
        slider.value = playerStats.playerSpecific.currentExp / playerStats.playerSpecific.expCurve.Evaluate(playerStats.playerSpecific.currentLevel);
        levelText.text = "LEVEL " + (playerStats.playerSpecific.currentLevel + 1).ToString();
    }
}
