using UnityEngine;

[CreateAssetMenu(fileName = "MoveForwardBehaviour", menuName = "ScriptableObjects/MoveBehaviours/MoveForwardBehaviour")]

public sealed class MoveForwardBehaviour : MoveBehaviour
{
    public override Vector3 GetPosition(Transform enemyTransform) => enemyTransform.right.normalized * 2 + enemyTransform.position;
}