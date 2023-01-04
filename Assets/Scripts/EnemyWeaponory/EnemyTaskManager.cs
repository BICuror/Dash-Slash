using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTaskManager : MonoBehaviour
{
    [Header("AttackSettings")]

    [Constant] [Range(0f, 10f)] [SerializeField] protected float randomAttackPeriod;

    [Constant] [SerializeField] protected float attackPeriod;

    [Constant] [SerializeField] protected float timeAfterWarning;

    private void Start()
    {
        Invoke("PrepeareCycle", Random.Range(0.7f, 2f));
    }

    private void PrepeareCycle()
    {
        Invoke("PrepeareCycle", timeAfterWarning + attackPeriod + Random.Range(0f, randomAttackPeriod));

        PreperateTask();

        Invoke("DoTask", timeAfterWarning);
    }

    protected virtual void PreperateTask()
    {
        Debug.LogError("Prepeare in EnemyTaskManager has not been overridden");
    }

    protected virtual void DoTask()
    {
        Debug.LogError("StartTask in EnemyTaskManager has not been overridden");
    }

    protected virtual void StopTask()
    {
        Debug.LogError("StopTask in EnemyTaskManager has not been overridden");
    }
}
