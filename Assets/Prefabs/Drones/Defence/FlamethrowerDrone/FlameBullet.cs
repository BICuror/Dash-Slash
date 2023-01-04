using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlameBullet : MonoBehaviour
{   
    public void SetLifetime(float lifetime) => Destroy(gameObject, lifetime);

    protected virtual void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.TryGetComponent(out EnemyStatus enemyStatus))
        {
            enemyStatus.SetOnFire();
        }
    }
}
