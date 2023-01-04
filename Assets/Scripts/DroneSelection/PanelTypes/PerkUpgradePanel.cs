using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class PerkUpgradePanel : SelectionPanel
{
    [Header("DroneInformationFields")]
    [SerializeField] private TextMeshProUGUI perkNameText;

    [SerializeField] private TextMeshProUGUI perkDescritionText;

    [Header("LevelUpDescription")]
    [SerializeField] private TextMeshProUGUI previousLevel;

    [SerializeField] private TextMeshProUGUI nextLevel;

    [SerializeField] private Image previousLevelSlider;

    [SerializeField] private Image nextLevelSlider;

    [Header("VisualLinks")]

    [SerializeField] private Image shineImage;

    [SerializeField] private ParticleSystemRenderer partSystem;

    [SerializeField] private Image icon;

    private PerkData _currentPerkData;

    private bool _upgraded;

    public void Setup(PerkData newPerkData)
    {
        _currentPerkData = newPerkData;
        
        _currentPerkData.Perk.SetUpgradeDescription(perkDescritionText);

        DisplayUpgrade(_currentPerkData.Perk.GetLevel()); 

        perkNameText.text = _currentPerkData.PerkName;

        icon.sprite = _currentPerkData.Icon;

        SetupVisual();
    }

    private void DisplayUpgrade(int level)
    {
        previousLevel.text = level.ToString();

        nextLevel.text = (level + 1).ToString();

        previousLevelSlider.fillAmount = level / 3f;

        nextLevelSlider.fillAmount = (level + 1) / 3f;
    }

    private void SetupVisual()
    {
        if (_currentPerkData.Type == DroneType.None) Destroy(shineImage);
        else
        {
            shineImage.material = Main.droneSelector.gameObject.GetComponent<UIMaterialFactory>().GetPanelMaterial(_currentPerkData.Type, 0);

            ///partSystem.material = Main.droneSelector.gameObject.GetComponent<UIMaterialFactory>().GetParticleMaterial((int)(drone.GetDroneType()));
        }
    }
    protected override void Select()
    {
        UpgradePerk();    
        Main.droneSelector.ClosePanels();
        Main.arenaManager.DronePickedUp();   
    }

    private void UpgradePerk()
    {
        _currentPerkData.Perk.UpgradePerk();

        _upgraded = true;
    }

    public override void Close()
    {
        if (_upgraded == false) Main.s_selectionStorage.ReturnUpgradablePerk(_currentPerkData);
        else if (_currentPerkData.Perk.GetLevel() < 3) Main.s_selectionStorage.ReturnUpgradablePerk(_currentPerkData);
        
        Destroy(gameObject);
    }
}
