using UnityEngine;

public class Spawner : MonoBehaviour
{
    private SpawnerModule _spawnerModule;

    private void Awake() => _spawnerModule = GetComponent<SpawnerModule>();
    
    public void SpawnEnemy()
    {
        _spawnerModule.SpawnEnemy(transform.position);
    }  
}
