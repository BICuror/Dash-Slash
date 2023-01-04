using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class PerkPickUpPanel : SelectionPanel
{
    [Header("DroneInformationFields")]
    [SerializeField] private TextMeshProUGUI perkNameText;

    [SerializeField] private TextMeshProUGUI perkDescritionText;

    [SerializeField] private GameObject pickupDesction;

    [Header("VisualLinks")]

    [SerializeField] private Image shineImage;

    [SerializeField] private ParticleSystemRenderer partSystem;

    [SerializeField] private Image icon;

    private PerkData _currentPerkData;

    private bool obtained;

    public void Setup(PerkData newPerkData)
    {
        _currentPerkData = newPerkData;
        
        _currentPerkData.Perk.SetDescription(perkDescritionText);

        perkNameText.text = _currentPerkData.PerkName;

        icon.sprite = _currentPerkData.Icon;

        SetupVisual();
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
        PerkObtained();    
        Main.droneSelector.ClosePanels();
        Main.arenaManager.DronePickedUp();   
    }

    private void PerkObtained()
    {
        Main.perkInventory.AddPerkInventoryPanel(_currentPerkData);

        _currentPerkData.Perk.Obtain();

        obtained = true;
    }

    public override void Close()
    {
        if (obtained == false) 
        {
            Main.s_selectionStorage.ReturnPerk(_currentPerkData);
        }
        else if (_currentPerkData.IsMultiplyable == true && _currentPerkData.Perk.ShouldBeReturned() == true) 
        {
            Main.s_selectionStorage.ReturnPerk(_currentPerkData);
        }
        else if (_currentPerkData.IsUpgradable == true)
        {
            Main.s_selectionStorage.ReturnUpgradablePerk(_currentPerkData);
        } 

        Destroy(gameObject);
    }
}
