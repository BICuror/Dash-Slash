using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinerBullet : MonoBehaviour
{
    [SerializeField] private GameObject mine;

    private float damage;

    private float bulletSpeed;

    private Vector3 finishPosition;

    private Rigidbody2D rb;

    public void Setup(float damageValue, float speedValue, Vector3 position)
    {
        rb = GetComponent<Rigidbody2D>();

        damage = damageValue;

        bulletSpeed = speedValue;

        finishPosition = position;
    }

    private void FixedUpdate()
    {
        rb.velocity = (finishPosition - transform.position).normalized * bulletSpeed;

        if (Vector3.Distance(finishPosition, transform.position) < 0.3f)
        {
            GameObject currentMine = Instantiate(mine, transform.position, Quaternion.identity);
            
            currentMine.GetComponent<Mine>().Setup(damage);

            Destroy(gameObject);
        }
    }
}
