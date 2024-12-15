using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static GameController;

public class PauseUI : MonoBehaviour
{
    [SerializeField] RectTransform rectTransform;
    void Update()
    {
        switch (instance.gameState)
        {
            case GameStates.PLAYING:
                rectTransform.anchoredPosition = new Vector3(10000, 0, 0);
                break;
            case GameStates.PAUSED:
                rectTransform.anchoredPosition = Vector3.zero;

                break;
            case GameStates.DEAD:
                rectTransform.anchoredPosition = new Vector3(10000, 0, 0);
                break;
        }
    }
}
