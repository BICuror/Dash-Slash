using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagingArea : MonoBehaviour
{
    [SerializeField] private float damage;
 
    public void SetDamage(float damageValue)
    {
        damage = damageValue;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.TryGetComponent(out IDamagable health))
        {
            health.GetHurt(Main.combatStats.MultiplyDamage(damage, DroneType.None));
        }
    }
}
