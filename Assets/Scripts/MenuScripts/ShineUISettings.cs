using UnityEngine;

public sealed class ShineUISettings : MonoBehaviour
{
    [SerializeField] private GameObject _menuShine;

    [SerializeField] private ToggleSettings _toggleSettings;

    private void Start()
    {
        _toggleSettings.StateChanged.AddListener(SetPostprocessingState);

        bool useShineUI;

        if (PlayerPrefs.HasKey("UseShineUI"))
        {   
            useShineUI = (PlayerPrefs.GetInt("UseShineUI") == 1);
        }
        else
        {
            PlayerPrefs.SetInt("UseShineUI", 1);

            useShineUI = true;
        }

        _toggleSettings.SetState(useShineUI);
    }

    private void SetPostprocessingState(bool state)
    {
        _menuShine.SetActive(state);
    
        if (state) PlayerPrefs.SetInt("UseShineUI", 1);
        else PlayerPrefs.SetInt("UseShineUI", 0);
    }
}
