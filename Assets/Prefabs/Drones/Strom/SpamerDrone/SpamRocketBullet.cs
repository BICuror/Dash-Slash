using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpamRocketBullet : MonoBehaviour
{
    [SerializeField] private GameObject bullet;

    [SerializeField] private int spawnSycles;

    [SerializeField] private int amountOfBulletsPerHit;
    
    [SerializeField] private float timeBeforeStartingExploding;

    [SerializeField] private float delayTime;

    [SerializeField] private int amountOfBulletsUponExplosion;

    [SerializeField] private float bulletSpeed;

    [SerializeField] private float rotationSpeed;

    private float bulletDamage;

    private EnemyList enemyList;

    private Rigidbody2D rb;

    public void Setup(float damageValue, int spawnSyclesValue, EnemyList newEnemyList, float thisBulletSpeed)
    {
        rb = GetComponent<Rigidbody2D>();

        rb.AddForce(transform.right * thisBulletSpeed, ForceMode2D.Impulse);

        bulletDamage = damageValue;

        spawnSycles = spawnSyclesValue;

        enemyList = newEnemyList;

        StartCoroutine(Shoot());
    }

    private IEnumerator Shoot()
    {
        yield return new WaitForSeconds(delayTime);

        float angle = 360f / amountOfBulletsPerHit;

        rb.angularVelocity = rotationSpeed;

        for (int i = 0; i < amountOfBulletsPerHit; i++)
        {
            GameObject currentBullet = Instantiate(bullet, this.gameObject.transform.position, transform.rotation);

            currentBullet.transform.Rotate(0f, 0f, angle * i);

            SetupBullet(currentBullet);
        }

        if (--spawnSycles > 0) StartCoroutine(Shoot());
        else StartCoroutine(Explode());

    }
    
    private IEnumerator Explode()
    {
        yield return new WaitForSeconds(delayTime * 3f);

        float angle = 360f / amountOfBulletsUponExplosion;

        for (int i = 0; i < amountOfBulletsUponExplosion; i++)
        {
            GameObject currentBullet = Instantiate(bullet, this.gameObject.transform.position, transform.rotation);

            currentBullet.transform.Rotate(0f, 0f, angle * i);

            SetupBullet(currentBullet);
        }

        Destroy(gameObject);
    }

    private void SetupBullet(GameObject currentBullet)
    {
        Bullet currentBulletScript = currentBullet.GetComponent<Bullet>();
        
        if (Main.combatStats.singleHomingSpeed > 0f)
        {
            currentBulletScript.SetupEnemyList(enemyList);

            currentBulletScript.SetupHoming(Main.combatStats.singleHomingSpeed);
        }

        if (Main.combatStats.singleBounceAmount > 0)
        {
            currentBulletScript.SetupEnemyList(enemyList);
        }

        if (Main.combatStats.singleArePenetrating == true)
        {
            currentBulletScript.SetupPenetrating();
        }
            
        currentBulletScript.Setup(bulletDamage, bulletSpeed);
    }
}
