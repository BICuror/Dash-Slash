using UnityEngine;

public class ShootingModule : MonoBehaviour
{
    [Header("ShootingSettings")]

    [SerializeField] private GameObject bullet;

    [SerializeField] private float bulletSpeed;

    public void ShootFromTransform(Transform shootFrom)
    {
        GameObject currentBullet = Instantiate(bullet, transform.position, shootFrom.rotation);

        currentBullet.GetComponent<Rigidbody2D>().AddForce(shootFrom.right * bulletSpeed, ForceMode2D.Impulse);
    }    

    public void ShootFromVector(Vector3 shootVector)
    {
        GameObject currentBullet = Instantiate(bullet, transform.position, Quaternion.identity);

        currentBullet.transform.right = shootVector;

        currentBullet.GetComponent<Rigidbody2D>().AddForce(shootVector * bulletSpeed, ForceMode2D.Impulse);
    }  
}
