using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrubEnemyHealth : EnemyHealth
{
    [Range(1, 50)] [SerializeField] private int segmentsAmount;

    [SerializeField] private float speedPerSegment;

    [SerializeField] private float startInvincibilityTime;
 
    private float healthPerSegment;

    private EnemyColorManager colorManager;

    private Grub grub;

    private bool isInvisible = true;

    private MoveAgent navigation;

    private void Awake()
    {
        healthPerSegment = maxHealth / segmentsAmount;

        grub = GetComponent<Grub>();

        colorManager = GetComponent<EnemyColorManager>();

        statusManager = GetComponent<EnemyStatus>();

        navigation = GetComponent<MoveAgent>();

        Invoke("StopInvinsibility", startInvincibilityTime);
    }

    private void StopInvinsibility()
    {
        isInvisible = false;
    }

    public override void GetHurt(float damage)
    {
        if (isInvisible == true) return;
 
        damage = MultyplyDamageByStatus(damage);

        currentHealth -= damage;

        navigation.SetMoveSpeed(speedPerSegment * grub.GetActiveSegmentsCount());

        if (currentHealth < healthPerSegment * grub.GetActiveSegmentsCount())
        {
            if (grub.GetActiveSegmentsCount() != 3) 
            {
                currentHealth = healthPerSegment * grub.GetActiveSegmentsCount();

                grub.DestroyLastSegment();

                statusManager.HighlightEnemy();
            }
            else   
            {
                Die();
            }
        }
        else 
        {
            currentHealth -= damage;

            statusManager.HighlightEnemy();            
        }
    }

    public override void Die()
    {
        grub.DestroyAllSegments();
        DestroyEnemy();     
    }

    public int GetMaxSegmentsCount()
    {
        return segmentsAmount;
    }
}
