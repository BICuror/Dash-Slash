using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sniper : EnemyTaskManager
{
    [SerializeField] private ParticleSystem partSystem;
    
    [SerializeField] private Animator anim;

    private ShootingModule shootingModule;

    private MoveAgent agent;


    private void Awake()
    {
        agent = GetComponent<MoveAgent>();

        shootingModule = GetComponent<ShootingModule>();
    }

    protected override void PreperateTask() 
    {
        agent.Stun(1f);

        anim.Play("EnemyPrepeare");
    }

    protected override void DoTask()
    {
        partSystem.Play();

        anim.Play("EnemyLineTask");

        agent.KnockBack(transform.right, 8f);

        shootingModule.ShootFromVector(transform.right);
    }  
}
