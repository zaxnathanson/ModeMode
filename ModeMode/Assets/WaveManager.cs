using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    public Mode currentMode;
    Transform enemyContainer;
    void Awake()
    {
        enemyContainer = GameObject.FindWithTag("EnemyContainer").transform;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
