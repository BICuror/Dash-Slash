using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;


public class TransmutationPanel : SelectionPanelWithPresentation
{
    [SerializeField] private ParticleSystem _partSystem;


    [Header("FromDrone")]
    [SerializeField] private TextMeshProUGUI _fromDroneNameText;

    [SerializeField] private TextMeshProUGUI _fromDroneLevel;

    [SerializeField] private Image _fromPresentationImage;

    [SerializeField] private Material _reversedPresentationMaterial;

    [SerializeField] private Image _fromShine;

    [SerializeField] private Image _fromShineReplacment;

    private PresentationManager _fromPresentationManager;

    private DroneBasis _fromDrone;


    [Header("ToDrone")]
    [SerializeField] private TextMeshProUGUI _toDroneNameText;

    [SerializeField] private TextMeshProUGUI _toDroneLevel;

    [SerializeField] private Image _toPresentationImage;

    [SerializeField] private Image _toShine;

    [SerializeField] private Image _toShineReplacment;

    private PresentationManager _toPresentationManager;

    private DroneBasis _toDrone;

    [SerializeField] private float _chanseForTwoAdditionalLevels;
    private int _additionalLevels = 1;

    public void Setup(DroneBasis newFromDrone, DroneBasis newToDrone) 
    {
        _fromDrone = newFromDrone;
        _toDrone = newToDrone;

        if (Random.Range(0, 100) < _chanseForTwoAdditionalLevels && _fromDrone.GetLevel() < 4)
        {
            _additionalLevels = 2;
        }

        SetupTextFields();

        SetupVisuals();
    }

    private void SetupTextFields()
    {
        _fromDroneNameText.text = _fromDrone.GetDroneData().Name;
        _toDroneNameText.text = _toDrone.GetDroneData().Name;
    
        _fromDroneLevel.text = _fromDrone.GetLevel().ToString();
        _toDroneLevel.text = (_fromDrone.GetLevel() + _additionalLevels).ToString(); 
    }

    private void SetupVisuals()
    {
        CreatePresentation(_fromDrone, 1);

        _fromPresentationManager = _currentPresentationManager;

        _fromPresentationImage.material = GetPresentationMaterial(_reversedPresentationMaterial);

        _fromShine.material = Main.droneSelector.gameObject.GetComponent<UIMaterialFactory>().GetPanelMaterial(_fromDrone.GetDroneData().Type, 2);

        _fromShineReplacment.material = Main.droneSelector.gameObject.GetComponent<UIMaterialFactory>().GetParticleMaterial(_fromDrone.GetDroneData().Type);  

        //////////////////////////////////////////////////////////////////////////////////////////

        CreatePresentation(_toDrone, _fromDrone.GetLevel() + _additionalLevels, 2);

        _toPresentationManager = _currentPresentationManager;

        _toPresentationImage.material = GetPresentationMaterial();

        _toShine.material = Main.droneSelector.gameObject.GetComponent<UIMaterialFactory>().GetPanelMaterial(_toDrone.GetDroneData().Type, 1);

        _toShineReplacment.material = Main.droneSelector.gameObject.GetComponent<UIMaterialFactory>().GetParticleMaterial(_toDrone.GetDroneData().Type);  

        SetupParticleSystem(_fromShine.material.GetColor("MainColor"), _toShine.material.GetColor("MainColor"));
    }

    private void SetupParticleSystem(Color fromColor, Color toColor)
    {
        Gradient gradient = new Gradient();

        GradientColorKey[] colorKey = new GradientColorKey[2];
        colorKey[0].color = fromColor;
        colorKey[0].time = 0f;
        colorKey[1].color = toColor;
        colorKey[1].time = 1f;

        GradientAlphaKey[] alphaKey = new GradientAlphaKey[2];
        alphaKey[0].alpha = 1f;
        alphaKey[0].time = 0f;
        alphaKey[1].alpha = 1f;
        alphaKey[1].time = 1f;

        gradient.SetKeys(colorKey, alphaKey);

        var col = _partSystem.colorOverLifetime;
        
        col.color = gradient;
    }

    protected override void Select()
    {
        Main.droneSelector.ClosePanels();

        GameObject currentDrone = Instantiate(_toDrone.gameObject, Vector3.zero, Quaternion.identity);

        currentDrone.GetComponent<DroneBasis>().UpgradeToLevel(_fromDrone.GetLevel() + _additionalLevels);

        Main.droneContainer.AddDrone(currentDrone);
        
        Main.droneContainer.DestroyDrone(_fromDrone.GetDroneData(), _fromDrone.GetLevel());

        Main.arenaManager.DronePickedUp();
    }

    public override void Close()
    {
        _fromPresentationManager.StopSession(); _toPresentationManager.StopSession();

        Destroy(_fromPresentationManager.gameObject); Destroy(_toPresentationManager.gameObject);

        Destroy(gameObject);
    }

    private void OnDisable()
    {
        _fromPresentationManager?.SetActive(false);
        _toPresentationManager?.SetActive(false);
    }

    private void OnEnable()
    {
        _toPresentationManager?.SetActive(true);
        _fromPresentationManager?.SetActive(true);
    }
}
