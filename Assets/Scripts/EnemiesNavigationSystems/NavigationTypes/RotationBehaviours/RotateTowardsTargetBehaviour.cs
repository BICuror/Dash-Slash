using UnityEngine;

[CreateAssetMenu(fileName = "RotateTowardsTargetBehaviour", menuName = "ScriptableObjects/RotateBehaviours/RotateTowardsTargetBehaviour")]

public sealed class RotateTowardsTargetBehaviour : RotationBehaviour
{
    public override Vector3 GetRotationPosition(Vector3 target) => target;
}
