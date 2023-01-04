using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleporter : EnemyTaskManager
{
    [SerializeField] private float teleportDuration;

    [SerializeField] private Animator anim;

    private TeleportModule teleportModule;

    private MoveAgent agent;

    private void Awake()
    {
        teleportModule = GetComponent<TeleportModule>();

        agent = GetComponent<MoveAgent>();
    }

    protected override void PreperateTask()
    {
        agent.Stun(teleportDuration);

        anim.Play("EnemyPrepeare");
    }

    protected override void DoTask()
    {
        Vector3 randomPosition = new Vector3(Random.Range(-Main.roomSettings.GetWidth(), Main.roomSettings.GetWidth()), Random.Range(-Main.roomSettings.GetHeight(), Main.roomSettings.GetHeight()), 0f);

        teleportModule.Teleport(randomPosition, teleportDuration);
    }
}