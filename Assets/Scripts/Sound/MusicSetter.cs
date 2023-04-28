using UnityEngine;

public sealed class MusicSetter : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;  
    
    public void SetMusic(AudioClip audioClip)
    {
        _audioSource.clip = audioClip;

        _audioSource.Play();
    }
}
