using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerModule : MonoBehaviour
{
    [Header("SpawnerSettings")]

    [SerializeField] private GameObject[] spawnEnemies;

    public void SpawnEnemy(Vector3 spawnPosition)
    {
        Main.enemySpawner.SpawnEnemy(spawnPosition, spawnEnemies[Random.Range(0, spawnEnemies.Length)]);
    }
}
