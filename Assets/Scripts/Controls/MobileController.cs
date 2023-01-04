using UnityEngine;

public sealed class MobileController : PlayerController
{
    [Header("PhoneMovmentScheme")]
    
    [SerializeField] private VariableJoystick _movmentJoystick;

    [SerializeField] private Joystick _abilityJoystick;

    public override Vector3 GetMoveDirection()
    {
        Vector3 moveDirection = Vector3.up * _movmentJoystick.Vertical + Vector3.right * _movmentJoystick.Horizontal;
        
        if (moveDirection.magnitude > 1f) moveDirection = moveDirection.normalized;

        return moveDirection;
    }

    public override void SetActivePlayer(bool state)
    {
        Main.playerTransform.gameObject.SetActive(state);

        Main.droneContainer.gameObject.SetActive(state);

        _movmentJoystick.gameObject.SetActive(state);

        _abilityJoystick.gameObject.SetActive(state);
    }
}
