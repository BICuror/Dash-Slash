using UnityEngine;
using UnityEngine.Events;

public sealed class EnemyTaskManager : MonoBehaviour
{
    [Header("AttackSettings")]

    [SerializeField] private float _warmupDelay;

    [Range(0f, 10f)] [SerializeField] private float _randomAttackPeriod;

    [SerializeField] private float _attackPeriod;

    [SerializeField] private float _timeAfterWarning;

    [SerializeField] private UnityEvent TaskBegunPrepearing;

    [SerializeField] private UnityEvent TaskStarted;

    private void Start()
    {
        Invoke("PrepeareCycle", _warmupDelay + Random.Range(0.7f, 2f));
    }

    private void PrepeareCycle()
    {
        Invoke("PrepeareCycle", _timeAfterWarning + _attackPeriod + Random.Range(0f, _randomAttackPeriod));

        PreperateTask();

        Invoke("DoTask", _timeAfterWarning);
    }

    private void PreperateTask() => TaskBegunPrepearing.Invoke();

    private void DoTask() => TaskStarted.Invoke();
}
