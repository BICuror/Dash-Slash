using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlamethrowerDrone : DroneBasis
{
    [Header("AdditionalSettings")]

    [SerializeField] private GameObject[] _flames;

    [SerializeField] private float bulletSpeed;

    [SerializeField] private float _lifetime;

    [Range(0f, 128f)] [SerializeField] private float shootingOffset;

    protected override void DoTask()
    {
        if (_enemyList.CheckIfEmpty() == false)
        {
            float rotation = Random.Range(0f, 360f);

            GameObject currentBullet = Instantiate(_flames[Random.Range(0, _flames.Length)], this.gameObject.transform.position, Quaternion.Euler(0, 0, rotation));
        
            currentBullet.transform.right = _enemyList.GetClosestEnemy(transform.position).position - this.transform.position + new Vector3(Random.Range(-shootingOffset, shootingOffset), Random.Range(-shootingOffset, shootingOffset), 0f);
        
            currentBullet.GetComponent<FlameBullet>().SetLifetime(_lifetime);

            currentBullet.GetComponent<Rigidbody2D>().AddForce(currentBullet.transform.right * bulletSpeed, ForceMode2D.Impulse);
        }          
    }
    
    protected override void UpgradeDrone()
    {
        int level = GetLevel();

        /*switch(level)
        {
            case 2: restoringTime -= 0.07f; break; 
            case 3: bulletSpeed += 2f; break;
            case 4: damage += 1f; break;
            case 5: restoringTime -= 0.1f; break;
            default: Debug.LogError("Wrong upgrade in " + gameObject.name); break;
        }*/
    }
}
