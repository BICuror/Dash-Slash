using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    [SerializeField] private ShootingModule _shootingModule;

    [SerializeField] private DashingModule _dashModule;

    [SerializeField] private Animator _anim;

    [Header("DashAttack")]

    [SerializeField] private float _dashDuration;

    [SerializeField] private float _bulletSpeed;

    [SerializeField] private int _bulletAmount;

    [SerializeField] private GameObject _bulletPrefab;

    [SerializeField] private float _timeAfterDash;

    [Header("TeleportDash")]

    [SerializeField] private GameObject _shooterBulletPrefab;

    [SerializeField] private float _shooterBulletSpeed;

    [SerializeField] private float _teleportDuration; 

    [SerializeField] private float _timeAfterTeleport;

    [SerializeField] private float _randomDelayTime;

    private void Awake() => StartCoroutine(WaitToChooseNextAttack(2f));

    private void BulletDash()
    {
        _anim.Play("EnemyPrepeare");

        StartCoroutine(WaitToBulletDash());
    
        IEnumerator WaitToBulletDash()
        {
            yield return new WaitForSeconds(0.75f);

            StartCoroutine(CreateBullet(0));

            _anim.Play("EnemyLineTask");

            StartCoroutine(WaitToChooseNextAttack(_timeAfterDash));
        }
    }

    private IEnumerator CreateBullet(int index)
    {
        yield return new WaitForSeconds(_dashDuration / _bulletAmount);

        if (Random.Range(0, 100) > 5) _shootingModule.ShootFromVector((Main.playerTransform.position - transform.position).normalized);
        else ShootTeleportBullet();

        index++;

        if (index < _bulletAmount) StartCoroutine(CreateBullet(index));
    }

    private void ShootRandomTeleportBullet()
    {
        GameObject currentBullet = Instantiate(_shooterBulletPrefab, transform.position, Quaternion.identity);

        currentBullet.transform.right = (Main.playerTransform.position - transform.position).normalized;

        currentBullet.GetComponent<Rigidbody2D>().AddForce(currentBullet.transform.right * _shooterBulletSpeed, ForceMode2D.Impulse);

        currentBullet.GetComponent<ShooterBullet>().HitSomething.AddListener(StartDashing);

        StartCoroutine(WaitToChooseNextAttack(_randomDelayTime));
    }

    private void ShootTeleportBullet()
    {
        GameObject currentBullet = Instantiate(_shooterBulletPrefab, transform.position, Quaternion.identity);

        currentBullet.transform.right = (Main.playerTransform.position - transform.position).normalized;

        currentBullet.GetComponent<Rigidbody2D>().AddForce(currentBullet.transform.right * _shooterBulletSpeed, ForceMode2D.Impulse);

        currentBullet.GetComponent<ShooterBullet>().HitSomething.AddListener(StartDashing);
    }

    private void StartDashing(Vector3 position)
    {
        StopAllCoroutines();

        StartCoroutine(Teleport(0, position));
    }

    private IEnumerator Teleport(float elapsedTime, Vector3 position)
    {
        yield return new WaitForFixedUpdate();

        elapsedTime += Time.deltaTime;

        transform.position = Vector3.Lerp(transform.position, position, elapsedTime / _teleportDuration);

        if (elapsedTime < _teleportDuration) StartCoroutine(Teleport(elapsedTime, position));
        else
        {
            StopAllCoroutines();

            StartCoroutine(WaitToChooseNextAttack(_timeAfterTeleport));
        }
    }

    private IEnumerator WaitToChooseNextAttack(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);

        ChooseNextAttack();
    }

    private void ChooseNextAttack()
    {
        int rand = Random.Range(0, 3);

        switch(rand)
        {
            case 0: ShootTeleportBullet(); break;
            case 1: BulletDash(); break;
            case 2: ShootRandomTeleportBullet(); break;
        }
    }

    public void StopAttacking()
    {
        StopAllCoroutines();
    }
}