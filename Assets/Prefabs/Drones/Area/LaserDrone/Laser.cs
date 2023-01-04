using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    private float damage; 

    private bool isBouncing;

    public void Setup(float damageValue, bool bouncingState)
    {
        damage = damageValue;

        isBouncing = bouncingState;
    }

    public void DestroyArea()
    {
        Destroy(this.gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.TryGetComponent(out IDamagable health))
        {
            health.GetHurt(Main.combatStats.MultiplyDamage(damage, DroneType.Area));
        }
        else if (isBouncing == true)
        {
            if (other.gameObject.name == "downBarier" || other.gameObject.name == "upBarier")
            {
                
            }   
            else
            {
                
            }  
        }
    }
}
