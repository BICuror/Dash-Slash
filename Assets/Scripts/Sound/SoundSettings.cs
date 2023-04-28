using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "SoundSettings", menuName = "SoundSettings", order = 0)]

public sealed class SoundSettings : ScriptableObject 
{
    private float _sfxVolume;

    private float _musicVolume;

    public UnityEvent SettingsChanged;

    public void LoadSettings()
    {
        if (PlayerPrefs.HasKey("sfxVolume")) _sfxVolume = PlayerPrefs.GetFloat("sfxVolume");
        else _sfxVolume = 0.5f; 

        if (PlayerPrefs.HasKey("musicVolume")) _musicVolume = PlayerPrefs.GetFloat("musicVolume");
        else _musicVolume = 0.5f; 
    }

    public float GetSfxVolume() => _sfxVolume;
    public float GetMusicVolume() => _musicVolume;

    public void SetSfxVolume(float value)
    {
        _sfxVolume = value;

        PlayerPrefs.SetFloat("sfxVolume", _sfxVolume);

        SettingsChanged.Invoke();
    }

    public void SetMusicVolume(float value)
    {
        _musicVolume = value;

        PlayerPrefs.SetFloat("musicVolume", _musicVolume);

        SettingsChanged.Invoke();
    }
}
