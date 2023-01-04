using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrubSegment : MonoBehaviour, IShockable, IDamagable, IKnockbackable, IBurnable
{
    private IShockable shockable;

    private IBurnable burnable;

    private EnemyHealth enemyHealth;

    private Rigidbody2D rb;

    public void Setup(GameObject headSegment)
    {
        rb = gameObject.GetComponent<Rigidbody2D>();

        shockable = headSegment.GetComponent<IShockable>();

        enemyHealth = headSegment.GetComponent<EnemyHealth>();

        burnable = headSegment.GetComponent<IBurnable>();
    }

    public void GetHurt(float damage)
    {
        enemyHealth.GetHurt(damage);
    }

    public void GetPercentHurt(float percent)
    {
        enemyHealth.GetPercentHurt(percent);
    }

    public void DestroySegment() 
    {
        if (enemyHealth != null)
        enemyHealth.gameObject.GetComponent<Grub>().RemoveSegment(gameObject);

        Destroy(gameObject);    
    }

    public void Shock(float stunDuration)
    {
        shockable.Shock(stunDuration);
    }

    public void SetOnFire()
    {
        burnable.SetOnFire();
    }

    public void KnockBack(Vector3 knockBackSourcePosition, float knockBackStrength) 
    {
        rb.AddForce((knockBackSourcePosition - transform.position).normalized * -knockBackStrength, ForceMode2D.Impulse);   
    }
}
