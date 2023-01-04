using System.Collections;
using UnityEngine;

public class DroneBasis : MonoBehaviour
{
    [SerializeField] private DroneData _droneData;

    [Header("DroneStats")]

    [SerializeField] protected float restoringTime;

    protected EnemyList _enemyList;

    private int _currentLevel = 1;
    private const int _maxLevel = 5;

    private void Awake()
    {
        if (_droneData == null) Debug.LogError("No drone data in " + gameObject.name);
    }

    public DroneData GetDroneData() => _droneData;

    public void SetEnemyList(EnemyList newEnemyList) => _enemyList = newEnemyList;
    
    public void StartTask() => StartCoroutine(Activate());
    
    private IEnumerator Activate()
    {
        yield return new WaitForSeconds(restoringTime);
        
        DoTask();

        StartTask();
    }

    protected virtual void DoTask() => Debug.Log("DoTask in DroneBasis was not overdrivved");

    public virtual void StopTask() => StopAllCoroutines();

    public void UpgradeToLevel(int upgradeLevel)
    {
        while (_currentLevel < upgradeLevel)
        {
            Upgrade();
        }
    }
    
    public void Upgrade()
    {
        if (_currentLevel < _maxLevel)
        {
            _currentLevel++;

            UpgradeDrone();
        }
    }

    protected virtual void UpgradeDrone() => Debug.Log("UpgradeDrone in DroneBasis was not overdrivved");
    
    public int GetLevel() => _currentLevel;
}
