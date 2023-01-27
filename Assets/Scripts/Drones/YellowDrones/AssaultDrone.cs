using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssaultDrone : DroneBasis
{
    [Header("AdditionalSettings")]
    
    [SerializeField] private GameObject bullet;

    [SerializeField] private float bulletSpeed;

    [SerializeField] private float damage;

    [SerializeField] private int amountOfBullets;

    protected override void DoTask()
    {
        if (_enemyList.CheckIfEmpty() == false)
        {
            GameObject currentBullet = Instantiate(bullet, this.gameObject.transform.position, Quaternion.identity);
            
            currentBullet.transform.right = _enemyList.GetClosestEnemy(transform.position).position - this.transform.position;
            
            AssaultBullet currentBulletScript = currentBullet.GetComponent<AssaultBullet>();

            currentBulletScript.SetAmountOfBullets(amountOfBullets);
        
            currentBulletScript.CheckAndSetupEffects();
            
            currentBulletScript.Setup(damage, bulletSpeed, _enemyList);
        }          
    }
    
    protected override void UpgradeDrone()
    {
        int level = GetLevel();

        switch(level)
        {
            case 2: amountOfBullets += 2; break; 
            case 3: damage += 2f; break;
            case 4: amountOfBullets += 2; break;
            case 5: restoringTime -= 0.4f; break;
            default: Debug.LogError("Wrong upgrade in " + gameObject.name); break;
        }
    }
}
