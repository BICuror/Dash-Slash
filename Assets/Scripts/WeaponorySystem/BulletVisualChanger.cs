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

        if (bullet.GetHomingState() == true)
        {
            partSystem.Play();
        }
        else 
        {
            Destroy(partSystem);
        }

        if (bullet.GetPenetratingState() == true)
        {
            renderer.sprite = penetratingSprite;
        }


        Destroy(this);
    }
}
