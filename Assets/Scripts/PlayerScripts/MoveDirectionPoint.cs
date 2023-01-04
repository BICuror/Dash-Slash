using UnityEngine;

public sealed class MoveDirectionPoint : MonoBehaviour
{
    private void Update()
    {
        transform.localPosition = Main.playerController.GetMoveDirection();
    }
}
