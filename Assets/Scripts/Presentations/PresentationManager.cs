using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class PresentationManager : MonoBehaviour
{
    [Header("GeneralSettings")]
    [Range(0f, 3f)] [SerializeField] private float _extraRandomPositionMagnitude;


    [Header("Prefabs")]
    [SerializeField] private GameObject _smallEnemyPrefab;
    [SerializeField] private GameObject _mediumEnemyPrefab;
    [SerializeField] private GameObject _bigEnemyPrefab;

    [SerializeField] private GameObject _spawnPrefab;    

    [Header("Links")]
    [SerializeField] private EnemyList _enemyList;
    [SerializeField] private Camera _camera;
    
    private PresentationData _currentPresentationData;
    private List<Transform> _spawns;
    private DroneBasis _presentedDroneBasis;
    private bool _presentationIsActive;

    public void StartSession(DroneBasis droneBasis, int droneLevel)
    {   
        if (_presentationIsActive == true) StopSession();

        _presentationIsActive = true;

        _currentPresentationData = droneBasis.GetDroneData().PresentationData;
        
        InstantiateEnemies();

        CreateDrone(droneBasis.gameObject, droneLevel);
    }

    private void CreateDrone(GameObject droneToCreate, int droneLevel)
    {
        GameObject presentedDrone = Instantiate(droneToCreate, transform.position + _currentPresentationData.DroneLocalPosition, Quaternion.identity);
        
        _presentedDroneBasis = presentedDrone.GetComponent<DroneBasis>();

        Debug.Log(_presentedDroneBasis.GetLevel().ToString());
        
        _presentedDroneBasis.UpgradeToLevel(droneLevel);
        _presentedDroneBasis.SetEnemyList(_enemyList);
        _presentedDroneBasis.StartTask();

        Debug.Log(_presentedDroneBasis.GetLevel().ToString());
    }

    private void InstantiateEnemies()
    {   
        _spawns = new List<Transform>();

        for (int i = 0; i < _currentPresentationData.PresentationEnemies.Length; i++)
        {
            Vector3 spawnPosition = new Vector3( _currentPresentationData.PresentationEnemies[i].EnemyLocalPosition.x,  _currentPresentationData.PresentationEnemies[i].EnemyLocalPosition.y, 0f);

            Transform newSpawn = Instantiate(_spawnPrefab, spawnPosition + transform.position, Quaternion.identity).transform;

            _spawns.Add(newSpawn);

            SpawnEnemy(i);
        }
    }

    private void SpawnEnemy(int index)
    {
        Vector3 spawnPosition = _spawns[index].position;

        if (_currentPresentationData.UseRandomizedPosition == true)
        {
            spawnPosition += new Vector3(Random.Range(-_extraRandomPositionMagnitude, _extraRandomPositionMagnitude), Random.Range(-_extraRandomPositionMagnitude, _extraRandomPositionMagnitude), 0f);
        }
        
        GameObject currentEnemy = Instantiate(GetEnemyType(index), spawnPosition, Quaternion.identity, _spawns[index]);

        EnemyHealth currentEnemyHealth = currentEnemy.GetComponent<EnemyHealth>();

        currentEnemyHealth.DeathEvent += _enemyList.RemoveEnemyFromList;

        currentEnemyHealth.DeathEvent += ReSpawnEnemy;  
        
        _enemyList.AddEnemy(currentEnemy.transform);
    }

    private GameObject GetEnemyType(int index)
    {
        switch (_currentPresentationData.PresentationEnemies[index].EnemyType)
        {
            case PresentationData.EnemyType.Small: return _smallEnemyPrefab;
            case PresentationData.EnemyType.Medium: return _mediumEnemyPrefab;
            case PresentationData.EnemyType.Big: return _bigEnemyPrefab;
            case PresentationData.EnemyType.Random:
            {
                GameObject[] enemyPrefabs = new GameObject[]{_smallEnemyPrefab, _mediumEnemyPrefab, _bigEnemyPrefab}; 
                
                return enemyPrefabs[Random.Range(0, enemyPrefabs.Length)];  
            } 
        }

        return null;
    }

    private void ReSpawnEnemy(Transform blank)
    {
        StartCoroutine(Spawn());
        IEnumerator Spawn()
        {
            yield return new WaitForSeconds(_currentPresentationData.RespawnTime);

            for (int i = 0; i < _spawns.Count; i++)
            {
                if (_spawns[i].childCount == 0) { SpawnEnemy(i); break; }
            }
        }
    }

    public void StopSession()
    {
        _presentationIsActive = false;

        StopAllCoroutines();

        Destroy(_presentedDroneBasis.gameObject);

        for (int i = 0; i < _spawns.Count; i++) 
        { 
            Destroy(_spawns[i].gameObject);
        }
    }


    public RenderTexture GetCameraTexture(int width, int height)
    {
        RenderTexture newTexture = new RenderTexture(width, height, 16);

        _camera.targetTexture = newTexture;

        return newTexture;
    }

    public void SetActive(bool state) => _camera.enabled = state;
}
