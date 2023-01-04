using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private PlayerMovment _playerMovment;
    
    public virtual Vector3 GetMoveDirection() => Vector3.zero;

    public virtual void SetActivePlayer(bool state) {}

    private void Start()
    {
        _playerMovment = GetComponent<PlayerMovment>();
    }

    private void FixedUpdate()
    {
        _playerMovment.Move(GetMoveDirection());
    }
}
