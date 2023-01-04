using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knight : EnemyTaskManager
{
    [SerializeField] private Animator anim;

    [SerializeField] private ParticleSystem partSystem;
 
    private ShootingModule shootingModule;

    private MoveAgent agent;

    private void Awake()
    {
        agent = GetComponent<MoveAgent>();

        shootingModule = GetComponent<ShootingModule>();
    }

    protected override void PreperateTask() 
    {
        agent.Stun(timeAfterWarning * 1.25f);

        anim.Play("EnemyPrepeare");

        partSystem.Play();
    }

    protected override void DoTask()
    {
        agent.KnockBack(transform.right, 5f);

        anim.Play("EnemyLineTask");

        shootingModule.ShootFromVector(transform.right + transform.up * 0.5f);
        shootingModule.ShootFromVector(transform.right);
        shootingModule.ShootFromVector(transform.right + transform.up * -0.5f);
    }  
}
