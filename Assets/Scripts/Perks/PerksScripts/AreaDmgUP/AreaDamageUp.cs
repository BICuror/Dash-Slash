using UnityEngine;
using TMPro;

[CreateAssetMenu(fileName = "AreaDamageUp", menuName = "ScriptableObjects/Perks/AreaDamageUp")]

public sealed class AreaDamageUp : PerkBasis
{
    public override void Obtain() => Main.combatStats.areaDamage += 0.5f;
    public override void Remove() => Main.combatStats.areaDamage -= 0.5f;

    public override void SetDescription(TextMeshProUGUI textField) 
    {
        textField.SetText($"{"AREA".AddColor(Main.colorManager.GetAreaColor())}" + " damage increased by 50%");
    }
}
