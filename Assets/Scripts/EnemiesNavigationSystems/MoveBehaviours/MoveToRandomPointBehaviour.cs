using UnityEngine;

[CreateAssetMenu(fileName = "MoveToRandomPointBehaviour", menuName = "ScriptableObjects/MoveBehaviours/MoveToRandomPointBehaviour")]

public sealed class MoveToRandomPointBehaviour : MoveBehaviour
{
    [Range(0f, 2f)] [SerializeField] private float _minimalDistance = 1f;

    private Vector3 _targetMovePosition;
    
    public override Vector3 GetPosition(Transform enemyTransform)
    {
        if (Vector3.Distance(enemyTransform.position, _targetMovePosition) < _minimalDistance)
        {
            SetNewRandomPosition();
        }
        
        return _targetMovePosition;
    }

    public void SetNewRandomPosition()
    {
        _targetMovePosition = Main.roomSettings.GetRandomRoomPosition();
    }
}
