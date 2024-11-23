using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CurrentUpgradesUI : MonoBehaviour
{
    private UpgradeHandler handler;
    [SerializeField] GameObject uiUpgrade;
    [SerializeField] Transform layoutGroup;
    List<string> CurrentUpgrades = new();
    void Awake()
    {
        handler = GameObject.FindWithTag("Player").GetComponent<UpgradeHandler>();
        handler.addUpgradeEvent += AddUpgrade;
    }

    void AddUpgrade(Upgrade upgradeGotten)
    {
        int amount = handler.GetUpgradeOfType(upgradeGotten).amount;
        GameObject uiElement;
        if (amount == 0)
        {
            CurrentUpgrades.Add(upgradeGotten.name);
            uiElement = Instantiate(uiUpgrade, layoutGroup);
            uiElement.GetComponent<Image>().sprite = upgradeGotten.icon;
            TextMeshProUGUI newText = uiElement.GetComponentInChildren<TextMeshProUGUI>();
            newText.color = Color.clear;
        }
        else
        {
            uiElement = layoutGroup.GetChild(CurrentUpgrades.IndexOf(upgradeGotten.name)).gameObject;
            TextMeshProUGUI newText = uiElement.GetComponentInChildren<TextMeshProUGUI>();
            newText.text = (amount + 1).ToString();
            newText.transform.DOPunchPosition(new Vector3(0,15,0), 0.25f);
            newText.color = Color.white;
        }
        uiElement.transform.DOPunchScale(Vector3.one * 0.3f, 0.25f);
    }
}
