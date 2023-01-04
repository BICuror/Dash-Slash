using UnityEngine;
using TMPro;

[CreateAssetMenu(fileName = "GravitonDash", menuName = "ScriptableObjects/Perks/GravitonDash")]

public sealed class GravitonDash : PerkBasis
{
    public override void Obtain() => FindObjectOfType<GravitoneDashAbility>().ObtainAbility();
    public override void Remove() => FindObjectOfType<GravitoneDashAbility>().RemoveAbility();
    
    public override void SetDescription(TextMeshProUGUI textField) 
    {
        textField.text = "Create gravitone upon dashing";
    }
}
