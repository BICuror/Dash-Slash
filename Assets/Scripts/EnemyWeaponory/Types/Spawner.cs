using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : EnemyTaskManager
{
    private SpawnerModule spawnerModule;

    private void Awake()
    {
        spawnerModule = GetComponent<SpawnerModule>();
    }

    protected override void PreperateTask() {}

    protected override void DoTask()
    {
        spawnerModule.SpawnEnemy(transform.position);
    }  
}
