using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserBullet : MonoBehaviour
{
    private float lifetime = 5f;

    private Rigidbody2D rb;

    private int damage;
    
    public void Setup(int newDamage) 
    {
        damage = newDamage;

        rb = GetComponent<Rigidbody2D>();

        Destroy(gameObject, lifetime);

        Invoke("StartDisappearing", lifetime - 0.75f);
    }

    private void StartDisappearing()
    {
        GetComponent<Animator>().Play("LaserBulletDisappear");
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.gameObject.TryGetComponent(out IDamagable health))
        {
            health.GetHurt(Main.combatStats.MultiplyDamage(damage, DroneType.Area));

            other.gameObject.GetComponent<IKnockbackable>().KnockBack(transform.position, 2f);
        }
        else if (other.gameObject.name == "downBarier" || other.gameObject.name == "upBarier")
        {
            rb.velocity = new Vector2(rb.velocity.x, -rb.velocity.y); 
        }   
        else
        {
            rb.velocity = new Vector2(-rb.velocity.x, rb.velocity.y); 
        }  
    }
}
