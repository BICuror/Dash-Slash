using UnityEngine;

public sealed class SoundContainer : MonoBehaviour
{
    [SerializeField] private AudioClip[] _soundsToPlay;

    [SerializeField] private bool _playOnAwake;

    private void Awake() 
    {
        if(_playOnAwake) PlaySound();
    }

    public void PlaySound()
    {
        Main.soundManager.PlaySfx(_soundsToPlay[Random.Range(0, _soundsToPlay.Length)]);
    }
}
