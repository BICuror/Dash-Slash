using UnityEngine;

[CreateAssetMenu(fileName = "RotateTowardsPlayerBehaviour", menuName = "ScriptableObjects/RotateBehaviours/RotateTowardsPlayerBehaviour")]

public sealed class RotateTowardsPlayerBehaviour : RotationBehaviour
{
    public override Vector3 GetRotationPosition(Vector3 target) => Main.playerTransform.position;
}
