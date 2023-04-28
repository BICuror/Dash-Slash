using UnityEngine;
using TMPro;
using UnityEngine.UI;

public sealed class DroneUpgradePanel : SelectionPanelWithPresentation
{
    [Header("TextFields")]
    
    [SerializeField] private TextMeshProUGUI _droneNameText;
    
    [SerializeField] private TextMeshProUGUI _droneUpgradeDescriptionText;
    
    [Header("LevelUpDescription")]
    
    [SerializeField] private TextMeshProUGUI _currentLevelText;
    
    [SerializeField] private TextMeshProUGUI _nextLevelText;
    
    [SerializeField] private Image _currentLevelSlider;
    
    [SerializeField] private Image _nextLevelSlider;

    [Header("VisualLinks")]
    
    [SerializeField] private Image _presentationImage;
    
    [SerializeField] private Image _shineImage;

    [SerializeField] private Image _shineReplacment;

    private DroneBasis _drone;

    public void Setup(DroneBasis droneToUpgrade)
    {
        _drone = droneToUpgrade;
        
        SetupTextFields();

        SetupSlider();

        SetupPresentationImage();
        
        SetupVisual();
    }

    private void SetupPresentationImage()
    {
        CreatePresentation(_drone, _drone.GetLevel() + 1, 1);
        
        _presentationImage.material = GetPresentationMaterial();
    }
    private void SetupTextFields()
    {
        DroneData droneData = _drone.GetDroneData();

        _droneNameText.text = droneData.Name;

        _droneUpgradeDescriptionText.text = droneData.UpgradesDescription[_drone.GetLevel() - 1];
    }
    private void SetupSlider()
    {
        int level = _drone.GetLevel();

        _currentLevelText.text = level.ToString();

        _nextLevelText.text = (level + 1).ToString();

        _currentLevelSlider.fillAmount = 1f / 5f * level;

        _nextLevelSlider.fillAmount = 1f / 5f * (level + 1);
    }
    private void SetupVisual()
    {
        _shineImage.material = Main.droneSelector.gameObject.GetComponent<UIMaterialFactory>().GetPanelMaterial(_drone.GetDroneData().Type, 0);

        _shineReplacment.material = Main.droneSelector.gameObject.GetComponent<UIMaterialFactory>().GetParticleMaterial(_drone.GetDroneData().Type);
    }

    protected override void Select()
    {
        UpgradeDrone();
        
        Main.droneSelector.ClosePanels();

        Main.arenaManager.DronePickedUp();
    }
    
    private void UpgradeDrone() => Main.droneContainer.TryGetUpgradableDrone(_drone.GetDroneData()).Upgrade();
    
    public override void Close()
    {
        _currentPresentationManager.StopSession();

        Destroy(_currentPresentationManager.gameObject);

        Destroy(gameObject);
    }
    
    private void OnDisable() => _currentPresentationManager?.SetActive(false);
    
    private void OnEnable() => _currentPresentationManager?.SetActive(true);
}
