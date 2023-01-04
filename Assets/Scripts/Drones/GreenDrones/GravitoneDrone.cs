using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravitoneDrone : DroneBasis
{
    [Header("AdditionalSettings")]

    [SerializeField] private GameObject bullet;

    [SerializeField] private float _areaDamage;

    [SerializeField] private float bulletSpeed;

    [Range(0f, 128f)] [SerializeField] private float shootingOffset;

    [SerializeField] private Material holeMaterial;

    private void Start()
    {
        holeMaterial.SetColor("MainColor", Main.colorManager.GetDefenceColor());
    }

    protected override void DoTask()
    {
        if (_enemyList.CheckIfEmpty() == false)
        {
            GameObject curBullet = Instantiate(bullet, this.gameObject.transform.position, Quaternion.identity);
            curBullet.transform.right = _enemyList.GetClosestEnemy(transform.position).position - this.transform.position + new Vector3(Random.Range(-shootingOffset, shootingOffset), Random.Range(-shootingOffset, shootingOffset), 0f);

            curBullet.GetComponent<Rigidbody2D>().AddForce(curBullet.transform.right * bulletSpeed, ForceMode2D.Impulse);

            curBullet.GetComponent<GravitoneBullet>().SetDamage(_areaDamage);
        }          
    }
    
    protected override void UpgradeDrone()
    {
        int level = GetLevel();

        switch(level)
        {
            case 2: bulletSpeed *= 1.5f;  break; 
            case 3: shootingOffset = 0f; _areaDamage += 3f; break;
            case 4: restoringTime -= 0.4f; break;
            case 5: bulletSpeed *= 1.5f; _areaDamage += 3f; break;
            default: Debug.LogError("Wrong upgrade in " + gameObject.name); break;
        }
    }
}
