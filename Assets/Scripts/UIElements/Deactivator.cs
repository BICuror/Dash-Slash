using UnityEngine;

public sealed class Deactivator : MonoBehaviour
{
    [SerializeField] private GameObject[] _objectsToDeactivate;

    public void Deactivate()
    {
        for (int i = 0; i < _objectsToDeactivate.Length; i++)
        {
            _objectsToDeactivate[i].SetActive(false);
        }
    }
}
