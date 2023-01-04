using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportPosition : MonoBehaviour
{
    [SerializeField] private SpriteRenderer spriteRenderer;

    public void Setup(Sprite sprite)
    {
        spriteRenderer.sprite = sprite;
    }

    private void FixedUpdate()
    {
        Vector3 direction = (Main.playerTransform.position - transform.position).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        this.transform.rotation = Quaternion.Euler(0f, 0f, angle);
    }
}
