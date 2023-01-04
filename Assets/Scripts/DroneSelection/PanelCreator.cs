using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class PanelCreator : MonoBehaviour
{
    [Header("PickUpPanelSettings")]
    [SerializeField] private GameObject _dronePickUpPanelPrefab;

    [Header("UpgradeDronePanelSettings")]
    [Range(0f, 100f)] [SerializeField] private float _droneUpgradePanelAppearanceChanse;
    [SerializeField] private GameObject _droneUpgradePanelPrefab;

    [Header("PerkPickUpPanelSettings")]
    [Range(0f, 100f)] [SerializeField] private float _perkPickUpPanelPanelAppearanceChanse;
    [SerializeField] private GameObject _perkPickUpPanelPrefab;

    [Header("PerkUpgradePanelSettings")]
    [Range(0f, 100f)] [SerializeField] private float _perkUpgradePanelPanelAppearanceChanse;
    [SerializeField] private GameObject _perkUpgradePanelPrefab;

    [Header("TransmutationPanel")]
    [Range(0f, 100f)] [SerializeField] private float _transmutationPanelAppearanceChanse;
    [SerializeField] private GameObject _transmutationPanelPrefab;

    [Header("DuplicationPanelSettings")]
    [Range(0f, 100f)] [SerializeField] private float _duplicationPanelPanelAppearanceChanse;
    [SerializeField] private GameObject _duplicationPanelPrefab;

    
    private List<DroneBasis> _playerDrones;
    private List<DroneBasis> _notObtainedDrones;

    private int _upgradableDronesLeft;

    public void UpdateSelectionStats()
    {
        _upgradableDronesLeft = Main.droneContainer.GetAmountOfUpgradableDrones();
        _playerDrones = GetPlayersDrones();
        _notObtainedDrones = GetNotObtainedDrones(_playerDrones);
    }

    public GameObject InstantiatePanel(Transform panelParentObject)
    {
        PanelType panelType = GetRandomPanelType();

        if (panelType == PanelType.DroneUpgrade || panelType == PanelType.Transmutation || panelType == PanelType.Duplication) _upgradableDronesLeft--;

        GameObject panel = InstantiatePanelGameObject(panelType, panelParentObject);

        SetupPanel(panelType, panel);

        return panel;
    }

    private void SetupPanel(PanelType panelType, GameObject selectionPanel)
    {
        switch (panelType)
        {
            case PanelType.Duplication: SetupDuplicationPanel(selectionPanel); break;
            case PanelType.Transmutation: SetupTransmutationPanel(selectionPanel); break;
            case PanelType.PerkPickUp: SetupPerkPickUpPanel(selectionPanel); break;
            case PanelType.PerkUpgrade: SetupPerkUpgradePanel(selectionPanel); break;
            case PanelType.DroneUpgrade: SetupDroneUpgradePanel(selectionPanel); break;
            case PanelType.DronePickUp: SetupDronePickUpPanel(selectionPanel); break;
        }
    }

    private List<DroneBasis> GetNotObtainedDrones(List<DroneBasis> playerDrones)
    {
        List<DroneBasis> notObtainedDrones = Main.s_selectionStorage.GetAllDrones(); 
        
        for (int x = 0; x < notObtainedDrones.Count; x++)
        {
            for (int y = 0; y < playerDrones.Count; y++)
            {
                if (notObtainedDrones[x].GetDroneData().Name == playerDrones[y].GetDroneData().Name)
                {
                    notObtainedDrones.RemoveAt(x);

                    if (playerDrones.Count <= x) return notObtainedDrones; 
                }
            }
        }

        return notObtainedDrones;
    }

    private List<DroneBasis> GetPlayersDrones()
    {
        List<DroneBasis> playerDrones = new List<DroneBasis>(Main.droneContainer.GetObtainedDrones());

        for (int x = 0; x < playerDrones.Count; x++)
        {
            for (int y = x + 1; y < playerDrones.Count; y++)
            {
                if (playerDrones[x].GetDroneData() == playerDrones[y].GetDroneData()) 
                {
                    playerDrones.RemoveAt(y); 

                    y--;

                    _upgradableDronesLeft--;
                }
            }
        }
         
        return playerDrones;
    }

    private PanelType GetRandomPanelType()
    {
        // Perk is default because perks dont have any condition except chanse
        PanelType randomType = PanelType.PerkPickUp;

        if (DuplicationPanelCanAppear() == true) randomType = PanelType.Duplication;
        else if (TransmutationPanelCanAppear() == true) randomType = PanelType.Transmutation;
        else if (PerkPickUpPanelCanAppear() == true) randomType = PanelType.PerkPickUp;
        else if (PerkUpgradePanelCanAppear() == true) randomType = PanelType.PerkUpgrade;
        else if (UpgradeDronePanelCanAppear() == true) randomType = PanelType.DroneUpgrade;
        else if (PickUpDronePanelCanAppear() == true) randomType = PanelType.DronePickUp;
        
        return randomType;
    }
    private GameObject InstantiatePanelGameObject(PanelType panelType, Transform parentTransform)
    {
        int randomNonObtainedDroneIndex = Random.Range(0, _notObtainedDrones.Count);

        GameObject panelToInstantiate = null;

        switch (panelType)
        {
            case PanelType.Duplication: panelToInstantiate = _duplicationPanelPrefab; break;
            case PanelType.Transmutation: panelToInstantiate = _transmutationPanelPrefab; break;
            case PanelType.PerkPickUp: panelToInstantiate = _perkPickUpPanelPrefab; break;
            case PanelType.PerkUpgrade: panelToInstantiate = _perkUpgradePanelPrefab; break;
            case PanelType.DroneUpgrade: panelToInstantiate = _droneUpgradePanelPrefab; break;
            case PanelType.DronePickUp: panelToInstantiate = _dronePickUpPanelPrefab; break;
        }

        GameObject panel = Instantiate(panelToInstantiate, parentTransform.transform.position, Quaternion.identity, parentTransform);

        return panel;
    }

    private void SetupDuplicationPanel(GameObject duplicationPanel)
    {
        int randomPlayerDroneIndex = Random.Range(0, _playerDrones.Count);

        duplicationPanel.GetComponent<DuplicationPanel>().Setup(_playerDrones[randomPlayerDroneIndex]);

        _playerDrones.RemoveAt(randomPlayerDroneIndex);
    }
    private void SetupTransmutationPanel(GameObject transmutationPanel)
    {
        DroneBasis upgradableDrone = null;

        for (int i = 0; i < _playerDrones.Count; i++)
        {
            if (_playerDrones[i].GetLevel() < 5)
            {
                upgradableDrone = _playerDrones[i];
            }
        }

        int randomNotObtainedDroneIndex = Random.Range(0, _notObtainedDrones.Count);

        transmutationPanel.GetComponent<TransmutationPanel>().Setup(upgradableDrone, _notObtainedDrones[randomNotObtainedDroneIndex]);

        // remove drones from list so they wont appear in other panels
        for (int i = 0; i < _playerDrones.Count; i++)
        {
            if (_playerDrones[i].GetDroneData() == upgradableDrone.GetDroneData()) 
            {
                _playerDrones.RemoveAt(i); break;
            }
        }
        _notObtainedDrones.RemoveAt(randomNotObtainedDroneIndex);
    }
    private void SetupPerkUpgradePanel(GameObject perkPanel)
    {
        perkPanel.GetComponent<PerkUpgradePanel>().Setup(Main.s_selectionStorage.GetRandomUpgradablePerk());
    }
    private void SetupPerkPickUpPanel(GameObject perkPanel)
    {
        perkPanel.GetComponent<PerkPickUpPanel>().Setup(Main.s_selectionStorage.GetRandomPerk());
    }
    private void SetupDroneUpgradePanel(GameObject upgradePanel)
    {
        DroneBasis upgradableDrone = null;

        for (int i = 0; i < _playerDrones.Count; i++)
        {
            if (_playerDrones[i].GetLevel() != 5)
            {
                upgradableDrone = _playerDrones[i];
            }
        }
        
        upgradePanel.GetComponent<DroneUpgradePanel>().Setup(upgradableDrone);

        // remove drone from list so they wont appear in other panels
        for (int i = 0; i < _playerDrones.Count; i++)
        {
            if (_playerDrones[i].GetDroneData() == upgradableDrone.GetDroneData()) 
            {
                _playerDrones.RemoveAt(i); break;
            }
        }
    }
    private void SetupDronePickUpPanel(GameObject pickUpPanel)
    {
        int randomNonObtainedDroneIndex = Random.Range(0, _notObtainedDrones.Count);

        pickUpPanel.GetComponent<DronePickUpPanel>().Setup(_notObtainedDrones[randomNonObtainedDroneIndex]);

        // remove drone from list so they wont appear in other panels
        _notObtainedDrones.RemoveAt(randomNonObtainedDroneIndex);
    }




   



    public bool PickUpDronePanelCanAppear() => (Main.droneContainer.HasFreeCells() == true);
    public bool UpgradeDronePanelCanAppear() 
    {
        return (_upgradableDronesLeft > 0 && Random.Range(0, 100) < _droneUpgradePanelAppearanceChanse);
    } 
    public bool TransmutationPanelCanAppear() 
    {
        return (Main.droneContainer.HasFreeCells() == false && _upgradableDronesLeft > 0 && Random.Range(0, 100) < _transmutationPanelAppearanceChanse);
    }
    public bool DuplicationPanelCanAppear()
    {
        return (Main.droneContainer.HasFreeCells() == true && _playerDrones.Count > 0 && Random.Range(0, 100) < _duplicationPanelPanelAppearanceChanse);
    }
    public bool PerkPickUpPanelCanAppear() => (Random.Range(0, 100) < _perkPickUpPanelPanelAppearanceChanse);
    public bool PerkUpgradePanelCanAppear() => (Main.s_selectionStorage.HasUpgradablePerks() == true && Random.Range(0, 100) < _perkUpgradePanelPanelAppearanceChanse);



    private enum PanelType
    {
        Duplication,
        Transmutation,
        PerkPickUp,
        PerkUpgrade,
        DroneUpgrade,
        DronePickUp
    }
}
