using UnityEngine;
using TMPro;

[CreateAssetMenu(fileName = "LaserDash", menuName = "ScriptableObjects/Perks/LaserDash")]

public sealed class LaserDashPerk : PerkBasis
{
    public override void Obtain() 
    {
        FindObjectOfType<LaserDashAbility>().ObtainAbility();

        UpdateStats();
    } 
    public override void Remove() => FindObjectOfType<LaserDashAbility>().RemoveAbility();

    protected override void Upgrade() => UpdateStats();

    private void UpdateStats()
    {
        LaserDashAbility ability = FindObjectOfType<LaserDashAbility>();
        
        ability.SetLaserDamage(GetLevel() * 2f);
        ability.SetLaserOffset(GetLevel() * 1.2f);
    }

    public override void SetDescription(TextMeshProUGUI textField) 
    {
        string descripion = "";

        switch (GetLevel())
        {
            case 1: descripion = "Dashing creates laser which deals a little "; break;
            case 2: descripion = "Dashing creates laser which deals a OK "; break;
            case 3: descripion = "Dashing creates laser which deals a good "; break; 
        }

        textField.SetText(
            descripion +
            $"{"AREA".AddColor(Main.colorManager.GetAreaColor())}" +
            " damage");
    }

    public override void SetUpgradeDescription(TextMeshProUGUI textField)
    {
        string descripion = "";

        switch (GetLevel())
        {
            case 1: descripion = "Dashing creates laser with bigger offset which deals more "; break;
            case 2: descripion = "Dashing creates laser with bigger offset which deals even more"; break; 
        }

        textField.SetText(
            descripion +
            $"{"AREA".AddColor(Main.colorManager.GetAreaColor())}" +
            " damage");
    }
}
