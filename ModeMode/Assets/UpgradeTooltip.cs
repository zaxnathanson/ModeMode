using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeTooltip : MonoBehaviour
{
    [SerializeField] Image icon;
    [SerializeField] Image background;
    Color initialBackgroundColor;
    [SerializeField] TextMeshProUGUI text;
    private UpgradeHandler handler;
    bool canShowNextUpgrade = true;

    List<Upgrade> UpgradeBacklog = new();
    void Awake()
    {
        handler = GameObject.FindWithTag("Player").GetComponent<UpgradeHandler>();
        handler.addUpgradeEvent += AddToBacklog;
        icon.color = Color.clear; text.color = Color.clear;
        initialBackgroundColor = background.color;
        background.color = Color.clear;
    }

    public void AddToBacklog(Upgrade newUpgrade)
    {
        UpgradeBacklog.Add(newUpgrade);
        Debug.Log("Lol2");
    }

    private void Update()
    {
        if (canShowNextUpgrade && UpgradeBacklog.Count >0)
        {
            StartCoroutine(SetNewUpgrade(UpgradeBacklog[0]));
            UpgradeBacklog.RemoveAt(0);
            canShowNextUpgrade = false;
        }
    }

    public IEnumerator SetNewUpgrade(Upgrade newUpgrade)
    {
        Color clear = new Color(1,1,1,0);
        icon.color = Color.white;
        icon.rectTransform.DOPunchScale(Vector3.one * 0.3f, 0.2f);
        icon.sprite = newUpgrade.enemySprite;
        icon.SetNativeSize();

        text.text = newUpgrade.toolTip;
        text.color = new Color(icon.color.r, icon.color.g, icon.color.b, 1);

        background.color = initialBackgroundColor;

        yield return new WaitForSeconds(4);

        float elapsed = 0;
        while (elapsed < 0.3f)
        {
            icon.color = Color.Lerp(Color.white, clear, elapsed);
            text.color = Color.Lerp(Color.white, clear, elapsed);
            background.color = Color.Lerp(initialBackgroundColor, clear, elapsed);
            elapsed += Time.deltaTime;
            yield return null;
        }
        icon.color = Color.clear;
        text.color = Color.clear;
        background.color = Color.clear;
        canShowNextUpgrade = true;
    }
}
