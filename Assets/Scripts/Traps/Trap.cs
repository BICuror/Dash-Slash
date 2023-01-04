using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.TryGetComponent(out EnemyHealth enemyHealth))
        {
            enemyHealth.Die();
        }
    }

    private void OnDestroy()
    {
        Main.enemySpawner.CheckIfLast();
    }
}
