using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using UnityEngine.Events;

public class UnlockInfoPanel : MonoBehaviour
{
    [Header("PresentationSettings")]
    [SerializeField] private GameObject _presentationPrefab;
    [SerializeField] private Material _presentationMaterial;

    [SerializeField] private Material _defaultMaterial;
    [SerializeField] private int _cameraTextureXResolution = 500;
    [SerializeField] private int _cameraTextureYResolution = 300;
    protected PresentationManager _currentPresentationManager;

    [Header("DroneInfoLinks")]
    [SerializeField] private GameObject _dronePanel;
    [SerializeField] private TextMeshProUGUI _droneNameTextField;
    [SerializeField] private TextMeshProUGUI _droneDescriptionTextField;
    [SerializeField] private Image _dronePresentationImage;

    [Header("PerlInfoLinks")]
    [SerializeField] private GameObject _perkPanel;
    [SerializeField] private TextMeshProUGUI _perkNameTextField;
    [SerializeField] private TextMeshProUGUI _perkDescriptionTextField;
    [SerializeField] private Image _perkPresentationImage;

    private Unlockable _unlockable;

    [Header("Lock|UnlockLinks")]
    [SerializeField] private TextMeshProUGUI _unlockPointsField;
    [SerializeField] private TextMeshProUGUI _priceField;
    [SerializeField] private GameObject _unlockButton;
    [SerializeField] private GameObject _unlockedText;
    [SerializeField] private UnlockPointsCounter _unlockPoints;

    [SerializeField] private Animator _anim;

    public UnityEvent UnlockedSomething;

    private void Awake() 
    {
        _unlockPoints.LoadPoints();
        
        UpdateCurrentUnlockPoints();
    }

    public void OnEnable() 
    {
        if (gameObject.activeSelf == false) 
        {
            gameObject.SetActive(true);
            
            _anim.Play("ProgressionInfoPanelAppear");
        }
    }

    public void StartDeactivatingPanel()
    {
        _anim.Play("ProgressionPanelDisappear");
    }

    public void DeactivatePanel() => gameObject.SetActive(false);

    public void DisplayDroneUnlockInfo(UnlockableDrone unlockableDrone) 
    {
        OnDisable();

        _dronePanel.SetActive(true);

        CreatePresentation(unlockableDrone.Drone);

        _unlockable = unlockableDrone;

        _dronePresentationImage.material = GetPresentationMaterial();

        _droneNameTextField.text = unlockableDrone.Drone.GetDroneData().Name;
        
        _droneDescriptionTextField.text = unlockableDrone.Drone.GetDroneData().Description;

        _priceField.text = _unlockable.Price.ToString();

        if (unlockableDrone.IsUnlocked()) DisplayUnlocked();
        else DisplayLocked();
    }

    private void CreatePresentation(DroneBasis drone)
    {
        GameObject currentPresentation = Instantiate(_presentationPrefab, new Vector3(0, -1000f, 0f), Quaternion.identity);

        _currentPresentationManager = currentPresentation.GetComponent<PresentationManager>();

        _currentPresentationManager.StartSession(drone, 1);
    }

    private Material GetPresentationMaterial()
    {
        Material presentationMaterial = new Material(_presentationMaterial); 

        presentationMaterial.SetTexture("MainTexture", _currentPresentationManager.GetCameraTexture(_cameraTextureXResolution, _cameraTextureYResolution));

        return presentationMaterial;
    }

    public void DisplayPerkUnlockInfo(UnlockablePerk unlockablePerk) 
    {
        OnDisable();
        
        _perkPanel.SetActive(true);

        _unlockable = unlockablePerk;

        _perkPresentationImage.sprite = unlockablePerk.Perk.Icon;

        _perkNameTextField.text = unlockablePerk.Perk.PerkName;

        unlockablePerk.Perk.Perk.SetDescription(_perkDescriptionTextField);

        _priceField.text = _unlockable.Price.ToString();

        if (unlockablePerk.IsUnlocked()) DisplayUnlocked();
        else DisplayLocked();
    }

    private void OnDisable()
    {
        _perkPanel.SetActive(false);

        _dronePanel.SetActive(false);

        if (_currentPresentationManager != null)
        {
            _currentPresentationManager.StopSession();

            Destroy(_currentPresentationManager.gameObject);
        } 
    }

    private void DisplayUnlocked()
    {
        _unlockButton.SetActive(false);
        _unlockedText.SetActive(true);
    }

    private void DisplayLocked()
    {
        _unlockButton.SetActive(true);
        _unlockedText.SetActive(false);
    }

    public void TryUnlockDisplayedUnlock()
    {
        if (_unlockable.Price <= _unlockPoints.GetPoints())
        {
            _unlockPoints.SubstractPoints(_unlockable.Price);

            UpdateCurrentUnlockPoints();

            UnlockCurrrentDisplayedUnlock();
        } 
    }

    private void UnlockCurrrentDisplayedUnlock()
    {
        _unlockable.Unlock();

        DisplayUnlocked();

        UnlockedSomething.Invoke();

        UnlockButton[] buttons = FindObjectsOfType<UnlockButton>();

        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].CheckToActivate();
        }
    }

    private void UpdateCurrentUnlockPoints() => _unlockPointsField.text = _unlockPoints.GetPoints().ToString();
}
