using UnityEngine;

[CreateAssetMenu(fileName = "MoveToRandomPointBehaviour", menuName = "ScriptableObjects/MoveBehaviours/MoveToRandomPointBehaviour")]

public sealed class MoveToRandomPointBehaviour : MoveBehaviour
{
    [Range(0f, 2f)] [SerializeField] private float _minimalDistance;

    private Vector3 _targetMovePosition;
    
    public override Vector3 GetPosition(Transform enemyTransform)
    {
        if (Vector3.Distance(enemyTransform.position, _targetMovePosition) < _minimalDistance)
        {
            _targetMovePosition = GetRandomPosition();
        }
        
        return _targetMovePosition;
    }

    private Vector3 GetRandomPosition()
    {
        float x = Random.Range(-Main.roomSettings.GetWidth(), Main.roomSettings.GetWidth());
        float y = Random.Range(-Main.roomSettings.GetHeight(), Main.roomSettings.GetHeight());

        return new Vector3(x, y, 0f);
    }
}
