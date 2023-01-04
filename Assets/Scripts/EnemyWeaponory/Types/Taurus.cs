using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Taurus : EnemyTaskManager
{
    [Header("Links")]
    [SerializeField] private ParticleSystem partSystem;

    [SerializeField] private Animator anim;

    private DashingModule dashingModule;

    private MoveAgent moveAgent;

    private void Awake()
    {
        moveAgent = GetComponent<MoveAgent>();

        dashingModule = GetComponent<DashingModule>();
    }

    protected override void PreperateTask()
    {
        partSystem.Play();

        anim.Play("EnemyPrepeare");
    }

    protected override void DoTask()
    {
        moveAgent.enabled = false;

        Vector3 direction = (Main.playerTransform.position - transform.position).normalized;

        dashingModule.StartDash(direction);
    }

    protected override void StopTask()
    {
        dashingModule.StopDash();

        moveAgent.enabled = true;
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
