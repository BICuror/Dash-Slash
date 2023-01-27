using UnityEngine;

public sealed class DestroyIt : MonoBehaviour
{
    [SerializeField] private float timeBeforeDestroying;

    private void Awake()
    {
        Destroy(gameObject, timeBeforeDestroying);
    }
}
