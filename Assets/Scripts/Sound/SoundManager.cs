using UnityEngine;

public sealed class SoundManager : MonoBehaviour
{
   [SerializeField] private AudioSource _musicSource; 

   private float _musicVolume;

   private bool _musicIsOn;

   [SerializeField] private AudioSource _sfxSource; 

   private float _sfxVolume;

   private bool _sfxIsOn;

   private void CloseSettings()
   {
      if (_musicVolume == 0f) _musicIsOn = false;
      if (_sfxVolume == 0f) _sfxIsOn = false; 
   }

   public void PlaySfx(AudioClip clip)
   {
      if (clip != null)
      _sfxSource.PlayOneShot(clip);
   }
}
