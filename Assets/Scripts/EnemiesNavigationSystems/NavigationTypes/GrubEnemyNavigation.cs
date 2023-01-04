using UnityEngine;
using System.Collections;

public sealed class GrubEnemyNavigation : MoveAgent
{
   /* [SerializeField] private float _positionChangePeriod;

    private Vector3 _targetMovePosition;

    private void Awake()
    {
        _targetMovePosition = GetRandomPosition();

        float angle = Mathf.Atan2(_targetMovePosition.y, _targetMovePosition.x) * Mathf.Rad2Deg;
        
        transform.rotation = Quaternion.Euler(0f, 0f, angle);

        StartCoroutine(ChangePosition());
    }

    private IEnumerator ChangePosition()
    {
        yield return new WaitForSeconds(_positionChangePeriod);

        _targetMovePosition = GetRandomPosition();

        StartCoroutine(ChangePosition());
    }

    private Vector3 GetRandomPosition()
    {
        float x = Random.Range(-Main.roomSettings.GetWidth() + 2f, Main.roomSettings.GetWidth() - 2f);
        float y = Random.Range(-Main.roomSettings.GetHeight() + 2f, Main.roomSettings.GetHeight() - 2f);

        return new Vector3(x, y, 0f);
    }

    public override Vector3 GetTargetMovePosition()
    {
        if (Vector3.Distance(transform.position, _targetMovePosition) < 0.4f)
        {
            StopAllCoroutines();

            _targetMovePosition = GetRandomPosition();
        }
        
        return transform.position + transform.right;
    }

    public override Vector3 GetTargetLookPosition() => _targetMovePosition;
    */
}
