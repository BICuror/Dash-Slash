using UnityEngine;

public sealed class DrifterEnemy : MoveAgent
{
    public override Vector3 GetTargetMovePosition() => transform.position + transform.right;

    public override Vector3 GetTargetLookPosition() => Main.playerTransform.position;
    
    protected override void RotateTowards(Vector2 direction)
    {
        Vector3 currentRotation = transform.right;

        Vector3 newRotation = Vector3.Lerp(direction, currentRotation, _rotationSpeed).normalized;

        float angle = Mathf.Atan2(newRotation.y, newRotation.x) * Mathf.Rad2Deg;
        
        transform.rotation = Quaternion.Euler(0f, 0f, angle);
    }
}
