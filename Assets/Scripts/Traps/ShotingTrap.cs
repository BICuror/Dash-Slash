using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotingTrap : MonoBehaviour
{
    [SerializeField] private float[] zRotationToShoot;

    [SerializeField] private float timeBeforeShooting;

    [SerializeField] private GameObject bullet;

    [SerializeField] private float bulletSpeed;

    private void Awake()
    {
        StartCoroutine(Shoot());
    }

    private IEnumerator Shoot()
    {
        yield return new WaitForSeconds(timeBeforeShooting);

        for (int i = 0; i < zRotationToShoot.Length; i++)
        {
            Transform currentBullet = Instantiate(bullet, transform.position, transform.rotation).transform;
            currentBullet.Rotate(0f, 0f, zRotationToShoot[i]);
            currentBullet.GetComponent<Rigidbody2D>().AddForce(currentBullet.right * bulletSpeed, ForceMode2D.Impulse);
        }

        Destroy(this);
    }
}
