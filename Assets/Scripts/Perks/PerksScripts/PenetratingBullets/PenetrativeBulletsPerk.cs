using UnityEngine;
using TMPro;

[CreateAssetMenu(fileName = "PenetrativeBullets", menuName = "ScriptableObjects/Perks/PenetrativeBullets")]

public sealed class PenetrativeBulletsPerk : PerkBasis
{
    public override void Obtain() => Main.combatStats.singleArePenetrating = true;
    public override void Remove() => Main.combatStats.singleArePenetrating = false;
    
    public override void SetDescription(TextMeshProUGUI textField) 
    {
        textField.SetText(
            $"{"SINGLE".AddColor(Main.colorManager.GetSingleColor())}" +
            " projectiles are penetrating");
    }
}
