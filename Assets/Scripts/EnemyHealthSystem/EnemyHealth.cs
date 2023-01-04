using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour, IDamagable
{
    [Header("MainStats")]

    [SerializeField] protected float maxHealth; 
    public float maxHP { get {return maxHealth;} set {maxHealth = value;} }

    protected float currentHealth;
    
    protected EnemyStatus statusManager;

    public delegate void EventHandler(Transform enemyTransform); 
    public event EventHandler DeathEvent;

    private void Awake() 
    {
        currentHealth = maxHealth;

        statusManager = GetComponent<EnemyStatus>();
    }

    public void SetMaxHealth(float health)
    {
        maxHealth = health;

        currentHealth = health;
    }

    public void MultiplyMaxHelath(float multiplier)
    {
        maxHealth *= multiplier;

        currentHealth = maxHealth;
    }

    public virtual void GetHurt(float damage) {}

    protected bool AntioneshotCheck(float damage)
    {
        return (damage >= maxHealth && currentHealth == maxHealth);
    }

    protected float MultyplyDamageByStatus(float damage)
    {
        if (statusManager.IsOnFire() == true) damage += damage * Main.combatStats.additionalDamageToOnFireEnemies;
        if (statusManager.IsShocked() == true) damage += damage * Main.combatStats.additionalDamageToShokedEnemies;

        return damage;
    }

    public virtual void GetPercentHurt(float percent)
    {
        GetHurt(maxHealth * percent);
    }

    public virtual void Die() 
    {
        DestroyEnemy(); 

        RemoveFromList();
    }

    protected void DestroyEnemy()
    {
        RemoveFromList();

        Destroy(gameObject);    
    }

    protected void RemoveFromList()
    {
        if (DeathEvent != null)
        {
            DeathEvent.Invoke(transform);

            DeathEvent = null;
        } 
    }
}
 