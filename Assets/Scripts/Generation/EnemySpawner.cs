using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("GeneralSettings")]

    [Range(0f, 10f)] [SerializeField] private float neededDistance;

    [SerializeField] private float randomExtraPosition;

    [SerializeField] private float timeBetweenWarningAndSpawning;

    [SerializeField] private float timeBetweenEnemies;

    [SerializeField] private float timeBetweenGroups;

    [SerializeField] private List<GameObject> enemies;

    [SerializeField] private GameObject warningObj;

    [Header("Balance")]
    [SerializeField] private float healthAmountPerWave;
    [SerializeField] private EnemyProgression progression;

    private int groupsLeftToSpawn;
    private bool isSpawning;

    public void SetEnemiesVariety(List<GameObject> newList) 
    {
        enemies = newList;
    }   

    private IEnumerator WaitForNextGroup()
    {
        yield return new WaitForSeconds(timeBetweenGroups);

        PrepareNextGroup();
    }

    private void PrepareNextGroup()
    {
        if (groupsLeftToSpawn <= 0) return; 

        Vector3 spawnPosition = GetRandomPosition();

        Warn(spawnPosition);
        
        SpawnNextGroup(spawnPosition);

        StartCoroutine(WaitForNextGroup());
    }
    
    private void SpawnNextGroup(Vector3 spawnPosition)
    {
        isSpawning = true;

        groupsLeftToSpawn--;

        StartCoroutine(WaitToSpawnGroup(spawnPosition));
    }
    
    private void Warn(Vector3 spawnPosition)
    {
        Instantiate(warningObj, spawnPosition, Quaternion.identity);
    }
    
    private IEnumerator WaitToSpawnGroup(Vector3 spawnPosition)
    {
        yield return new WaitForSeconds(timeBetweenWarningAndSpawning);

        float leftHPamount = healthAmountPerWave;

        int spawnedEnemies = 0;

        while (true)
        {   
            GameObject enemyToSpawn = enemies[Random.Range(0, enemies.Count)];

            StartCoroutine(WatiToSpawnEnemy(spawnedEnemies, spawnPosition, enemyToSpawn));

            leftHPamount -= enemyToSpawn.GetComponent<EnemyHealth>().MaxHealth;
            
            spawnedEnemies++;

            if (leftHPamount <= 0f) break; 
        }
        
        StartCoroutine(DisableSpawn(spawnedEnemies + 1));
    }

    private IEnumerator WatiToSpawnEnemy(int multiplier, Vector3 spawnPosition, GameObject enemyToSpawn)
    {
        yield return new WaitForSeconds(timeBetweenEnemies * multiplier);

        SpawnEnemy(spawnPosition, enemyToSpawn);
    }

    public void SpawnEnemy(Vector3 spawnPosition, GameObject enemyToSpawn)
    {
        float angle = Mathf.Atan2(Main.playerTransform.position.y, Main.playerTransform.position.x) * Mathf.Rad2Deg;
        
        GameObject curentEnemy = Instantiate(enemyToSpawn, spawnPosition, Quaternion.Euler(0f, 0f, angle));

        EnemyHealth health = curentEnemy.GetComponent<EnemyHealth>();

        health.DeathEvent.AddListener(Main.enemyList.RemoveEnemyFromList);

        health.MultiplyMaxHelath(progression.GetEnemyHealth(Main.arenaManager.GetCurrentWave()));

        health.DeathEvent.AddListener(CheckIfLast);

        Main.enemyList.AddEnemy(curentEnemy.transform);
    }

    public bool TrySpawnSingleEnemy(Vector3 spawnPosition)
    {
        bool condition = groupsLeftToSpawn > 0;

        if (condition) SpawnEnemy(spawnPosition, enemies[Random.Range(0, enemies.Count)]);

        return condition; 
    }

    private IEnumerator DisableSpawn(int multiplier)
    {
        yield return new WaitForSeconds(timeBetweenEnemies * multiplier);

        isSpawning = false;
    }

    private Vector3 GetRandomPosition()
    {   
        float x;
        float y;

        while (true)
        {
            x = Random.Range(-Main.roomSettings.GetWidth() + 1f, Main.roomSettings.GetWidth() - 1f);
            y = Random.Range(-Main.roomSettings.GetHeight() + 1f, Main.roomSettings.GetHeight() - 1f);

            if (Vector3.Distance(Main.playerTransform.position, new Vector3(x, y)) > neededDistance) break;            
        }

        return new Vector3(x, y, 0f);
    }
    
    public void StartSpawn(int amountsOfGroups)
    {
        groupsLeftToSpawn = amountsOfGroups;

        PrepareNextGroup();
    }

    public void CheckIfLast(Transform enemy)
    {
        if (Main.enemyList.CheckIfEmpty())
        {
            if (isSpawning == false && groupsLeftToSpawn > 0 )
            { 
                StopAllCoroutines();

                PrepareNextGroup();
            }
            else if (isSpawning == false && groupsLeftToSpawn <= 0 && Main.arenaManager.ArenaIsActive() == true) Main.arenaManager.StopArenaBattle();
        }
    }  

    public void StopSpawn()
    {   
        groupsLeftToSpawn = 0;
        isSpawning = false;

        StopAllCoroutines();

        CheckIfLast(null);
    }

    public void ChangeGroupSize(int amount)
    {
        healthAmountPerWave += amount;
    }
}
