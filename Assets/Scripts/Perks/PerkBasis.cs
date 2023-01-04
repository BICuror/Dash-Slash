using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PerkBasis : ScriptableObject
{
    private int level = 1;

    public virtual void Obtain() {}

    public virtual void Remove() {}

    public void SetToDefault() => level = 1;
    

    public int GetLevel() => level;

    public void UpgradePerk()
    {
        level++;

        Upgrade();
    }

    protected virtual void Upgrade() {}

    public virtual bool ShouldBeReturned() => true;

    // for selection
    public virtual void SetDescription(TextMeshProUGUI textField) {}
    // for perk inventory
    public virtual void SetUpgradeDescription(TextMeshProUGUI textField) {}
}
