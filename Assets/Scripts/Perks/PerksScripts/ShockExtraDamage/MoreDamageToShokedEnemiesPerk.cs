using UnityEngine;
using TMPro;

[CreateAssetMenu(fileName = "ExtraShockedDamage", menuName = "ScriptableObjects/Perks/ExtraShockedDamage")]

public sealed class MoreDamageToShokedEnemiesPerk : PerkBasis
{
    public override void Obtain()
    {
        Main.combatStats.additionalDamageToShokedEnemies += 1.3f;
    }

    public override void Remove()
    {
        Main.combatStats.additionalDamageToShokedEnemies -= 1.3f;
    }

    public override void SetDescription(TextMeshProUGUI textField) 
    {
        textField.SetText(
            $"{"SHOKED".AddColor(Main.colorManager.GetAreaColor())}" + 
            " enemies take 130% more damage");
    }
}
