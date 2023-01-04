using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssaultBullet : Bullet
{
    [SerializeField] private GameObject bullet;

    [SerializeField] private float bulletSpeed;

    [SerializeField] private float timeBeforeExploding;

    private int amountOfBullets;

    private void Start()
    {
        StartCoroutine(WaitToExplode());        
    }

    public void SetAmountOfBullets(int newAmountOfBullets) {amountOfBullets = newAmountOfBullets;}

    private IEnumerator WaitToExplode()
    {
        yield return new WaitForSeconds(timeBeforeExploding);

        Explode();
    }

    private IEnumerator InstantiateBullet(float waitingTime)
    {
        yield return new WaitForSeconds(waitingTime);

        GameObject currentBullet = Instantiate(bullet, this.gameObject.transform.position, Quaternion.Euler(0f, 0f, Random.Range(0f, 360f)));
    
        Bullet currentBulletScript = currentBullet.GetComponent<Bullet>();
        
        currentBulletScript.Setup(damage, bulletSpeed);

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
    }

    private void Explode()
    {
        StopAllCoroutines();

        for (int i = 1; i < amountOfBullets + 1; i++)
        {
            StartCoroutine(InstantiateBullet(i * 0.01f));
        }

        Destroy(gameObject, 0.1f);
    }

    protected override void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.TryGetComponent(out IDamagable health))
        {
            health.GetHurt(damage);
        
            Explode();
        }
        else if (other.gameObject.tag == "Borders")
        {
            Explode();
        }
    }
}
