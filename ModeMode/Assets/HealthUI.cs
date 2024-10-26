using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour
{
    [SerializeField] GameObject healthUIObject;
    Stats statsRef;
    int uiHealth;
    void Awake()
    {
        statsRef = GameObject.FindWithTag("Player").GetComponent<Stats>();

        ChangeTotalHearts();
        uiHealth = statsRef.playerHealth.maxHealth;

        ChangeHealth();
    }

    void ChangeTotalHearts()
    {
        while (transform.childCount < statsRef.playerHealth.maxHealth)
        {
            Instantiate(healthUIObject, transform);
        }
        while (transform.childCount > statsRef.playerHealth.maxHealth)
        {
            Destroy(transform.GetChild(transform.childCount - 1).gameObject);
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
            }
        }
    }
    void Update()
    {
        if (uiHealth != statsRef.playerHealth.currentHealth)
        {
            ChangeHealth();
        }
        uiHealth = statsRef.playerHealth.currentHealth;
    }
}
