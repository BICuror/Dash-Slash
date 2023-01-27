using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletVisualChanger : MonoBehaviour
{
    [SerializeField] private ParticleSystem partSystem;

    [SerializeField] private SpriteRenderer renderer;

    [SerializeField] private Sprite penetratingSprite;

    private void Start()
    {
        Bullet bullet = GetComponent<Bullet>();

        if (bullet.IsHoming() == true)
        {
            partSystem.Play();
        }
        else 
        {
            Destroy(partSystem);
        }

        if (bullet.IsPenetrating() == true)
        {
            renderer.sprite = penetratingSprite;
        }

        Destroy(this);
    }
}
