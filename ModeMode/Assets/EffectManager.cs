using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
            cameras.transform.localPosition = Vector3.zero;
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            Time.timeScale = 1;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    public void CameraShake(float duration, float strength, int vibrato)
    {
        shakeTween = cameras.transform.DOShakePosition(duration, strength, vibrato);
    }
}
