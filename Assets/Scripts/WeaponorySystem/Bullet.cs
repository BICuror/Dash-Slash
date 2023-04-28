using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private DroneType _damageType;

    [SerializeField] private bool _isPenetrating;

    [SerializeField] private GameObject _bulletParticles;

    [HideInInspector] private float _damage;

    // homing settings
    protected Rigidbody2D _rb;

    protected float _speed;

    private float _homingSpeed;

    protected EnemyList _enemyList;

    protected Transform _targetTransform;

    // bounce settings

    private int _leftBounces;

    public void Setup(float newDamageValue, float newSpeedValue, EnemyList enemyList) 
    {
        _speed = newSpeedValue;
        
        _damage = newDamageValue; 

        _enemyList = enemyList;

        _rb = GetComponent<Rigidbody2D>();
        
        _rb.AddForce(transform.right * _speed, ForceMode2D.Impulse);

        Destroy(gameObject, 4f);
    }

    public void CheckAndSetupEffects()
    {
        if (Main.combatStats.singleHomingSpeed > 0f) SetupHoming(Main.combatStats.singleHomingSpeed);

        if (Main.combatStats.singleBounceAmount > 0) _leftBounces = Main.combatStats.singleBounceAmount;

        if (Main.combatStats.singleArePenetrating == true) SetupPenetrating();
    }

    public void SetupBounce(int bounces) => _leftBounces = bounces;
    
    public void SetupHoming(float rotationSpeed)
    {
        _homingSpeed = rotationSpeed;

        StartCoroutine(HomeBullet());
    }
    
    public void SetupPenetrating() => _isPenetrating = true;

    private IEnumerator HomeBullet()
    {
        yield return new WaitForFixedUpdate();

        if (_targetTransform == null) 
        {
            if (_enemyList.GetAmountOfEnemies() != 0) _targetTransform = _enemyList.GetClosestEnemy(transform.position);
            else _rb.angularVelocity = 0f;
        } 
        else
        {
            float rotationAmount = Vector3.Cross((_targetTransform.position - transform.position).normalized, transform.right).z;

            _rb.angularVelocity = -rotationAmount * _homingSpeed;

            _rb.velocity = transform.right * _speed;
        }
        
        StartCoroutine(HomeBullet());
    }

    protected virtual void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.TryGetComponent(out IDamagable health))
        {
            health.GetHurt(Main.combatStats.MultiplyDamage(_damage, _damageType));
            
            if (_isPenetrating == false)
            {
                DestroyBullet((this.transform.position + other.gameObject.transform.position) / 2);
            }
        
            other.gameObject.GetComponent<IKnockbackable>().KnockBack(transform.position, 2f);
        }
        else if (other.gameObject.CompareTag("Borders"))
        {
            if (_leftBounces > 0)
            {
                Transform newTarget = _enemyList.GetClosestEnemy(transform.position);
            
                if (newTarget != null) Bounce(transform);
                else DestroyBullet(transform.position);
            }
            else
            {
                DestroyBullet(transform.position);
            }
        }
    }

    private void Bounce(Transform enemy)
    {
        Transform newTarget = _enemyList.GetClosestEnemyToEnemy(enemy);
            
        if (newTarget != null)
        {
            transform.right = newTarget.position - transform.position;

            _rb.velocity = transform.right * _speed;
        }

        _leftBounces--;
    }

    protected void DestroyBullet(Vector3 spawnPosition)
    {
        Instantiate(_bulletParticles, spawnPosition, Quaternion.identity);

        Destroy(gameObject);
    }

    protected float GetDamage() => _damage;

    public bool IsPenetrating() => _isPenetrating;

    public bool IsHoming() => _homingSpeed > 0;
}
