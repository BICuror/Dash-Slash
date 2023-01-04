using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserDashAbility : MonoBehaviour
{
    [SerializeField] Transform _sourseTransform;

    [SerializeField] float _offset;

    [SerializeField] GameObject _laserPrefab;
    
    private float _laserDamage;

    public void ObtainAbility()
    {
        Main.playerAbility.AbilityActivated += _ => Activate();
    }

    public void RemoveAbility()
    {
        Main.playerAbility.AbilityActivated -= _ => Activate();
    }

    public void SetLaserDamage(float damage) => _laserDamage = damage;
    public void SetLaserOffset(float offset) => _offset = offset; 
        
    private void Activate()
    {
        GameObject currentLaser = Instantiate(_laserPrefab, _sourseTransform.position, _sourseTransform.rotation);

        currentLaser.transform.Rotate(0, 0, 180f);

        currentLaser.transform.position = currentLaser.transform.position + -(currentLaser.transform.right * _offset);

        currentLaser.GetComponent<WeaponArea>().Setup(_laserDamage);
    }
}
