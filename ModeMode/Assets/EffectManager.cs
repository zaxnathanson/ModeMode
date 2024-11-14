using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EffectManager : MonoBehaviour
{
    public static EffectManager instance;
    public GameObject cameras;
    Tween shakeTween;
    public Camera[] cameraComponents;
    public Image fadeImage;

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

    // CallUpdate is called once per frame
    void Update()
    {
        if (!shakeTween.IsActive())
        {
            cameras.transform.localPosition = UnityEngine.Vector3.zero;
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

    public IEnumerator CameraZoom(float duration, float amount)
    {
        UnityEngine.Vector3 orthoSize = new UnityEngine.Vector3(Camera.main.orthographicSize, 0,0);
        float originalOrthoSize = Camera.main.orthographicSize;
        Tween orthoPunch = DOTween.Punch(() => orthoSize, x => orthoSize = x, new UnityEngine.Vector3(amount, 0, 0), duration);
        while (orthoPunch.active)
        {
            foreach (Camera cam in cameraComponents)
            {
                cam.orthographicSize = orthoSize.x;
            }
            yield return null;
        }
        foreach (Camera cam in cameraComponents)
        {
            cam.orthographicSize = originalOrthoSize;
        }
    }

    public IEnumerator ScreenFade(Color color, float targetOpacity, float fadeInDuration, float fadeOutDuration, float solidDuration)
    {
        float elapsed = 0;

        fadeImage.color = new Color(color.r,color.g,color.b,0);
        Color initialColor = fadeImage.color;
        Color targetColor = new Color(color.r, color.g, color.b, targetOpacity);

        while (elapsed < fadeInDuration)
        {
            fadeImage.color = Color.Lerp(initialColor, targetColor, elapsed/fadeInDuration);
            elapsed += Time.deltaTime;
            yield return null;
        }
        fadeImage.color = targetColor;

        yield return new WaitForSeconds(solidDuration);

        elapsed = 0;

        while (elapsed < fadeOutDuration)
        {
            fadeImage.color = Color.Lerp(targetColor, initialColor, elapsed / fadeOutDuration);
            elapsed += Time.deltaTime;
            yield return null;
        }
        fadeImage.color = initialColor;
    }
}
