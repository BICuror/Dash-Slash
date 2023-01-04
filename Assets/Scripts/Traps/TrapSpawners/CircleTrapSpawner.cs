using UnityEngine;

public sealed class CircleTrapSpawner : DefaultTrapSpawner
{
    [SerializeField] private GameObject _regularCircleTrap;

    [SerializeField] private GameObject _upgradedCircleTrap;

    [SerializeField] private float _chanceToSpawnUpgradedTrap;


    public override void Spawn(Vector3 spawnPosition)
    {
        if (Random.Range(0f, 100f) > _chanceToSpawnUpgradedTrap) Instantiate(_regularCircleTrap, spawnPosition, Quaternion.identity);
        else Instantiate(_upgradedCircleTrap, spawnPosition, Quaternion.identity);
    } 
}
