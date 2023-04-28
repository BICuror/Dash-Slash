using UnityEngine;

public sealed class WallTrapSpawner : DefaultTrapSpawner
{
    [SerializeField] private GameObject _regularWallTrap;

    [SerializeField] private GameObject _upgradedWallTrap;

    [Range(0f, 100f)] [SerializeField] private float _difficultyToSpawnUpgradedTrap;

    public override void Spawn(Vector3 spawnPosition, float difficulty)
    {
        float rotation = 0f;

        if (GetRandomBool())
        {
            if (GetRandomBool()) { spawnPosition.x = Main.roomSettings.GetWidth(); rotation = 90f; }
            else { spawnPosition.x = -Main.roomSettings.GetWidth(); rotation = -90f; }
        }
        else
        {
            if (GetRandomBool()) { spawnPosition.y = Main.roomSettings.GetHeight(); rotation = 180f; }
            else { spawnPosition.y = -Main.roomSettings.GetHeight(); rotation = 0f; }
        }
            
        if (difficulty < _difficultyToSpawnUpgradedTrap) Instantiate(_regularWallTrap, spawnPosition, Quaternion.Euler(0f, 0f, rotation));
        else Instantiate(_upgradedWallTrap, spawnPosition, Quaternion.Euler(0f, 0f, rotation));
    }   
}
