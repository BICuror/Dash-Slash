using UnityEngine;
using TMPro;
using UnityEngine.UI;

public sealed class DronePickUpPanel : SelectionPanelWithPresentation
{
    [Header("TextFields")]
    [SerializeField] private TextMeshProUGUI _droneNameText;
    [SerializeField] private TextMeshProUGUI _droneDescritionText;

    [Header("VisualLinks")]
    [SerializeField] private Image _presentationImage;
    [SerializeField] private Image _shineImage;
    [SerializeField] private Image _shineReplacment;

    private DroneBasis _drone;

    public void Setup(DroneBasis pickUpDrone)
    {
        _drone = pickUpDrone;

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

        _droneDescritionText.text = droneData.Description;

        _droneNameText.text = droneData.Name;
    }
    private void SetupVisual()
    {
        _shineImage.material = Main.droneSelector.gameObject.GetComponent<UIMaterialFactory>().GetPanelMaterial(_drone.GetDroneData().Type, 0);

        _shineReplacment.material = Main.droneSelector.gameObject.GetComponent<UIMaterialFactory>().GetParticleMaterial(_drone.GetDroneData().Type);
    }
    
    private void PlaceDrone()
    {
        GameObject currentDrone = Instantiate(_drone.gameObject, Vector3.zero, Quaternion.identity);

        Main.droneContainer.AddDrone(currentDrone);
    }

    protected override void Select()
    {
        Main.droneSelector.ClosePanels();

        PlaceDrone();    

        Main.arenaManager.DronePickedUp();
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
