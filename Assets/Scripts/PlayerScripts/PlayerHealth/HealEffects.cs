using UnityEngine;
using UnityEngine.Events;

public sealed class HealEffects : MonoBehaviour
{
    [SerializeField] private PlayerHealthStats _playerHealthStats;

    private void Awake()
    {
        _playerHealthStats.HealthIncreasedEvent.AddListener(OnHealthIncreased);
    }

    private void OnHealthIncreased()
    {
        Main.cameraEffects.ActivateHealedEffect();
    }
}
