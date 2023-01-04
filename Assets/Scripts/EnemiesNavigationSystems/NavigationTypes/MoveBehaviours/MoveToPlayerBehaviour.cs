using UnityEngine;

[CreateAssetMenu(fileName = "MoveToPlayerBehaviour", menuName = "ScriptableObjects/MoveBehaviours/MoveToPlayerBehaviour")]

public sealed class MoveToPlayerBehaviour : MoveBehaviour
{
    public override Vector3 GetPosition(Transform enemyTransform) => Main.playerTransform.position;
}