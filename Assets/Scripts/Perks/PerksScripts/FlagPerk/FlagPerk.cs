using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FlagPerk : PerkBasis
{
    private GameObject _spawner;

    public override void Obtain()
    {
        //_spawner = Instantiate(perkData.perkObject, new Vector3(0f, 0f, 0f), Quaternion.identity);

        //_spawner.GetComponent<FlagSpawwner>().SetDamageIncrease(1f);
    }

    protected override void Upgrade()
    {
        _spawner.GetComponent<FlagSpawwner>().SetDamageIncrease(1f);
    }

    public override void SetDescription(TextMeshProUGUI textField) 
    {
        string descripion = "";

        switch (GetLevel())
        {
            case 1: descripion = "Sometimes zones appear by standing in them u increase all your dmg by 100%"; break;
            case 2: descripion = "Sometimes zones appear by standing in them u increase all your dmg by 200%"; break;
            case 3: descripion = "Sometimes zones appear by standing in them u increase all your dmg by 300%"; break; 
        }

        textField.text = descripion;
    }

    public override void SetUpgradeDescription(TextMeshProUGUI textField)
    {
        string descripion = "";

        switch (GetLevel())
        {
            case 0: descripion = "Sometimes zones appear by standing in them u increase all your dmg by 100%"; break;
            case 1: descripion = "Sometimes zones appear by standing in them u increase all your dmg by 100% > 200%"; break;
            case 2: descripion = "Sometimes zones appear by standing in them u increase all your dmg by 200% > 300%"; break;
        }

        textField.text = descripion;
    }
}
