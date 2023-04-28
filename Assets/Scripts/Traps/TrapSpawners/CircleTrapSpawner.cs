using UnityEngine;

public sealed class CircleTrapSpawner : DefaultTrapSpawner
{
    [SerializeField] private GameObject _regularCircleTrap;

    [SerializeField] private GameObject _upgradedCircleTrap;

    [Range(0f, 100f)] [SerializeField] private float _difficultyToSpawnUpgradedTrap;

    public override void Spawn(Vector3 spawnPosition, float difficulty)
    {
        if (difficulty < _difficultyToSpawnUpgradedTrap) Instantiate(_regularCircleTrap, spawnPosition, Quaternion.identity);
        else Instantiate(_upgradedCircleTrap, spawnPosition, Quaternion.identity);
    } 
}
