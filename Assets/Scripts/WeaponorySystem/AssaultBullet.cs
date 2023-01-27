using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class AssaultBullet : Bullet
{
    [SerializeField] private GameObject _bullet;

    [SerializeField] private float _bulletSpeed;

    [SerializeField] private float _timeBeforeExploding;

    private int _amountOfBullets;

    private void Start() => StartCoroutine(WaitToExplode());        

    public void SetAmountOfBullets(int newAmountOfBullets) {_amountOfBullets = newAmountOfBullets;}

    private IEnumerator WaitToExplode()
    {
        yield return new WaitForSeconds(_timeBeforeExploding);

        Explode();
    }

    private void Explode()
    {
        StopAllCoroutines();

        for (int i = 1; i < _amountOfBullets + 1; i++)
        {
            StartCoroutine(InstantiateBullet(i * 0.01f));
        }

        Destroy(gameObject, 0.1f);
    }
    
    private IEnumerator InstantiateBullet(float waitingTime)
    {
        yield return new WaitForSeconds(waitingTime);

        GameObject currentBullet = Instantiate(_bullet, this.gameObject.transform.position, Quaternion.Euler(0f, 0f, Random.Range(0f, 360f)));
    
        Bullet currentBulletScript = currentBullet.GetComponent<Bullet>();
        
        currentBulletScript.Setup(GetDamage(), _bulletSpeed, _enemyList);

        currentBulletScript.CheckAndSetupEffects();
    }
    
    protected override void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.TryGetComponent(out IDamagable health))
        {
            health.GetHurt(GetDamage());
        }

        Explode();
    }
}
