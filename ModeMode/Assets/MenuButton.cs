using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    [SerializeField] string targetScene;
    [SerializeField] Button buttonRef;
    [SerializeField] RectTransform rectTransform;
    [SerializeField] float scaleAmount;
    [SerializeField] float punchAmount;
    [SerializeField] AudioPlayer audioPlayer;
    [SerializeField] float scaleSpeed;
    [SerializeField] float punchSpeed;
    bool hasBeenClicked = false;

    public void OnPointerExit(PointerEventData eventData)
    {
        rectTransform.DOScale(Vector2.one, scaleSpeed).SetUpdate(true);
    }
    // Update is called once per frame
    public void OnPointerEnter(PointerEventData eventData)
    {
        rectTransform.DOComplete();
        rectTransform.DOScale(scaleAmount, scaleSpeed).SetUpdate(true);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (!hasBeenClicked)
        {
            hasBeenClicked = true;
            StartCoroutine(ButtonClick());
        }

    }

    IEnumerator ButtonClick()
    {
        audioPlayer.PlayAudio();
        rectTransform.DOComplete();
        Tween clickTween = rectTransform.DOPunchScale(Vector3.one * punchAmount, punchSpeed).SetUpdate(true);
        yield return clickTween.WaitForCompletion();

        if (targetScene != "")
        {
            Time.timeScale = 1;
            SceneManager.LoadScene(targetScene);
        }
        hasBeenClicked = false;
    }

}
