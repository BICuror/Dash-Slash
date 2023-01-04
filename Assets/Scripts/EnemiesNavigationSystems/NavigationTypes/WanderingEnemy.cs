using System.Collections;
using UnityEngine;

public class WanderingEnemy : MoveAgent
{
    [SerializeField] private float positionChangePeriod;

    private Vector3 targetMovePosition;

    private Vector3 targetLookPosition;

    private void Awake()
    {
        targetMovePosition = GetRandomPosition();

        StartCoroutine(ChangePosition());
    }

    private IEnumerator ChangePosition()
    {
        yield return new WaitForSeconds(positionChangePeriod);

        targetMovePosition = GetRandomPosition();

        StartCoroutine(ChangePosition());
    }

    private Vector3 GetRandomPosition()
    {
        float x = Random.Range(-Main.roomSettings.GetWidth(), Main.roomSettings.GetWidth());
        float y = Random.Range(-Main.roomSettings.GetHeight(), Main.roomSettings.GetHeight());

        return new Vector3(x, y, 0f);
    }

    public override Vector3 GetTargetMovePosition()
    {
        if (Vector3.Distance(transform.position, targetMovePosition) < 0.4f)
        {
            StopAllCoroutines();

            targetMovePosition = GetRandomPosition();
        }
        
        return targetMovePosition;
    }

    public override Vector3 GetTargetLookPosition()
    {
        return Main.playerTransform.position;
    }
}
