using UnityEngine;
using TMPro;

public class SinglePerkInventoryPanel : PerkInventoryPanel
{
    [SerializeField] private TextMeshProUGUI _descriptionTextField;

    public override void Setup()
    {
        SetNameAndIcon(GetPerkData());

        SetShine(GetPerkData());

        GetPerkData().Perk.SetDescription(_descriptionTextField);
    }
}
