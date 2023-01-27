using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArenaManager : MonoBehaviour
{
    [Header("BattleSettings")]

    [SerializeField] private int startingAmountOfEnemiesInGroups;

    [SerializeField] private int additionalEnemies;

    [SerializeField] private BossSpawner _bossSpawner;

    [Range(0f, 100f)]
    [SerializeField] private float _chanceOfBossArena;


    private int currentWave;

    private bool arenaIsActive = false;

    public delegate void EventHandler(); 
    public event EventHandler StartArena;

    public event EventHandler StopArena;

    private void Start()
    {
        StopArenaBattle();
    }

    public bool ArenaIsActive()
    {
        return arenaIsActive;
    }

    public void StartArenaBattle()
    {   
        if (arenaIsActive == true) return;

        arenaIsActive = true;

        currentWave++;

        StartArena?.Invoke();

        Main.playerTransform.position = Vector3.zero;

        Main.droneContainer.StartTasks();
        
        if (IsBossArena() != false)
        {
            Main.s_roundTimeManager.StartTimer();

            startingAmountOfEnemiesInGroups += additionalEnemies;
            
            Main.enemySpawner.StartSpawn(startingAmountOfEnemiesInGroups);

            Main.trapSpawner.StartArena();
        }
        else
        {
            _bossSpawner.SpawnBoss();
        }
    }

    private bool IsBossArena() => (currentWave > 3 && Random.Range(0f, 100f) < _chanceOfBossArena);

    public void StopArenaBattle()
    {
        arenaIsActive = false;

        StopArena?.Invoke();

        Main.droneContainer.StopTasks();

        Main.trapSpawner.StopArena();

        Main.uiStateManager.ChangeUIToOptionsState();

        //Main.curseManager.DestroyAllCurses();

        Main.playerController.SetActivePlayer(false);

        ClearEverything();
        
        ChoseDrone();
    }
    
    public void ChoseDrone()
    {
        Main.droneSelector.ActivateSelection();

        Main.playerHealth.FullyHeal();
    }

    public void DroneChosen()
    {
        
    }

    public void InventoryClosed()
    {
        Main.uiStateManager.ChangeUIToArenaState();
        
        Main.playerController.SetActivePlayer(true);
    }

    public void DronePickedUp()
    {
        ChooseWeaknesss();
    }

    public void ChooseWeaknesss()
    {
        StartArenaBattle();

        /*if (Main.curseManager.TrySetCurse() == true)
        {
            Invoke("StartArenaBattle", 2.5f);
        }
        else
        {
            StartArenaBattle();
        }*/
    }   

    public void ClearEverything()
    {
        GameObject[] bullets = GameObject.FindGameObjectsWithTag("EnemyBullet");
        foreach (GameObject bullet in bullets) { Destroy(bullet); }

        GameObject[] traps = GameObject.FindGameObjectsWithTag("Trap");
        foreach (GameObject trap in traps) { Destroy(trap); }

        GameObject[] projectiles = GameObject.FindGameObjectsWithTag("Projectile");
        foreach (GameObject projectile in projectiles) { Destroy(projectile); }

        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject enemy in enemies) { if (enemy.TryGetComponent(out EnemyHealth health)) { health.Die(); } }
    }

    public int GetCurrentWave() => currentWave;
}
