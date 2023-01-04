using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chaser : EnemyTaskManager
{
    [Header("Links")]
    [SerializeField] private ParticleSystem partSystem;

    [SerializeField] private Animator anim;

    private DashingModule dashingModule;

    private void Awake()
    {
        dashingModule = GetComponent<DashingModule>();
    }

    protected override void PreperateTask()
    {
        partSystem.Play();

        anim.Play("EnemyPrepeare");
    }

    protected override void DoTask()
    {
        anim.Play("EnemyShortDash");

        dashingModule.StartDash(transform.right);
    }

    protected override void StopTask()
    {
        dashingModule.StopDash();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Borders"))
        {
            StopTask();

            partSystem.Play();
        }
    }
}
