using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[RequireComponent(typeof(SpriteRenderer))]

public class SpriteSquasher : MonoBehaviour
{
    [Header("ScaleSettings")]

    [SerializeField] private float standartScale;

    [Range(0f, 2f)] [SerializeField] private float strengthOfXSquash;

    [Range(0f, 2f)] [SerializeField] private float strengthOfYSquash;

    [SerializeField] private bool captureWithStart;
    
    private void Start()
    {
        if (captureWithStart) standartScale = this.gameObject.transform.localScale.x;
    }

    public void Squash(Vector3 squashDirection)
    {
        this.gameObject.transform.right = squashDirection;

        float distance = Math.Abs(squashDirection.y) + Math.Abs(squashDirection.x);

        this.transform.localScale = new Vector3(standartScale + distance * strengthOfXSquash, standartScale - distance * strengthOfYSquash, standartScale);
    }
}
