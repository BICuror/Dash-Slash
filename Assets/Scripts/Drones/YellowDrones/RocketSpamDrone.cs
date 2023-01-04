using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketSpamDrone : DroneBasis
{
    [Header("AdditionalSettings")]

    [SerializeField] private GameObject bullet;

    [SerializeField] private float damage;

    [SerializeField] private int shotingCycles;

    [SerializeField] private float spamBulletSpeed;

    protected override void DoTask()
    {
        if (_enemyList.CheckIfEmpty() == false)
        {
            GameObject currentBullet = Instantiate(bullet, this.gameObject.transform.position, Quaternion.identity);
        
            currentBullet.transform.right = _enemyList.GetEnemy().position - this.transform.position;

            currentBullet.GetComponent<SpamRocketBullet>().Setup(damage, shotingCycles, _enemyList, spamBulletSpeed);
        }          
    }


    
    protected override void UpgradeDrone()
    {
        int level = GetLevel();

        switch(level)
        {
            case 2: shotingCycles += 2; break; 
            case 3: damage += 2f;break;
            case 4: damage += 2f; break;
            case 5: shotingCycles += 2; break;
            default: Debug.LogError("Wrong upgrade in " + gameObject.name); break;
        }
    }
}
