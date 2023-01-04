using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public sealed class EnemyList : MonoBehaviour
{
    private List<Transform> _enemiesTransforms = new List<Transform>();

    private int _killedEnemiesCount = 0;
 
    public UnityEvent<Vector3> EnemyDied;

    public void RemoveEnemyFromList(Transform enemy)
    {
        _killedEnemiesCount++;

        _enemiesTransforms.Remove(enemy);

        EnemyDied.Invoke(enemy.position);
    }

    public Transform GetEnemy()
    {
        ClearFromNulls();

        return _enemiesTransforms[0];
    }

    public Transform GetEnemyByIndex(int index)
    {
        ClearFromNulls();

        return _enemiesTransforms[index];
    }

    public int GetAmountOfEnemies()
    {
        return _enemiesTransforms.Count;
    }

    public void AddEnemy(Transform newEnemy)
    {
        _enemiesTransforms.Add(newEnemy);
    }

    public Transform GetClosestEnemy(Vector3 postion)
    {
        ClearFromNulls();

        if (GetAmountOfEnemies() == 0) return null;

        float distance = Mathf.Infinity;

        int index = 0;

        for (int i = 0; i < _enemiesTransforms.Count; i++)
        {
            float currentDistance = Vector3.Distance(_enemiesTransforms[i].position, postion);

            if (currentDistance < distance)
            {
                distance = currentDistance;

                index = i;
            }
        }

        return _enemiesTransforms[index];
    }

    public List<Transform> GetRandomGroup(int groupSize)
    {
        List<Transform> availableEnemies = new List<Transform>(_enemiesTransforms);

        List<Transform> resultEnemies = new List<Transform>();

        if (groupSize > availableEnemies.Count) groupSize = availableEnemies.Count; 

        for (int i = 0; i < groupSize; i++)
        {
            int randomIndex = Random.Range(0, availableEnemies.Count);

            resultEnemies.Add(availableEnemies[randomIndex]);

            availableEnemies.RemoveAt(randomIndex);
        }

        return resultEnemies;
    }

    public Transform GetClosestEnemyToEnemy(Transform otherEnemy)
    {
        ClearFromNulls();

        if (GetAmountOfEnemies() <= 1) return null;

        float distance = Mathf.Infinity;

        int index = 0;

        for (int i = 0; i < _enemiesTransforms.Count; i++)
        {
            if (_enemiesTransforms[i] == otherEnemy) continue;

            float currentDistance = Vector3.Distance(_enemiesTransforms[i].position, otherEnemy.position);

            if (currentDistance < distance)
            {
                distance = currentDistance;

                index = i;
            }
        }

        return _enemiesTransforms[index];
    }

    public bool CheckIfEmpty()
    {
        ClearFromNulls();

        return _enemiesTransforms.Count == 0;
    }

    private void ClearFromNulls()
    {
        for (int i = 0; i < _enemiesTransforms.Count; i++)
        {
            if (_enemiesTransforms[i] == null) 
            {
                _enemiesTransforms.RemoveAt(i);

                i--;
            }
        }
    }

    public void ClearList() => _enemiesTransforms = new List<Transform>();
    
    public int GetAmountOfKilledEnemies() => _killedEnemiesCount;
}
