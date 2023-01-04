using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour, IDamagable, IKnockbackable
{
    [SerializeField] private float _health;

    [SerializeField] private GameObject _destroyParticles;

    private float _currentHealth;

    private Rigidbody2D _rigidbody;

    private EnemyStatus _enemyStatus;

    private void Awake() 
    {
        _currentHealth = _health;

        _rigidbody = GetComponent<Rigidbody2D>();

        _enemyStatus = GetComponent<EnemyStatus>();
    } 

    public virtual void GetHurt(float damage) 
    {
        _currentHealth -= damage;

        _enemyStatus.HighlightEnemy();

        if (_currentHealth <= 0f)
        {
            Die();
        }
    }

    public void GetPercentHurt(float percent)
    {
        GetHurt(_health * percent);
    }
    
    public virtual void Die() 
    {
        Instantiate(_destroyParticles, transform.position, Quaternion.identity);

        Destroy(gameObject);
    }

    public void KnockBack(Vector3 knockBackSourcePosition, float knockBackStrength)
    {
        Vector3 knockbackDirection = (knockBackSourcePosition - transform.position).normalized;

        _rigidbody.AddForce(knockbackDirection * -knockBackStrength, ForceMode2D.Impulse);
    }
}
