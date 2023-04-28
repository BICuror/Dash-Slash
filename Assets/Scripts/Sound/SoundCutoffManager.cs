using System.Collections;
using UnityEngine;

public sealed class SoundCutoffManager : MonoBehaviour
{
    [SerializeField] private float _defaultStrength;

    [SerializeField] private float _muffedCutoffStrength;

    [SerializeField] private float _pausedCutoffStrength;

    [SerializeField] private AudioLowPassFilter _filter;

    public void SetTodefaultState()
    {
        if (Main.arenaManager.ArenaIsActive() == true) SetCutoffFrequency(_defaultStrength, 2.5f);
        else SetCutoffFrequency(_muffedCutoffStrength, 3f);
    } 

    public void SetPausedState() => _filter.cutoffFrequency = _pausedCutoffStrength;

    private void SetCutoffFrequency(float freq, float time)
    {
        StopAllCoroutines();

        float speed = (freq - _filter.cutoffFrequency) / (50f * time);

        StartCoroutine(ChangeCutoffFrequency(freq, speed));
    }

    private IEnumerator ChangeCutoffFrequency(float finalFreq, float speed)
    {
        yield return new WaitForFixedUpdate();

        _filter.cutoffFrequency += speed;

        if (speed < 0 && _filter.cutoffFrequency > finalFreq) StartCoroutine(ChangeCutoffFrequency(finalFreq, speed));
        else if (speed > 0 && _filter.cutoffFrequency < finalFreq) StartCoroutine(ChangeCutoffFrequency(finalFreq, speed));
    }
}
