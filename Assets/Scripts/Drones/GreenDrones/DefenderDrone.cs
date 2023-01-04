using UnityEngine;

public class DefenderDrone : DroneBasis
{
    [Header("AdditionalSettings")]

    [SerializeField] private GameObject bullet;

    [SerializeField] private float bulletSpeed;

    [SerializeField] private float damage;

    [Range(0f, 128f)] [SerializeField] private float shootingOffset;

    protected override void DoTask()
    {
        if (_enemyList.CheckIfEmpty() == false)
        {/*
            GameObject curBullet = Instantiate(bullet, this.gameObject.transform.position, Quaternion.identity);
            curBullet.transform.right = enemyList.GetEnemy().position - this.transform.position + new Vector3(Random.Range(-shootingOffset, shootingOffset), Random.Range(-shootingOffset, shootingOffset), 0f);

            curBullet.GetComponent<Rigidbody2D>().AddForce(curBullet.transform.right * bulletSpeed, ForceMode2D.Impulse);

            curBullet.GetComponent<Bullet>().damage = (int)damage;
            */
        }          
    }
    
    protected override void UpgradeDrone()
    {
        int level = GetLevel();

        switch(level)
        {
            case 1:  break;
            case 2:  break; 
            case 3:  break;
            case 4:  break;
            case 5:  break;
            default: Debug.LogError("Wrong upgrade in " + gameObject.name); break;
        }
    }
}
