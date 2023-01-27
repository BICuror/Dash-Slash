using UnityEngine;

public sealed class BossUnlockPointCreator : MonoBehaviour
{
    [SerializeField] private int _pointsAmount;

    [SerializeField] private GameObject _pointPrefab;

    public void CreatePoints()
    {
        for (int i = 0; i < _pointsAmount; i++)
        {
            Instantiate(_pointPrefab, gameObject.transform.position, Quaternion.Euler(0f, 0f, Random.Range(0f, 360f)));
        }
    }
}
