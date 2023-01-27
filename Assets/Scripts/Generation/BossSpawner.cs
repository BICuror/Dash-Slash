using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSpawner : MonoBehaviour
{
    [SerializeField] private List<GameObject> _bosses;

    [SerializeField] private EnemyProgression _enemyProgression;

    public void SpawnBoss()
    {
        GameObject currentBoss = Instantiate(_bosses[Random.Range(0, _bosses.Count)], Vector3.zero, Quaternion.identity);

        BossEnemyHealth bossHealth = currentBoss.GetComponent<BossEnemyHealth>();

        bossHealth.MultiplyMaxHelath(_enemyProgression.GetEnemyHealth(Main.arenaManager.GetCurrentWave()));

        Main.enemyList.AddEnemy(currentBoss.transform);
    }
}
