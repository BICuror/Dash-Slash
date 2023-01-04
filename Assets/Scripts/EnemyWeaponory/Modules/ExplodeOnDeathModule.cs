using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplodeOnDeathModule : EnemyHealth
{
    [Header("Links")]
    [SerializeField] protected ParticleSystem partSystem;

    [SerializeField] private Animator anim;

    [Header("ExplosionSettings")]
    [SerializeField] private int amountOfBullets;
    [SerializeField] private GameObject bullet;
    [SerializeField] private float bulletSpeed;

    [SerializeField] private ParticleSystem explosionParticleSystem;

    public override void GetHurt(float damage)
    { 
        damage = MultyplyDamageByStatus(damage);

        currentHealth -= damage;

        partSystem.Play();

        if (AntioneshotCheck(damage))
        {
            currentHealth = 1f;

            statusManager.HighlightEnemy();
        }
        else 
        {
            currentHealth -= damage;

            if (currentHealth <= 0f) 
            {
                Die();
            }
            else
            {
                statusManager.HighlightEnemy();
            }
        }
    }

    public override void Die()
    {
        explosionParticleSystem.Play();
            RemoveFromList();

            GetComponent<Rigidbody2D>().velocity = new Vector2(0f, 0f);

            anim.Play("EnemyExplode");

            Destroy(GetComponent<MoveAgent>());

            Destroy(GetComponent<CircleCollider2D>());

            Invoke("Explode", 1.5f);
        
    }

    private void Explode()
    {
        float angle = 360f / amountOfBullets;

        for (int i = 0; i < amountOfBullets; i++)
        {
            GameObject curBullet = Instantiate(bullet, this.gameObject.transform.position, Quaternion.Euler(0f, 0f, angle * i));

            curBullet.GetComponent<Rigidbody2D>().AddForce(curBullet.transform.right * bulletSpeed, ForceMode2D.Impulse);
        }

        Destroy(gameObject);
    }
}
