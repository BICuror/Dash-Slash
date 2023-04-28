using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

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

    private bool _isBossArena = false;

    public UnityEvent ArenaStarted;

    public UnityEvent ArenaStopped;

    public UnityEvent BossArenaStopped;

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

        ArenaStarted.Invoke();

        Main.playerTransform.position = Vector3.zero;

        Main.droneContainer.StartTasks();
        
        if (IsBossArena() != true)
        {
            Main.s_roundTimeManager.StartTimer();

            startingAmountOfEnemiesInGroups += additionalEnemies;
            
            Main.enemySpawner.StartSpawn(startingAmountOfEnemiesInGroups);

            Main.trapSpawner.StartArena();
            
            _isBossArena = false;
        }
        else
        {
            _bossSpawner.SpawnBoss();

            _isBossArena = true;
        }
    }

    private bool IsBossArena() => (currentWave > 3 && Random.Range(0f, 100f) < _chanceOfBossArena);

    public void StopArenaBattle()
    {
        if (_isBossArena) BossArenaStopped.Invoke();

        arenaIsActive = false;

        ArenaStopped.Invoke();

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
