using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(EnemyStatus))]

public class EnemyHealth : MonoBehaviour, IDamagable
{
    [Header("MainStats")]
    [SerializeField] private float _maxHealth;
    public float MaxHealth { get => _maxHealth; set {_maxHealth = value;} }

    private float _currentHealth;
    
    private EnemyStatus _statusManager;
    
    [Header("Events")]
    public UnityEvent HitEvent; 
    public UnityEvent<Transform> DeathEvent; 
    
    private void Awake() => _statusManager = GetComponent<EnemyStatus>();

    public float GetCurrentHealth() => _currentHealth / _maxHealth;
    
    public void MultiplyMaxHelath(float multiplier)
    {
        _maxHealth *= multiplier;

        _currentHealth = _maxHealth;
    }
    
    public void GetPercentHurt(float percent) => GetHurt(_maxHealth * percent);

    public void GetHurt(float damage) 
    {
        HitEvent.Invoke();

        damage = MultyplyDamageByStatus(damage);

        if (AntioneshotCheck(damage))
        {
            _currentHealth = 1f; 
        }
        else 
        {
            _currentHealth -= damage;
            
            if (_currentHealth <= 0f) 
            {
                Die();
            }
        }
    }
    
    private bool AntioneshotCheck(float damage)
    {
        return (damage >= _maxHealth && _currentHealth == _maxHealth);
    }

    private float MultyplyDamageByStatus(float damage)
    {
        if (_statusManager.IsOnFire() == true) damage += damage * Main.combatStats.additionalDamageToOnFireEnemies;
        if (_statusManager.IsShocked() == true) damage += damage * Main.combatStats.additionalDamageToShokedEnemies;

        return damage;
    }

    public void Die() 
    {
        DeathEvent.Invoke(transform);
        
        Destroy(gameObject);
    }
}
 