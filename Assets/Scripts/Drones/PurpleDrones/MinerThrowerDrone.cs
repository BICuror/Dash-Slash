using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinerThrowerDrone : DroneBasis
{
    [Header("AdditionalSettings")]
    
    [SerializeField] private GameObject bullet;

    [SerializeField] private float bulletSpeed;

    [SerializeField] private float damage;

    protected override void DoTask()
    {
        if (_enemyList.CheckIfEmpty() == false)
        {
            GameObject currentBullet = Instantiate(bullet, this.gameObject.transform.position, Quaternion.identity);
            
            currentBullet.transform.right = _enemyList.GetClosestEnemy(transform.position).position - this.transform.position;
            
            currentBullet.GetComponent<MinerBullet>().Setup(damage, bulletSpeed, _enemyList.GetEnemy().position);
        }          
    }
    
    protected override void UpgradeDrone()
    {
        int level = GetLevel();

        switch(level)
        {
            case 2: damage += 10f; break; 
            case 3: damage += 10f; break;
            case 4: bulletSpeed *= 2f; break;
            case 5: damage += 10f; break;
            default: Debug.LogError("Wrong upgrade in " + gameObject.name); break;
        }
    }
}
