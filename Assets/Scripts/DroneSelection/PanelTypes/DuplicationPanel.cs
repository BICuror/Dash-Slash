using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DuplicationPanel : SelectionPanelWithPresentation
{
    [Header("TextFields")]
    [SerializeField] private TextMeshProUGUI _droneNameText;

    [Header("VisualLinks")]
    [SerializeField] private Image _presentationImage;
    [SerializeField] private Image _shineImage;
    [SerializeField] private ParticleSystemRenderer _particleSystem;

    private DroneBasis _drone;

    public void Setup(DroneBasis droneToDuplicate)
    {
        _drone = droneToDuplicate;

        SetupTextFields();

        SetupPresentationImage();

        SetupVisual();
    }
    private void SetupPresentationImage()
    {
        CreatePresentation(_drone, 1);
        
        _presentationImage.material = GetPresentationMaterial();
    }
    private void SetupTextFields()
    {   
        DroneData droneData = _drone.GetDroneData();

        _droneNameText.text = droneData.Name + " " + _drone.GetLevel().ToString();
    }
    private void SetupVisual()
    {
        _shineImage.material = Main.droneSelector.gameObject.GetComponent<UIMaterialFactory>().GetPanelMaterial(_drone.GetDroneData().Type, 0);

        _particleSystem.material = Main.droneSelector.gameObject.GetComponent<UIMaterialFactory>().GetParticleMaterial(_drone.GetDroneData().Type);
    }
    
    private void PlaceDrone()
    {
        GameObject currentDrone = Instantiate(_drone.gameObject, new Vector3(0f, 4f, 0f), Quaternion.identity);

        currentDrone.GetComponent<DroneBasis>().UpgradeToLevel(_drone.GetLevel());

        Main.droneContainer.AddDrone(currentDrone);

        Main.droneSelector.ClosePanels();

        Main.arenaManager.DronePickedUp();
    }
    protected override void Select()
    {
        Main.droneSelector.ClosePanels();

        PlaceDrone();    
    }
    public override void Close()
    {
        _currentPresentationManager.StopSession();

        Destroy(_currentPresentationManager.gameObject);

        Destroy(gameObject);
    }



    private void OnDisable() => _currentPresentationManager?.SetActive(false);
    private void OnEnable() => _currentPresentationManager?.SetActive(true);
}
