using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapSpawner : MonoBehaviour
{ 
    [SerializeField] private int _difficultyPerLevel;

    private float _delayMultyplicator = 1f;

    [Range(0, 100)] private int _difficulty;

    [SerializeField] private DefaultTrapSpawner[] _spawners;

    [SerializeField] private WallMoveTrapSpawner _movingWallTrapSpawner;

    [SerializeField] private float _enemyWeightForMovingWallApearing;

    private bool _targetPlayer;

    public void StartArena()
    {
        _targetPlayer = false;

        Main.arenaManager.ArenaStopped.AddListener(AddDifficulty);

        StartCoroutine(SetUpNextTrap(3f));
    }

    private void AddDifficulty() => _difficulty += _difficultyPerLevel;

    private void SetUpTrap()
    {
        float waitingTime = 1f;
        
        if (Random.Range(0f, 100f) < Main.enemyList.GetAmountOfEnemies() * _enemyWeightForMovingWallApearing)
        {
            _movingWallTrapSpawner.Spawn(GetRandomPosition(), _difficulty);
            waitingTime = _movingWallTrapSpawner.GetDelayTime();
        }
        else
        {
            float currentAppearanceChance = 0f;

            while (true)
            {
                int randomIndex = Random.Range(0, _spawners.Length);

                currentAppearanceChance += _spawners[randomIndex].GetAppearanceChance();

                if (currentAppearanceChance > Random.Range(0f, 100f))
                {
                    if (_targetPlayer == false) _spawners[randomIndex].Spawn(GetRandomPosition(), _difficulty);
                    else _spawners[randomIndex].Spawn(Main.playerTransform.position, _difficulty);

                    waitingTime = _spawners[randomIndex].GetDelayTime();
                    
                    break;
                }
            }
        }

        StartCoroutine(SetUpNextTrap(waitingTime * _delayMultyplicator));
    }

    private IEnumerator SetUpNextTrap(float waitingTime)
    {
        yield return new WaitForSeconds(waitingTime);

        SetUpTrap();
    }

    private Vector3 GetRandomPosition()
    {
        float x = Random.Range(-Main.roomSettings.GetWidth() + 1f, Main.roomSettings.GetWidth() - 1f);
        float y = Random.Range(-Main.roomSettings.GetHeight() + 1f, Main.roomSettings.GetHeight() - 1f);

        return new Vector3(x, y, 0f);
    }

    public void StopArena()
    {
        StopAllCoroutines();
    }

    public void StartTargetingPlayer() => _targetPlayer = true;
}
