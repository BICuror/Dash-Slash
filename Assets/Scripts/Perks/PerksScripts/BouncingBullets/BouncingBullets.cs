using UnityEngine;
using TMPro;

[CreateAssetMenu(fileName = "BouncingBullets", menuName = "ScriptableObjects/Perks/BouncingBullets")]

public sealed class BouncingBullets : PerkBasis
{
    public override void Obtain() => Main.combatStats.singleBounceAmount++;
    protected override void Upgrade() => Main.combatStats.singleBounceAmount++;
    
    public void Remove() => Main.combatStats.singleBounceAmount = 0;

    public override void SetDescription(TextMeshProUGUI textField) 
    {
        string descripion = "";

        switch (GetLevel())
        {
            case 1: descripion = " projectiles bounce off enemies and walls 1 time"; break;
            case 2: descripion = " projectiles bounce off enemies and walls 2 times"; break;
            case 3: descripion = " projectiles bounce off enemies and walls 3 times"; break; 
        }

        textField.SetText(
            $"{"SINGLE".AddColor(Main.colorManager.GetSingleColor())}" +
            $"{descripion.AddColor(Color.white)}");
    }

    public override void SetUpgradeDescription(TextMeshProUGUI textField)
    {
        string descripion = "";

        switch (GetLevel())
        {
            case 1: descripion = " projectiles bounce off enemies and walls 1 > 2 times"; break;
            case 2: descripion = " projectiles bounce off enemies and walls 2 > 3 times"; break; 
        }

        textField.SetText(
            $"{"SINGLE".AddColor(Main.colorManager.GetSingleColor())}" +
            $"{descripion.AddColor(Color.white)}");
            
    }
}
