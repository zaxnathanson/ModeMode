using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeContainer
{
    public Upgrade upgrade;
    public int amount;
}

public class UpgradeHandler : MonoBehaviour
{
    [SerializeField]
    public List<UpgradeContainer> upgrades = new();

    public void AddUpgrade(Upgrade upgradeToAdd)
    {
        UpgradeContainer newUpgrade = new();
        newUpgrade.upgrade = upgradeToAdd;
        newUpgrade.amount = 0;
        upgrades.Add(newUpgrade);
        newUpgrade.upgrade.Setup(this);
    }
    private void Update()
    {
        foreach (UpgradeContainer upgradeContainer in upgrades)
        {
            upgradeContainer.upgrade.CallUpdate(Time.deltaTime);
        }


    }

    public UpgradeContainer GetUpgradeOfType(Upgrade upgrade)
    {
        foreach (UpgradeContainer u in upgrades)
        {
            if (u.upgrade == upgrade)
            {
                return u;
            }
        }
        return null;
    }

    public bool CheckContainerForType(Upgrade upgrade)
    {
        foreach(UpgradeContainer u in upgrades)
        {
            if (u.upgrade == upgrade)
            {
                return true;
            }
        }

        return false;
    }
}
