using TMPro;
using UnityEngine;

[CreateAssetMenu(fileName = "SingleDamageUp", menuName = "ScriptableObjects/Perks/SingleDamageUp")]

public class SingleDamageUp : PerkBasis
{
    public override void Obtain()
    {
        Main.combatStats.singleDamage += 0.5f;
    }

    public override void Remove()
    {
        Main.combatStats.singleDamage -= 0.5f;
    }

    public override void SetDescription(TextMeshProUGUI textField) 
    {
        textField.SetText($"{" SINGLE".AddColor(Main.colorManager.GetSingleColor())}" + " damage increased by 50%");
    }
}
