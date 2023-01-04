using UnityEngine;

public sealed class DeadBodyCreator : MonoBehaviour
{
    [SerializeField] private GameObject _enemyDeadBody;

    public void CreateDeadBody(Vector3 position)
    {
        Instantiate(_enemyDeadBody, position, Quaternion.identity);
    }
}
