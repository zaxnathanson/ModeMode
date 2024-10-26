using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectManager : MonoBehaviour
{
    public static EffectManager instance;
    public GameObject cameras;
    Tween shakeTween;
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!shakeTween.IsActive())
        {
            Debug.Log("Stop");
            cameras.transform.localPosition = Vector3.zero;
        }
    }

    public void CameraShake(float duration, float strength, int vibrato)
    {
        Debug.Log("shake");
        shakeTween = cameras.transform.DOShakePosition(duration, strength, vibrato);
    }
}
