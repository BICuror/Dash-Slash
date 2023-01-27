using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MisslesAbility : MonoBehaviour
{
    [SerializeField] Transform _sourseTransform;

    [SerializeField] GameObject _misslePrefab;

    [SerializeField] private float _abilityDuration;
    
    [SerializeField] private float _missleSpeed;

    private int _missleAmount;

    private float _missleDamage;

    public void ObtainAbility()
    {
        Main.playerAbility.AbilityActivated += _ => Activate();
    }

    public void RemoveAbility()
    {
        Main.playerAbility.AbilityActivated -= _ => Activate();
    }

    public void SetMissleDamage(float damage) => _missleDamage = damage;
    public void SetMissleAmount(int amount) => _missleAmount = amount;

    private void Activate()
    {
        float delay = _abilityDuration / _missleAmount;

        for (int i = 1; i < _missleAmount + 1; i++)
        {
            StartCoroutine(CreateMissle(delay * i, _sourseTransform.up));
            StartCoroutine(CreateMissle(delay * i, -_sourseTransform.up));
        }
    }

    private IEnumerator CreateMissle(float delay, Vector3 direction)
    {
        yield return new WaitForSeconds(delay);

        GameObject currentBullet = Instantiate(_misslePrefab, _sourseTransform.position, Quaternion.identity);
        
        currentBullet.transform.right = direction;
        
        Bullet currentBulletScript = currentBullet.GetComponent<Bullet>();
        
        currentBulletScript.SetupHoming(360f);
                
        currentBulletScript.Setup(_missleDamage, _missleSpeed, Main.enemyList);
    }
}
