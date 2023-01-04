using UnityEngine;
using TMPro;

[CreateAssetMenu(fileName = "RocketDash", menuName = "ScriptableObjects/Perks/RocketDash")]

public sealed class RocketDashPerk : PerkBasis
{
    public override void Obtain()
    {
        FindObjectOfType<MisslesAbility>().ObtainAbility();

        Upgrade();
    }

    public override void Remove()
    {
        FindObjectOfType<MisslesAbility>().RemoveAbility();
    }

    protected override void Upgrade()
    {
        MisslesAbility ability = FindObjectOfType<MisslesAbility>();
        
        ability.SetMissleAmount(GetLevel() + 1);

        ability.SetMissleDamage(GetLevel() * 2f);
    }

    public override void SetDescription(TextMeshProUGUI textField) 
    {
        string descripion = "";

        switch (GetLevel())
        {
            case 1: descripion = "Dashing creates 4 homing missles dealing "; break;
            case 2: descripion = "Dashing creates 6 homing missles dealing "; break;
            case 3: descripion = "Dashing creates 8 homing missles dealing "; break; 
        }

        textField.SetText(
            descripion +
            $"{"SINGLE".AddColor(Main.colorManager.GetSingleColor())}" +
            " damage");
        
    }

    public override void SetUpgradeDescription(TextMeshProUGUI textField)
    {
        string descripion = "";

        switch (GetLevel())
        {
            case 1: descripion = "Dashing creates 4 > 6 homing missles dealing more"; break;
            case 2: descripion = "Dashing creates 6 > 8 homing missles dealing more"; break; 
        }

        textField.SetText(
            descripion +
            $"{"SINGLE".AddColor(Main.colorManager.GetSingleColor())}" +
            " damage");
    }
}
