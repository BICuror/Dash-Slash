using UnityEngine;

public sealed class LaserTrapSpaner : DefaultTrapSpawner
{
    [SerializeField] private GameObject _laserTrap;

    [SerializeField] private bool _canSpawnDiagonalLines;

    public override void Spawn(Vector3 spawnPosition)
    {        
        float rotation = 0f; 
        
        if (_canSpawnDiagonalLines == false) rotation = 90f * Random.Range(0, 4);
        else rotation = Random.Range(0f, 360f);
        
        Instantiate(_laserTrap, spawnPosition, Quaternion.Euler(0f, 0f, rotation));
    }
}
