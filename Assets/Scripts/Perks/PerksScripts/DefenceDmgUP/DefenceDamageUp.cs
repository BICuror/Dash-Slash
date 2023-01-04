using TMPro;
using UnityEngine;

[CreateAssetMenu(fileName = "DefenceDamageUp", menuName = "ScriptableObjects/Perks/DefenceDamageUp")]

public sealed class DefenceDamageUp : PerkBasis
{
    public override void Obtain()
    {
        Main.combatStats.defenceDamage += 0.5f;
    }

    public override void Remove()
    {
        Main.combatStats.defenceDamage -= 0.5f;
    }

    public override void SetDescription(TextMeshProUGUI textField) 
    {
        textField.SetText($"{"DEFENCE".AddColor(Main.colorManager.GetDefenceColor())}" + " damage increased by 50%");
    }

    public override bool ShouldBeReturned() {return true;}
}
