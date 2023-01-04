using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovment : MonoBehaviour
{
    [SerializeField] private float speed;

    private float defaultSpeed;

    [SerializeField] private SpriteSquasher spriteSquasher;

    private void Awake()
    {
        defaultSpeed = speed;
    }

    public void Move(Vector3 direction)
    {
        spriteSquasher.Squash(direction);

        this.transform.position = this.transform.position + (direction * speed); 
    }

    public void StopMoving(float time)
    {
        StartCoroutine(StartMoving(time, speed));

        speed = 0f;
    }

    private IEnumerator StartMoving(float time, float prevSpeed)
    {
        yield return new WaitForSeconds(time);

        speed = prevSpeed;
    }

    public void SetSpeedMultiplier(float multiplier)
    {
        speed = defaultSpeed * multiplier;
    }
}