using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomShooter : EnemyTaskManager
{
    [Header("GunsSetting")]
    [SerializeField] private GameObject gunPrefab;
    
    [SerializeField] private int gunsAmount;

    [Range(1f, 100f)] [SerializeField] private float minimalRotationDifference;

    private Transform[] guns;

    private ShootingModule shootingModule;
 
    private void Awake()
    {
        shootingModule = GetComponent<ShootingModule>();

        Setup();
    }

    private void Setup()
    {
        guns = new Transform[gunsAmount];

        float partAngle = 360f / gunsAmount;

        for (int i = 0; i < gunsAmount; i++)
        {
            float gunRotation = Random.Range(partAngle * (i) + (minimalRotationDifference / 2), partAngle * (i + 1) - (minimalRotationDifference));

            guns[i] = Instantiate(gunPrefab, transform.position, Quaternion.Euler(0f, 0f, gunRotation)).transform;

            guns[i].SetParent(transform);
        }
    }

    protected override void PreperateTask()
    {

    }

    protected override void DoTask()
    {
        for (int i = 0; i < gunsAmount; i++)
        {
            shootingModule.ShootFromTransform(guns[i]);
        }
    }    
}
