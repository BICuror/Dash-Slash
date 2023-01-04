using UnityEngine;
using TMPro;

[CreateAssetMenu(fileName = "ExtraFireDamage", menuName = "ScriptableObjects/Perks/ExtraFireDamage")]

public sealed class MoreDamageToEnemiesOnFire : PerkBasis
{
    public override void Obtain() => Main.combatStats.additionalDamageToOnFireEnemies += 1.3f;
    public override void Remove() => Main.combatStats.additionalDamageToOnFireEnemies -= 1.3f;
    
    public override void SetDescription(TextMeshProUGUI textField) 
    {
        textField.SetText(
            "Enemies on " +
            $"{"FIRE".AddColor(Main.colorManager.GetDefenceColor())}"
            + " take 130% more damage");
    }
}
