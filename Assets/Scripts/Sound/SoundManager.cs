using UnityEngine;
using System.Collections;

public sealed class SoundManager : MonoBehaviour
{
   [SerializeField] private SoundSettings _soundSettings;

   [SerializeField] private AudioSource _musicSource; 

   [SerializeField] private AudioSource _sfxSource;
   
   [SerializeField] private AudioSource _ambienceSource;

   private void Start()
   {
      _soundSettings.LoadSettings();

      _musicSource.volume = 0f;
      _ambienceSource.volume = 0f;

      StartCoroutine(IncreaseVolume());

      _soundSettings.SettingsChanged.AddListener(ChangeMusicVolume);
      _soundSettings.SettingsChanged.AddListener(cahngeAmbienceVolume);

      StartCoroutine(IncreaseVolume());
   }

   private IEnumerator IncreaseVolume()
   {
      yield return new WaitForFixedUpdate();

      _musicSource.volume += _soundSettings.GetMusicVolume() / 70f;
      _ambienceSource.volume += _soundSettings.GetSfxVolume() / 70f;

      if (_musicSource.volume < _soundSettings.GetMusicVolume()) StartCoroutine(IncreaseVolume());
      else 
      {
         _musicSource.volume = _soundSettings.GetMusicVolume();
         _ambienceSource.volume += _soundSettings.GetSfxVolume();
      }
   }

   private void OnDestroy() => _soundSettings.SettingsChanged.RemoveListener(ChangeMusicVolume);

   private void ChangeMusicVolume() => _musicSource.volume = _soundSettings.GetMusicVolume();
   private void cahngeAmbienceVolume() => _ambienceSource.volume = _soundSettings.GetSfxVolume();

   public void PlaySfx(AudioClip clip)
   {
      if (clip != null && _soundSettings.GetSfxVolume() > 0)
      {
         _sfxSource.PlayOneShot(clip, _soundSettings.GetSfxVolume());
      }
   }

   public void DecreaseMusicVolume() => StartCoroutine(DecreaseVolume());

   private IEnumerator DecreaseVolume()
   {
      yield return new WaitForFixedUpdate();

      _musicSource.volume -= 1f / 50f;

      if (_musicSource.volume > 0) StartCoroutine(DecreaseVolume());
   }
}
