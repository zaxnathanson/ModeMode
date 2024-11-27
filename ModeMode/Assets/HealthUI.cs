using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour
{
    [SerializeField] GameObject healthUIObject;
    Stats statsRef;
    int uiHealth;
    int uiMaxHealth;

    void Start()
    {
        statsRef = GameObject.FindWithTag("Player").GetComponent<Stats>();

        ChangeTotalHearts();
        uiHealth = statsRef.playerHealth.maxHealth;
        uiMaxHealth = statsRef.playerHealth.maxHealth;
        ChangeHealth();
    }

    void ChangeTotalHearts()
    {
        while (transform.childCount < statsRef.playerHealth.maxHealth)
        {
            Instantiate(healthUIObject, transform);
        }
        for (int i = transform.childCount;  i > statsRef.playerHealth.maxHealth; i--)
        {
            Destroy(transform.GetChild(transform.childCount - i).gameObject);
        }
    }
    void ChangeHealth()
    {
        for (int i = 0; i < statsRef.playerHealth.maxHealth; i++)
        {
            if (i < statsRef.playerHealth.currentHealth)
            {
                transform.GetChild(i).GetChild(0).GetComponent<Image>().enabled = true;

            }
            else
            {
                transform.GetChild(i).GetChild(0).GetComponent<Image>().enabled = false;
                Debug.Log(i);
                Debug.Log(statsRef.playerHealth.currentHealth);
                if (i == statsRef.playerHealth.currentHealth)
                {
                    transform.GetChild(i).GetComponent<RectTransform>().DOPunchScale(Vector3.one * 0.3f, 0.2f);
                }
            }
        }
    }
    void Update()
    {
        if (uiMaxHealth != statsRef.playerHealth.maxHealth)
        {
            ChangeTotalHearts();
        }
        uiMaxHealth = statsRef.playerHealth.maxHealth;

        if (uiHealth != statsRef.playerHealth.currentHealth)
        {
            ChangeHealth();
        }
        uiHealth = statsRef.playerHealth.currentHealth;


    }
}
