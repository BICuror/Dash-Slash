using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SluggerDrone : DroneBasis
{
    [Header("AdditionalSettings")]

    [SerializeField] private GameObject bullet;

    [SerializeField] private float bulletSpeed;

    [SerializeField] private float damage;

    [Range(0f, 128f)] [SerializeField] private float shootingOffset;

    protected override void DoTask()
    {
        if (_enemyList.CheckIfEmpty() == false)
        {
            GameObject currentBullet = Instantiate(bullet, this.gameObject.transform.position, Quaternion.identity);
        
            currentBullet.transform.right = _enemyList.GetClosestEnemy(transform.position).position - this.transform.position + new Vector3(Random.Range(-shootingOffset, shootingOffset), Random.Range(-shootingOffset, shootingOffset), 0f);
        
            Bullet currentBulletScript = currentBullet.GetComponent<Bullet>();
        
            if (Main.combatStats.singleHomingSpeed > 0f)
            {
                currentBulletScript.SetupEnemyList(_enemyList);

                currentBulletScript.SetupHoming(Main.combatStats.singleHomingSpeed);
            }

            if (Main.combatStats.singleBounceAmount > 0)
            {
                currentBulletScript.SetupEnemyList(_enemyList);
            }

            if (Main.combatStats.singleArePenetrating == true)
            {
                currentBulletScript.SetupPenetrating();
            }
            
            currentBulletScript.Setup(damage, bulletSpeed);
        }          
    }
    
    protected override void UpgradeDrone()
    {
        int level = GetLevel();

        switch(level)
        {
            case 2: restoringTime -= 0.2f; break; 
            case 3: bulletSpeed += 2f; break;
            case 4: damage += 1f; break;
            case 5: restoringTime -= 0.3f; break;
            default: Debug.LogError("Wrong upgrade in " + gameObject.name); break;
        }
    }
}
