using UnityEngine;
using TMPro;

[CreateAssetMenu(fileName = "HomingBullets", menuName = "ScriptableObjects/Perks/HomingBullets")]

public sealed class HomingBulletsPerk : PerkBasis
{
    public override void Obtain() => Main.combatStats.singleHomingSpeed += 120f;
    public override void Remove() => Main.combatStats.singleHomingSpeed = 0f;

    protected override void Upgrade() => Main.combatStats.singleHomingSpeed += 120f;

    public override void SetDescription(TextMeshProUGUI textField) 
    {
        textField.SetText(
            $"{"SINGLE".AddColor(Main.colorManager.GetSingleColor())}" +
            " projectiles home towards enemies");
        
    }

    public override void SetUpgradeDescription(TextMeshProUGUI textField)
    {
        string descripion = "";
        
        switch (GetLevel())
        {
            case 1: descripion = " projectiles home faster towards enemies"; break;
            case 2: descripion = " projectiles home even faster towards enemies"; break; 
        }

        textField.SetText(
            $"{"SINGLE".AddColor(Main.colorManager.GetSingleColor())}" + descripion);
    }
}
