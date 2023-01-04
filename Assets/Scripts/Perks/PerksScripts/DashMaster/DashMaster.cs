using UnityEngine;
using TMPro;

[CreateAssetMenu(fileName = "DashMaster", menuName = "ScriptableObjects/Perks/DashMaster")]

public sealed class DashMaster : PerkBasis
{
    public override void Obtain()
    {
        Main.playerAbility.SetMaxAbilityCharge(1);
        Main.playerAbility.IncreaseRechargeSpeed(0.005f);
    }

    public override void Remove()
    {
        Main.playerAbility.SetMaxAbilityCharge(-1);
        Main.playerAbility.IncreaseRechargeSpeed(-0.005f);
    }

    public override void SetDescription(TextMeshProUGUI textField) 
    {
        textField.text = "Additional ability charge and faster dash recharge";
    }
}
