using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialButton : MonoBehaviour
{
    [SerializeField] GameObject tutorialGameobject;


    public void ShowTutorial()
    {
        if (tutorialGameobject.activeInHierarchy)
        {
            tutorialGameobject.SetActive(false);
        }
        else
        {
            tutorialGameobject.SetActive(true);
        }
    }
}
