using UnityEngine;

public class UnlockPointsSpawner : MonoBehaviour
{
    [SerializeField] private GameObject _unlockPoint;

    [Range(0f, 100f)] [SerializeField] private float _chanseToSpawn;

    private void Start()
    {
        Main.enemyList.EnemyDied.AddListener(CreatePoint); 
    }    

    private void CreatePoint(Vector3 position)
    {
        if (_chanseToSpawn > Random.Range(0f, 100f)) Instantiate(_unlockPoint, position, Quaternion.Euler(0f, 0f, Random.Range(0f, 360f)));
    }
}
