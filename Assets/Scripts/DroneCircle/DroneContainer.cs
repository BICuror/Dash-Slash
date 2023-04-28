using System.Collections.Generic;
using UnityEngine;

public sealed class DroneContainer : MonoBehaviour
{
    [Header("ContainerSettins")]

    [SerializeField] private int _avalableCells;

    [Header("Links")]

    [SerializeField] private Transform _parentTransform;
    
    private List<DroneBasis> _obtainedDrones = new List<DroneBasis>();

    public void AddDroneCell() => _avalableCells++;

    public void RemoveDroneCell() 
    {
        if (_obtainedDrones[_obtainedDrones.Count - 1] != null) Destroy(_obtainedDrones[_obtainedDrones.Count - 1]);
        _avalableCells--;
    }

    public bool HasFreeCells() => _obtainedDrones.Count < _avalableCells;
    
    public int GetAmountOfCells() => _avalableCells;
    
    public int GetAmountOfFreeCells() => _avalableCells - _obtainedDrones.Count;
    
    public List<DroneBasis> GetObtainedDrones() => _obtainedDrones;
    
    public DroneBasis GetDrone(int index) => _obtainedDrones[index];    
    
    public DroneBasis GetRandomDrone() => _obtainedDrones[Random.Range(0, _obtainedDrones.Count)];

    public DroneBasis GetRandomUpgradableDrone() 
    {
        if (GetAmountOfUpgradableDrones() > 0)
        {
            List<DroneBasis> drones = new List<DroneBasis>(_obtainedDrones);
            
            while (true)
            {
                int randomIndex = Random.Range(0, drones.Count);
                
                if (drones[randomIndex].GetLevel() >= 5)
                {
                    drones.RemoveAt(randomIndex);
                }
                else
                {
                    return drones[randomIndex];
                } 
            }
        }
        else
        {
            Debug.LogError("No upgradable drones to pick from in " + gameObject.name);
            return null;
        }
    }

    public int GetAmountOfDrones() => _obtainedDrones.Count;

    public void DestroyDrone(DroneData droneData, int level)
    {
        for (int i = 0; i < _obtainedDrones.Count; i++)
        {
            if (_obtainedDrones[i].GetDroneData() == droneData && _obtainedDrones[i].GetLevel() == level)
            {
                Destroy(_obtainedDrones[i].gameObject);
             
                _obtainedDrones.RemoveAt(i);
                
                break;
            } 
        }

        IntropolatePositions();
    }

    public DroneBasis TryGetUpgradableDrone(DroneData neededDroneData)
    {
        if (_obtainedDrones.Count != 0)
        {
            for (int i = 0; i < _obtainedDrones.Count; i++)
            {
                if (_obtainedDrones[i].GetDroneData() == neededDroneData && _obtainedDrones[i].GetLevel() < 5)
                {
                    return _obtainedDrones[i]; 
                }
            }
        }
        
        return null;
    }

    public void AddDrone(GameObject drone)
    {
        _obtainedDrones.Add(drone.GetComponent<DroneBasis>());

        drone.transform.SetParent(_parentTransform);

        drone.GetComponent<DroneBasis>().SetEnemyList(Main.enemyList);   
        
        IntropolatePositions();
    }

    public void StartTasks()
    {
        for(int i = 0; i < _obtainedDrones.Count; i++)
        {
            _obtainedDrones[i].StartTask();
        }
    }

    public void StopTasks()
    {
        for(int i = 0; i < _obtainedDrones.Count; i++)
        {
            _obtainedDrones[i].StopTask();
        }
    }

    public int GetAmountOfUpgradableDrones()
    {
        int count = 0;

        for (int i = 0; i < _obtainedDrones.Count; i++)
        {
            if (_obtainedDrones[i].GetLevel() < 5) 
            {
                count++;
            }
        } 

        return count;
    }

    private void IntropolatePositions()
    { 
        float angle = 360f / _obtainedDrones.Count;

        for (int i = 0; i < _obtainedDrones.Count; i++)
        {
            Vector3 pos = new Vector3(Mathf.Cos(angle * i * Mathf.Deg2Rad), Mathf.Sin(angle * i * Mathf.Deg2Rad), 0f);

            _obtainedDrones[i].gameObject.transform.localPosition = pos;
        }
    }
}
