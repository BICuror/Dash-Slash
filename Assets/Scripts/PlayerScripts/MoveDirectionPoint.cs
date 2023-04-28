using UnityEngine;

public sealed class MoveDirectionPoint : MonoBehaviour
{
    private void FixedUpdate() 
    {
        transform.localPosition = Main.playerController.GetMoveDirection();
        transform.right = Main.playerController.GetMoveDirection();
    }
}
