using UnityEngine;

public sealed class SoundManager : MonoBehaviour
{
   [SerializeField] private AudioSource _musicSource; 

   private float musicVolume;

   private bool musicIsOn;

   [SerializeField] private AudioSource _sfxSource; 

   private float sfxVolume;

   private bool sfxIsOn;

   private void CloseSettings()
   {
      if (musicVolume == 0f) musicIsOn = false;
      if (sfxVolume == 0f) sfxIsOn = false; 
   }

   public void PlaySfx(AudioClip clip)
   {if (clip != null)
      _sfxSource.PlayOneShot(clip);
   }
}
