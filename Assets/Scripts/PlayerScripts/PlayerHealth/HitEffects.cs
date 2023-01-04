using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitEffects : MonoBehaviour
{
    [SerializeField] private PlayerHealthStats _playerHealthStats;

    private ParticleSystem particleSystem;

    private void Awake()
    {
        _playerHealthStats.HealthDecreasedEvent.AddListener(Activate);

        particleSystem = GetComponent<ParticleSystem>();
    }

    public void Activate()
    {
        particleSystem.Play();

        Main.cameraEffects.ActivateHitEffect();

        Main.cameraEffects.StartMediumScreenShake();
    }
}
