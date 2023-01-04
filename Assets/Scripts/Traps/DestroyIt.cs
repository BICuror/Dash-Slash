using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyIt : MonoBehaviour
{
    [SerializeField] private float timeBeforeDestroying;

    private void Awake()
    {
        Destroy(gameObject, timeBeforeDestroying);
    }
}
