using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BouncerDrone : DroneBasis
{
    [Header("AdditionalSettings")]

    [SerializeField] private GameObject bullet;

    [SerializeField] private float bulletSpeed;

    [SerializeField] private float damage;

    protected override void DoTask()
    {
        if (_enemyList.CheckIfEmpty() == false)
        {
            GameObject curBullet = Instantiate(bullet, this.gameObject.transform.position, Quaternion.identity);
            curBullet.transform.right = _enemyList.GetEnemy().position - this.transform.position;

            curBullet.GetComponent<Rigidbody2D>().AddForce(curBullet.transform.right * bulletSpeed, ForceMode2D.Impulse);

            curBullet.GetComponent<LaserBullet>().Setup((int)damage);
        }          
    }
    
    protected override void UpgradeDrone()
    {
        int level = GetLevel();

        switch(level)
        {
            case 2: damage += 8f; break; 
            case 3: damage += 10f; break;
            case 4: damage += 8f; break;
            case 5: damage += 10f; break;
            default: Debug.LogError("Wrong upgrade in " + gameObject.name); break;
        }
    }
}
