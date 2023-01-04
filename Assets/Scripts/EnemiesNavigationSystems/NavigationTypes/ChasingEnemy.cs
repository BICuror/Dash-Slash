using UnityEngine;

public sealed class ChasingEnemy : MoveAgent
{
    public override Vector3 GetTargetMovePosition() => Main.playerTransform.position;
    
    public override Vector3 GetTargetLookPosition() => Main.playerTransform.position;
}
