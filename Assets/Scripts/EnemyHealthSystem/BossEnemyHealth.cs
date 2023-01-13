using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(EnemyStatus))]

public sealed class BossEnemyHealth : MonoBehaviour, IDamagable
{
    [Header("HealthSettings")]

    [SerializeField] private float _maxHealth;

    private float _currentHealth;

    private EnemyStatus _statusManager;

    [Header("Events")]
    public UnityEvent OnHit; 
    public UnityEvent OnDeath;  

    private void OnEnable() => Main.s_bossHealthBar.ActivateHealthBar();

    private void OnDisable() => Main.s_bossHealthBar.DeactivateHealthBar();

    public void GetPercentHurt(float percent) => GetHurt(_maxHealth * percent);

    public void GetHurt(float damage) 
    {
        OnHit.Invoke();

        damage = MultyplyDamageByStatus(damage);

        _currentHealth -= damage;
            
        if (_currentHealth <= 0f) 
        {
            Die();
        }
    }

    private float MultyplyDamageByStatus(float damage)
    {
        if (_statusManager.IsOnFire() == true) damage += damage * Main.combatStats.additionalDamageToOnFireEnemies;
        if (_statusManager.IsShocked() == true) damage += damage * Main.combatStats.additionalDamageToShokedEnemies;

        return damage;
    }

    private void Die()
    {
        OnDeath.Invoke();
    }
}
