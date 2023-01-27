using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class SpamRocketBullet : MonoBehaviour
{
    [SerializeField] private GameObject _bullet;

    [SerializeField] private int _spawnSycles;

    [SerializeField] private int _amountOfBulletsPerHit;
    
    [SerializeField] private float _timeBeforeStartingExploding;

    [SerializeField] private float _delayTime;

    [SerializeField] private int _amountOfBulletsUponExplosion;

    [SerializeField] private float _bulletSpeed;

    [SerializeField] private float _rotationSpeed;

    private float _bulletDamage;

    private EnemyList _enemyList;

    private Rigidbody2D _rb;

    public void Setup(float damageValue, int spawnSyclesValue, EnemyList newEnemyList, float thisBulletSpeed)
    {
        _rb = GetComponent<Rigidbody2D>();

        _rb.AddForce(transform.right * thisBulletSpeed, ForceMode2D.Impulse);

        _bulletDamage = damageValue;

        _spawnSycles = spawnSyclesValue;

        _enemyList = newEnemyList;

        StartCoroutine(Shoot());
    }

    private IEnumerator Shoot()
    {
        yield return new WaitForSeconds(_delayTime);

        float angle = 360f / _amountOfBulletsPerHit;

        _rb.angularVelocity = _rotationSpeed;

        for (int i = 0; i < _amountOfBulletsPerHit; i++)
        {
            GameObject currentBullet = Instantiate(_bullet, this.gameObject.transform.position, transform.rotation);

            currentBullet.transform.Rotate(0f, 0f, angle * i);

            SetupBullet(currentBullet);
        }

        if (--_spawnSycles > 0) StartCoroutine(Shoot());
        else StartCoroutine(Explode());
    }
    
    private IEnumerator Explode()
    {
        yield return new WaitForSeconds(_delayTime * 3f);

        float angle = 360f / _amountOfBulletsUponExplosion;

        for (int i = 0; i < _amountOfBulletsUponExplosion; i++)
        {
            GameObject currentBullet = Instantiate(_bullet, this.gameObject.transform.position, transform.rotation);

            currentBullet.transform.Rotate(0f, 0f, angle * i);

            SetupBullet(currentBullet);
        }

        Destroy(gameObject);
    }

    private void SetupBullet(GameObject currentBullet)
    {
        Bullet currentBulletScript = currentBullet.GetComponent<Bullet>();
        
        currentBulletScript.CheckAndSetupEffects();
            
        currentBulletScript.Setup(_bulletDamage, _bulletSpeed, _enemyList);
    }
}
