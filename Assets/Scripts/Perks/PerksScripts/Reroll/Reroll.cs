using UnityEngine;
using TMPro;

[CreateAssetMenu(fileName = "Reroll", menuName = "ScriptableObjects/Perks/Reroll")]

public class Reroll : PerkBasis
{
    public override void Obtain() => FindObjectOfType<RerollButton>().Activate();
    public override void Remove() => FindObjectOfType<RerollButton>().Dectivate();

    public override void SetDescription(TextMeshProUGUI textField) 
    {
        textField.text = "Allows you to reroll once per selection";
    }
}
