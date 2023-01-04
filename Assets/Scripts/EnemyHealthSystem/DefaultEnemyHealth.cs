
using UnityEngine;

public sealed class DefaultEnemyHealth : EnemyHealth
{
    [Header("Links")]

    [SerializeField] protected ParticleSystem _hurtParticleSystem;

    public override void GetHurt(float damage)
    {
        _hurtParticleSystem.Play();

        damage = MultyplyDamageByStatus(damage);

        if (AntioneshotCheck(damage))
        {
            currentHealth = 1;

            statusManager.HighlightEnemy();    
        }
        else 
        {
            currentHealth -= damage;
            
            if (currentHealth <= 0f) 
            {
                DestroyEnemy();
            }
            else
            {
                statusManager.HighlightEnemy();   
            }
        }
    }
}
