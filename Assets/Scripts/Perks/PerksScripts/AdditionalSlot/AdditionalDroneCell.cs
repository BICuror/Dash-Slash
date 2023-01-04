using UnityEngine;
using TMPro;

[CreateAssetMenu(fileName = "AdditionalDroneCell", menuName = "ScriptableObjects/Perks/AdditionalDroneCell")]

public sealed class AdditionalDroneCell : PerkBasis
{
    public override void Obtain() => Main.droneContainer.AddDroneCell();
    public override void Remove() => Main.droneContainer.RemoveDroneCell();

    public override void SetDescription(TextMeshProUGUI textField) 
    {
        textField.text = "You gain additional drone cell";
    }

    public override bool ShouldBeReturned()
    {
        return (Main.droneContainer.GetAmountOfCells() < 8);    
    }
}
