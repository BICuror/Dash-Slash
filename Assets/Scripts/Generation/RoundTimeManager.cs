using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoundTimeManager : MonoBehaviour
{
    [SerializeField] private float _roundTime;

    private void Start() => Main.arenaManager.StopArena += StopTimer;

    public void StartTimer() => StartCoroutine(CountdownTimer());
    
    private IEnumerator CountdownTimer()
    {
        yield return new WaitForSeconds(_roundTime);

        Main.enemySpawner.StopSpawn();

        Main.trapSpawner.StartTargetingPlayer();
    }

    private void StopTimer() => StopAllCoroutines();
}
