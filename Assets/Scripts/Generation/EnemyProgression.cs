using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProgression : MonoBehaviour
{   
    [SerializeField] private float enemyHealthProgressinonPerArena = 0.3f;

    [SerializeField] private float overallEnemyHealthMultiplierPerArena = 0.05f;

    public float GetEnemyHealth(int wave, float baseHealth)
    {
        return (baseHealth * (1f + (enemyHealthProgressinonPerArena * wave))) * (1f + (overallEnemyHealthMultiplierPerArena * wave)); ;
    }
}
