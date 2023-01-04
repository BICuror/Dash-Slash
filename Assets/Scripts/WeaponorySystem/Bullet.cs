using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private DroneType _type;

    [SerializeField] protected bool isPenetrating;

    [SerializeField] protected GameObject bulletParticles;

    [SerializeField] private bool canBeSplitted;

    [HideInInspector] protected float damage;

    // homing settings
    protected Rigidbody2D rb;

    protected float speed;

    protected float homingSpeed;

    protected EnemyList enemyList;

    protected Transform targetTransform;

    // bounce settings

    private int leftBounces;

    public void Setup(float newDamageValue, float newSpeedValue) 
    {
        speed = newSpeedValue;
        
        damage = newDamageValue; 

        rb = GetComponent<Rigidbody2D>();
        
        rb.AddForce(transform.right * speed, ForceMode2D.Impulse);

        StartCoroutine(DestroyAfter(4f));

        // bounceSettings

        if (Main.combatStats.singleBounceAmount > 0) leftBounces = Main.combatStats.singleBounceAmount;
    }

    private IEnumerator DestroyAfter(float time)
    {
        yield return new WaitForSeconds(time);
        
        DestroyBullet(transform.position);
    }

    public void SetupBounce(int bounces)
    {
        leftBounces = bounces;
    }

    public void SetupHoming(float rotationSpeed)
    {
        homingSpeed = rotationSpeed;

        StartCoroutine(Homing());
    }

    public bool GetHomingState()
    {
        return (homingSpeed > 0);
    }

    public void SetupPenetrating()
    {
        isPenetrating = true;
    }

    public bool GetPenetratingState()
    {
        return isPenetrating;
    }

    public void SetupEnemyList(EnemyList currentEnemyList)
    {
        enemyList = currentEnemyList;
    }
 
    private IEnumerator Homing()
    {
        yield return new WaitForFixedUpdate();

        if (targetTransform == null) 
        {
            if (enemyList.GetAmountOfEnemies() != 0)
            {
                targetTransform = enemyList.GetClosestEnemy(transform.position);
            }
            else
            {
                rb.angularVelocity = 0f;
            }
        } 
        else
        {
            float rotationAmount = Vector3.Cross((targetTransform.position - transform.position).normalized, transform.right).z;

            rb.angularVelocity = -rotationAmount * homingSpeed;

            rb.velocity = transform.right * speed;
        }
        
        StartCoroutine(Homing());
    }

    protected virtual void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.TryGetComponent(out IDamagable health))
        {
            health.GetHurt(Main.combatStats.MultiplyDamage(damage, _type));
            
            if (leftBounces > 0) 
            {
                Transform newTarget = enemyList.GetClosestEnemyToEnemy(other.transform);
            
                if (newTarget != null)
                {
                    Bounce(newTarget);
                }
            }
            else if (isPenetrating == false)
            {
                DestroyBullet((this.transform.position + other.gameObject.transform.position) / 2);
            }
        
            if (isPenetrating == false) other.gameObject.GetComponent<IKnockbackable>().KnockBack(transform.position, 2f);
        }
        else if (other.gameObject.tag == "Borders")
        {
            if (leftBounces > 0)
            {
                Transform newTarget = enemyList.GetClosestEnemyToEnemy(transform);
            
                if (newTarget != null)
                {
                    Bounce(transform);
                }
                else
                {
                    DestroyBullet(transform.position);
                }
            }
            else
            {
                DestroyBullet(transform.position);
            }
        }
    }

    private void Bounce(Transform enemy)
    {
        Transform newTarget = enemyList.GetClosestEnemyToEnemy(enemy);
            
        if (newTarget != null)
        {
            transform.right = newTarget.position - transform.position;

            rb.velocity = transform.right * speed;
        }

        leftBounces--;
    }

    protected void DestroyBullet(Vector3 spawnPosition)
    {
        Instantiate(bulletParticles, spawnPosition, Quaternion.identity);

        Destroy(this.gameObject);
    }
}
