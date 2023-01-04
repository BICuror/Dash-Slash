using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flag : MonoBehaviour
{
    private float increase;

    public void Setup(float multiplier)
    {
        increase = multiplier;
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Main.combatStats.allDamage = (Main.combatStats.allDamage + increase);
        }    
    }

    private void OnTriggerExit2D(Collider2D other) 
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Main.combatStats.allDamage = (Main.combatStats.allDamage - increase);
        }    
    }
}
