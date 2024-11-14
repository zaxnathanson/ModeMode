using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CapsuleUI : MonoBehaviour
{
    Stats playerStats;
    [SerializeField] TextMeshProUGUI text;
    void Start()
    {
        playerStats = GameObject.FindWithTag("Player").GetComponent<Stats>();

    }

    // CallUpdate is called once per frame
    void Update()
    {   
        text.text = playerStats.playerSpecific.capsules.ToString();
    }
}
