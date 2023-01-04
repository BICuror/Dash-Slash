using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dasher : MonoBehaviour
{
    [SerializeField] private ShootingModule _shootingModule;

    [SerializeField] private DashingModule _dashModule;

    [SerializeField] private Animator _anim;

    [Header("DashTowardsPlayerAttak")]

    [SerializeField] private float _timeAfterDashTowardsPlayer;

    [SerializeField] private float _dashTowardsPlayerStrength;

    [SerializeField] private float _maxNonDashDistance;

    [Header("BulletDash")]

    [SerializeField] private float _timeAfterBulletDash;

    [SerializeField] private float _dashBulletDashStrength;

    [SerializeField] private float _bullletDashBulletAmount;

    [SerializeField] private float _dashDuration;

    [Header("BulletExplotion")]

    [SerializeField] private float _timeAfterBulletExplotion;
    
    [SerializeField] private float _bulletExplotionBulletAmount;

    private void Awake() => StartCoroutine(WaitToChooseNextAttack(2f));

    private void DashTowardsPlayer()
    {
        _anim.Play("EnemyPrepeare");

        StartCoroutine(WaitToDash(_dashTowardsPlayerStrength));  
    }
    
    private IEnumerator WaitToDash(float strength)
    {
        yield return new WaitForSeconds(0.75f);

        _anim.Play("EnemyLineTask");

        _dashModule.StartDash((Main.playerTransform.position - transform.position).normalized * strength);

        _shootingModule.ShootFromVector((Main.playerTransform.position - transform.position).normalized);
        
        StartCoroutine(WaitToChooseNextAttack(_timeAfterDashTowardsPlayer));
    }

    private void BulletDash()
    {
        _anim.Play("EnemyPrepeare");

        StartCoroutine(WaitToBulletDash());
    
        IEnumerator WaitToBulletDash()
        {
            yield return new WaitForSeconds(0.75f);

            StartCoroutine(CreateBullet(0));

            _anim.Play("EnemyLineTask");

            _dashModule.StartDash((Main.roomSettings.GetRandomRoomPosition() - transform.position).normalized * _dashBulletDashStrength);

            StartCoroutine(WaitToChooseNextAttack(_timeAfterBulletDash));
        }
    }

    private IEnumerator CreateBullet(int index)
    {
        yield return new WaitForSeconds(_dashDuration / _bullletDashBulletAmount);

        _shootingModule.ShootFromVector((Main.playerTransform.position - transform.position).normalized);

        index++;

        if (index < _bullletDashBulletAmount) StartCoroutine(CreateBullet(index));
    }

    private void ShootPlayer()
    {
        _shootingModule.ShootFromVector((Main.playerTransform.position - transform.position).normalized);

        StartCoroutine(WaitToChooseNextAttack(_timeAfterBulletDash));
    }

    private void ExplodeWithBullets()
    {
        _anim.Play("EnemyPrepeare");

        StartCoroutine(WaitToExplode());

        IEnumerator WaitToExplode()
        {
            yield return new WaitForSeconds(1f);

            for (int i = 0; i < _bulletExplotionBulletAmount; i++)
            {
                _shootingModule.ShootFromVector(((Main.playerTransform.position - transform.position).normalized + new Vector3(Random.Range(-0.1f, 0.1f), Random.Range(-0.1f, 0.1f), 0f).normalized).normalized);
            }

            StartCoroutine(WaitToChooseNextAttack(_timeAfterBulletExplotion));
        }
    }

    private IEnumerator WaitToChooseNextAttack(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);

        ChooseNextAttack();
    }

    private void ChooseNextAttack()
    {
        _shootingModule.ShootFromVector((Main.playerTransform.position - transform.position).normalized);

        if (Vector3.Distance(transform.position, Main.playerTransform.position) > _maxNonDashDistance)
        {
            DashTowardsPlayer(); return;
        }

        int rand = Random.Range(0, 2);

        switch(rand)
        {
            case 0: BulletDash(); break;
            case 1: ExplodeWithBullets(); break;
        }
    }

    public void StopAttacking()
    {
        StopAllCoroutines();
    }
}
