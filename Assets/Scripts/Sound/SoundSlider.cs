using UnityEngine;

public sealed class SoundSlider : MonoBehaviour
{
    [SerializeField] private SoundSettings _soundSettings;

    [SerializeField] private VolumeSliderType _sliderType;

    public enum VolumeSliderType
    {
        Sfx,
        Music
    }

    private void Awake()
    {
        if (_sliderType == VolumeSliderType.Sfx)
        {
            GetComponent<UnityEngine.UI.Slider>().value = _soundSettings.GetSfxVolume();
        }
        else    
        {
            GetComponent<UnityEngine.UI.Slider>().value = _soundSettings.GetMusicVolume();
        }
    }
}
