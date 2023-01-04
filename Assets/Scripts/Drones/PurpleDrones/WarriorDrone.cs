using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarriorDrone : DroneBasis
{
    [Header("AdditionalSettings")]
    
    [SerializeField] private GameObject area;

    [SerializeField] private float damage;

    [Range(0f, 128f)] [SerializeField] private float shootingOffset;

    [SerializeField] private float scale = 1f;

    protected override void DoTask()
    {
        if (_enemyList.CheckIfEmpty() == false)
        {
            GameObject curBullet = Instantiate(area, this.gameObject.transform.position, Quaternion.identity);
            curBullet.transform.right = _enemyList.GetClosestEnemy(transform.position).position - this.transform.position + new Vector3(Random.Range(-shootingOffset, shootingOffset), Random.Range(-shootingOffset, shootingOffset), 0f);

            curBullet.GetComponent<WeaponArea>().Setup(damage);

            curBullet.transform.localScale = new Vector3(scale, scale, scale);
        }          
    }
    
    protected override void UpgradeDrone()
    {
        int level = GetLevel();

        switch(level)
        {
            case 2: scale += 0.25f; damage += 5f; break; 
            case 3: damage += 5f; break;
            case 4: restoringTime -= 0.5f; break;
            case 5: scale += 0.5f; damage += 5f; break;
            default: Debug.LogError("Wrong upgrade in " + gameObject.name); break;
        }
    }
}
