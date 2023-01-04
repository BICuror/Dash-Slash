using UnityEngine;

public class SelectionPanelWithPresentation : SelectionPanel
{
    [Header("PresentationSettings")]
    [SerializeField] private GameObject _presentationPrefab;
    [SerializeField] private Material _presentationMaterial;
    [SerializeField] private int _cameraTextureXResolution = 500;
    [SerializeField] private int _cameraTextureYResolution = 300;
    protected PresentationManager _currentPresentationManager;

    
    protected void CreatePresentation(DroneBasis drone, int presentationIndex)
    {
        int currentIndex = transform.parent.GetSiblingIndex();

        GameObject currentPresentation = Instantiate(_presentationPrefab, new Vector3(presentationIndex * 100f, currentIndex * 100f, 0f), Quaternion.identity);

        _currentPresentationManager = currentPresentation.GetComponent<PresentationManager>();

        _currentPresentationManager.StartSession(drone, drone.GetLevel());
    }

    protected void CreatePresentation(DroneBasis drone, int droneLevel, int presentationIndex)
    {
        int currentIndex = transform.parent.GetSiblingIndex();

        GameObject currentPresentation = Instantiate(_presentationPrefab, new Vector3(presentationIndex * 100f, currentIndex * 100f, 0f), Quaternion.identity);

        _currentPresentationManager = currentPresentation.GetComponent<PresentationManager>();

        _currentPresentationManager.StartSession(drone, droneLevel);
    }

    protected Material GetPresentationMaterial()
    {
        Material presentationMaterial = new Material(_presentationMaterial); 

        presentationMaterial.SetTexture("MainTexture", _currentPresentationManager.GetCameraTexture(_cameraTextureXResolution, _cameraTextureYResolution));

        return presentationMaterial;
    }

    protected Material GetPresentationMaterial(Material specialMaterial)
    {
        Material presentationMaterial = new Material(specialMaterial); 

        presentationMaterial.SetTexture("MainTexture", _currentPresentationManager.GetCameraTexture(_cameraTextureXResolution, _cameraTextureYResolution));

        return presentationMaterial;
    }
}
