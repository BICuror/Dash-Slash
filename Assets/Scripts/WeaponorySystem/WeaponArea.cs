using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponArea : MonoBehaviour
{
    [SerializeField] private float timeBeforeDestroyed;

    [SerializeField] private DroneType _type;

    private float damage = 3f; 

    public void Setup(float damageValue)
    {
        damage = damageValue;

        if (timeBeforeDestroyed != 0f) Destroy(this.gameObject, timeBeforeDestroyed);
    }

    public void DestroyArea()
    {
        Destroy(this.gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.TryGetComponent(out IDamagable health))
        {
            health.GetHurt(Main.combatStats.MultiplyDamage(damage, _type));
        }
    }
}
