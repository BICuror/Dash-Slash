using UnityEngine;

public sealed class WallMoveTrapSpawner : DefaultTrapSpawner
{   
    [SerializeField] private GameObject _horizontalWall;

    [SerializeField] private GameObject _verticalWall;

    public override void Spawn(Vector3 spawnPosition, float difficulty)
    {
        GameObject wallMoveTrapToSpawn;

        if (GetRandomBool())
        {
            spawnPosition.x = 0;
            spawnPosition.y /= -2f;
            
            wallMoveTrapToSpawn = _horizontalWall;
        }   
        else
        {
            spawnPosition.x /= -2f;
            spawnPosition.y = 0f;

            wallMoveTrapToSpawn = _verticalWall;
        }

        if (GetRandomBool()) Instantiate(wallMoveTrapToSpawn, spawnPosition, Quaternion.Euler(0f, 0f, 0f));
        else Instantiate(wallMoveTrapToSpawn, spawnPosition, Quaternion.Euler(0f, 0f, -180f));
    }
}
