using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeartUI : MonoBehaviour
{
    [SerializeField] Sprite[] fullHearts;
    [SerializeField] Sprite[] emptyHearts;
    [SerializeField] Image fullHeartRenderer, emptyHeartRenderer;

    void Awake()
    {
        int i = Random.Range(0, fullHearts.Length);
        fullHeartRenderer.sprite = fullHearts[i];
        emptyHeartRenderer.sprite = emptyHearts[i];
    }
}
