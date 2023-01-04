using UnityEngine;
using UnityEngine.Events;

public sealed class LowHealthEffect : MonoBehaviour
{
    [SerializeField] private PlayerHealthStats _playerHealthStats;

    private void Awake()
    {
        _playerHealthStats.HealthChangedEvent.AddListener(OnHealthChanged);
    }

    private void OnHealthChanged(int healthPoints)
    {
        if (healthPoints != 1)
        {
            Main.cameraEffects.DeactivateLowHealthEffect();
        }
        else
        {
            Main.cameraEffects.ActivateLowHealthEffect();
        }
    }
}
