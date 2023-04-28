using UnityEngine;

public class SpecialEffectsSetter : MonoBehaviour
{
    [SerializeField] private GameObject _menuPostProcessing;

    [SerializeField] private ToggleSettings _toggleSettings;

    private void Start()
    {
        _toggleSettings.StateChanged.AddListener(SetPostprocessingState);

        bool useSpecialEffects;

        if (PlayerPrefs.HasKey("UseSpecialEffects"))
        {   
            useSpecialEffects = (PlayerPrefs.GetInt("UseSpecialEffects") == 1);
        }
        else
        {
            PlayerPrefs.SetInt("UseSpecialEffects", 1);

            useSpecialEffects = true;
        }

        _toggleSettings.SetState(useSpecialEffects);
    }

    private void SetPostprocessingState(bool state)
    {
        _menuPostProcessing.SetActive(state);

        if (state) PlayerPrefs.SetInt("UseSpecialEffects", 1);
        else PlayerPrefs.SetInt("UseSpecialEffects", 0);
    }
}
