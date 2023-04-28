using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class UpgradablePerkInventoryPanel : PerkInventoryPanel
{
    [SerializeField] private TextMeshProUGUI _descriptionTextField;

    [Header("LevelSettings")]

    [SerializeField] private TextMeshProUGUI _levelTextField;

    [SerializeField] private Image _leveSliderImage;

    public override void Setup()
    {   
        SetNameAndIcon(GetPerkData());

        SetShine(GetPerkData());

        UpdatePanel();
    }

    public void UpdatePanel()
    {
        _levelTextField.text = GetPerkData().Perk.GetLevel().ToString();

        _leveSliderImage.fillAmount = GetPerkData().Perk.GetLevel() / 3f;

        GetPerkData().Perk.SetDescription(_descriptionTextField);
    }

    private void OnEnable() => UpdatePanel();
}
