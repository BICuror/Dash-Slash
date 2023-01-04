using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class CameraScreenshake : MonoBehaviour
{
    [Header("SmallScreenshakeSettings")]
    
    [SerializeField] private ScreenshakeSettings _smallScreenshakeSettings;

    [Header("MediumScreenshakeSettings")]
    
    [SerializeField] private ScreenshakeSettings _mediumScreenshakeSettings;

    [Header("BigScreenshakeSettings")]
    
    [SerializeField] private ScreenshakeSettings _bigScreenshakeSettings;

    [System.Serializable] public struct ScreenshakeSettings
    {
        public AnimationCurve Curve;

        public float Strength;

        public float Duration;
    }

    private ScreenshakeSettings _currentScreenShakeSettings;

    private Vector3 _randomVector;
    
    private float _timeEvaluated;

    public void StartScreenShake(CurveType type)
    {
        _timeEvaluated = 0f;

        _currentScreenShakeSettings = type switch
        {
            CurveType.Small => _smallScreenshakeSettings,
            CurveType.Medium => _mediumScreenshakeSettings,
            CurveType.Big => _bigScreenshakeSettings
        };
        
        _randomVector = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0f).normalized * _currentScreenShakeSettings.Strength;

        StartCoroutine(Shake());
    }

    private IEnumerator Shake()
    {
        yield return new WaitForFixedUpdate();

        _timeEvaluated += Time.deltaTime;

        transform.position = _currentScreenShakeSettings.Curve.Evaluate(_timeEvaluated / _currentScreenShakeSettings.Duration) * _randomVector;

        if (_timeEvaluated < _currentScreenShakeSettings.Duration) StartCoroutine(Shake());
        else transform.position = Vector3.zero;
    }

    public enum CurveType
    {
        Small,
        Medium, 
        Big
    }
}
