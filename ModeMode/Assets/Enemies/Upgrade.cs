using DamageNumbersPro;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class Upgrade : ScriptableObject
{   
    public Stats statsRef;
    public DamageNumber effectText;
    [TextArea]
    public string toolTip;
    public Sprite enemySprite;
    public Sprite icon;


    [HideInInspector]protected UpgradeHandler upgradeHandler;

    public virtual void CallUpdate(float deltaTime) { } 

    public virtual void Setup(UpgradeHandler ctx)
    {
        upgradeHandler = ctx;
        statsRef = upgradeHandler.gameObject.GetComponent<Stats>();
    }
}


