using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReloadBar : MonoBehaviour
{
    Stats playerStats;
    PlayerShooting playerShootingScript;

    [SerializeField] Slider slider;
    [SerializeField] GameObject sliderGameObject;
    float timer;
    void Awake()
    {
        playerStats = GameObject.FindWithTag("Player").GetComponent<Stats>();
        playerShootingScript = GameObject.FindWithTag("Player").GetComponent<PlayerShooting>();

    }

    // Update is called once per frame
    void Update()
    {
        if (playerShootingScript.reloadTimer >= 1/playerStats.shootingStats.reloadSpeed || playerShootingScript.reloadTimer ==0)
        {
            sliderGameObject.SetActive(false);

        }
        else
        {
            slider.value = playerShootingScript.reloadTimer / (1/playerStats.shootingStats.reloadSpeed);
            sliderGameObject.SetActive(true);

        }
    }
}
