using UnityEngine;

public sealed class CameraEffects : MonoBehaviour
{
    [Header("PostprocessingEffectsAnimator")]
    
    [SerializeField] private Animator _dashPostprocessingAnimator;
    
    [SerializeField] private Animator _hitPostprocessingAnimator;
    
    [SerializeField] private Animator _healPostprocessingAnimator;
    
    [SerializeField] private Animator _lowHealthPostprocessingAnimator;

    [SerializeField] private GameObject _defaultPostProcessing;

    private Animator _anim;
    
    private CameraScreenshake _screenshakeModule;
 
    public void StartSmallScreenShake() => _screenshakeModule.StartScreenShake(CameraScreenshake.CurveType.Small);
    
    public void StartMediumScreenShake() => _screenshakeModule.StartScreenShake(CameraScreenshake.CurveType.Medium);

    public void StartBigScreenShake() => _screenshakeModule.StartScreenShake(CameraScreenshake.CurveType.Big);

    public void ActivateDashEffect() => _dashPostprocessingAnimator.Play("Activate");

    public void ActivateHitEffect() => _hitPostprocessingAnimator.Play("Activate");

    public void ActivateHealedEffect() => _healPostprocessingAnimator.Play("Activate");
    
    public void ActivateLowHealthEffect() => _lowHealthPostprocessingAnimator.Play("Activate");

    public void DeactivateLowHealthEffect() => _lowHealthPostprocessingAnimator.Play("Deactivate");

    private void Start()
    {
        if (PlayerPrefs.HasKey("UseSpecialEffects"))
        {   
            bool useSpecialEffects = (PlayerPrefs.GetInt("UseSpecialEffects") == 1);
            SetActiveAdditionalEffects(useSpecialEffects);
            _defaultPostProcessing.SetActive(useSpecialEffects);
        }
    }

    public void SetActiveAdditionalEffects(bool state)
    {
        _dashPostprocessingAnimator.gameObject.SetActive(state);
        _healPostprocessingAnimator.gameObject.SetActive(state);
        _hitPostprocessingAnimator.gameObject.SetActive(state);
    }

    private void Awake()
    {
        _anim = GetComponent<Animator>();

        _screenshakeModule = GetComponent<CameraScreenshake>();
    }
}
