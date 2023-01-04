using UnityEngine;
using TMPro;

[CreateAssetMenu(fileName = "SpeedUp", menuName = "ScriptableObjects/Perks/SpeedUp")]

public sealed class SpeedUp : PerkBasis
{
    public override void Obtain() => UpdateSpeed();
    public override void Remove() => Main.playerMovment.SetSpeedMultiplier(1);
    
    protected override void Upgrade() => UpdateSpeed();

    private void UpdateSpeed()
    {
        Main.playerMovment.SetSpeedMultiplier((GetLevel() * 0.3f) + 1);
    }

    public override void SetDescription(TextMeshProUGUI textField) 
    {
        string descripion = "";

        switch (GetLevel())
        {
            case 1: descripion = "Speeds you up by 1.33"; break;
            case 2: descripion = "Speeds you up by 1.66"; break;
            case 3: descripion = "Speeds you up by 2"; break; 
        }

        textField.text = descripion;
    }

    public override void SetUpgradeDescription(TextMeshProUGUI textField)
    {
        string descripion = "";

        switch (GetLevel())
        {
            case 1: descripion = "Speeds you up by 1.33 > 1.66"; break;
            case 2: descripion = "Speeds you up by 1.66 > 2"; break; 
        }

        textField.text = descripion;
    }
}
