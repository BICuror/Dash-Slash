using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySelector : MonoBehaviour
{
    [SerializeField] private float oneEnemyVarietyChanse;
    [SerializeField] private float twoEnemyVarietyChanse;
    [SerializeField] private float threeEnemyVarietyChanse;

    [SerializeField] private EnemySpawner spawner;

    [SerializeField] private List<GameObject> easyEnemies;

    [SerializeField] private List<GameObject> mediumEnemies;

    [SerializeField] private List<GameObject> hardEnemies;


    private void Start()
    {   
        Main.arenaManager.StartArena += RandomizeEnemies;
    }

    private int RandomizeEnemiesVariety()
    {
        float randomValue = Random.Range(0f, 1f);

        float currentChanse = oneEnemyVarietyChanse;

        if (currentChanse > randomValue) return 1; else currentChanse += twoEnemyVarietyChanse;
        if (currentChanse > randomValue) return 2; 
        return 3; 
    }

    private void RandomizeEnemies()
    {
        int variety = RandomizeEnemiesVariety();

        List<GameObject> selectedEnemies = new List<GameObject>();

        List<GameObject> enemies;

        if (Main.arenaManager.GetCurrentWave() < 4) enemies = easyEnemies;
        else if (Main.arenaManager.GetCurrentWave() < 8) enemies = mediumEnemies;
        else enemies = hardEnemies;

        List<GameObject> availableEnemies = new List<GameObject>(enemies);

        for (int i = 0; i < variety; i++)
        {
            int randomIndex = Random.Range(0, availableEnemies.Count);

            selectedEnemies.Add(availableEnemies[randomIndex]);

            availableEnemies.RemoveAt(randomIndex);
        }

        spawner.SetEnemiesVariety(selectedEnemies);
    }
}
