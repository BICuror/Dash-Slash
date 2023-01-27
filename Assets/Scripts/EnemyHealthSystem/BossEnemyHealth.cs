using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(EnemyStatus))]

public sealed class BossEnemyHealth : MonoBehaviour, IDamagable
{
    [Header("HealthSettings")]

    [SerializeField] private float _maxHealth;

    private float _currentHealth;

    private EnemyStatus _statusManager;

    private bool _isDead;

    [Header("Events")]
    public UnityEvent OnHit; 
    public UnityEvent OnDeath;  

    private void Start() => _statusManager = GetComponent<EnemyStatus>();

    private void OnEnable() => Main.s_bossHealthBar.ActivateHealthBar();

    public void MultiplyMaxHelath(float percent)
    {
        _maxHealth *= percent;

        _currentHealth = _maxHealth;
    }

    public void GetPercentHurt(float percent) => GetHurt(_maxHealth * percent * 0.1f);

    public void GetHurt(float damage) 
    {
        OnHit.Invoke();

        damage = MultyplyDamageByStatus(damage);

        _currentHealth -= damage;
            
        Main.s_bossHealthBar.UpdateHealthBar(_currentHealth / _maxHealth);

        if (_currentHealth <= 0f && _isDead == false) 
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
        _isDead = true;

        Main.s_bossHealthBar.DeactivateHealthBar();

        OnDeath.Invoke();
    }

    private void OnDestroy() => Main.arenaManager.StopArenaBattle();
}
