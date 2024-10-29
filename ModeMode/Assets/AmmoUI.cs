using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoUI : MonoBehaviour
{
    [SerializeField] GameObject bulletUIObject;
    Stats statsRef;
    int uiAmmo = 0;
    void Start()
    {
        statsRef = GameObject.FindWithTag("Player").GetComponent<Stats>();

    }

    void SetAmmoUI()
    {
        while (transform.childCount - 2 < statsRef.shootingStats.ammo)
        {
            Transform newChild = Instantiate(bulletUIObject, transform).transform;
            newChild.SetSiblingIndex(newChild.GetSiblingIndex() - 1);
        }
        for (int i = transform.childCount - 2; i > statsRef.shootingStats.ammo; i--)
        {
            Debug.Log("Destroy");
            Destroy(transform.GetChild(transform.childCount - i - 1).gameObject);
        }
    }

    void Update()
    {
        if (uiAmmo != statsRef.shootingStats.ammo)
        {
            SetAmmoUI();
        }
        uiAmmo = statsRef.shootingStats.ammo;
    }
}
