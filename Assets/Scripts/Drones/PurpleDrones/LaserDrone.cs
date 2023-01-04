using UnityEngine;

public class LaserDrone : DroneBasis
{
    [Header("AdditionalSettings")]
    
    [SerializeField] private GameObject laserBeam;

    [SerializeField] private float damage;

    [Range(0f, 128f)] [SerializeField] private float shootingOffset;

    protected override void DoTask()
    {
        if (_enemyList.CheckIfEmpty() == false)
        {
            GameObject curBullet = Instantiate(laserBeam, gameObject.transform.position, Quaternion.identity);
            curBullet.transform.right = _enemyList.GetEnemy().position - transform.position + new Vector3(Random.Range(-shootingOffset, shootingOffset), Random.Range(-shootingOffset, shootingOffset), 0f);

            curBullet.GetComponent<WeaponArea>().Setup(damage);
        }          
    }
    
    protected override void UpgradeDrone()
    {
        int level = GetLevel();

        switch(level)
        {
            case 2: damage += 6f; break; 
            case 3: restoringTime -= 0.4f; break;
            case 4: restoringTime -= 0.4f; break;
            case 5: damage += 6f; break;
            default: Debug.LogError("Wrong upgrade in " + gameObject.name); break;
        }
    }
}
