using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class BurnEffectManager : MonoBehaviour
{
    private float _hitDelay = 1f;

    private float _damagePerFireHit = 0.2f;

    private bool _isOnFire;

    private IDamagable _damagable;

    public void Start()
    {
        _damagable = gameObject.GetComponent<IDamagable>();
    }

    public bool IsOnFire() => _isOnFire;

    public void SetOnFire()
    {
        if (IsOnFire() == false)
        {
            _isOnFire = true;

            StartCoroutine(Burn());
        }
        else
        {
            _damagable.GetPercentHurt(_damagePerFireHit);
        }
    }

    private IEnumerator Burn()
    {
        yield return new WaitForSeconds(_hitDelay);

        _damagable.GetPercentHurt(_damagePerFireHit);

        StartCoroutine(Burn());
    }
}
