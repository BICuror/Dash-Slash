using UnityEngine;

public sealed class JoystickSideLoader : MonoBehaviour
{
    [SerializeField] private RectTransform _abilityJoystick;

    [SerializeField] private RectTransform _movmentJoystick;

    [SerializeField] private Transform _leftJoystickContainer;

    [SerializeField] private Transform _rightJoystickContainer;

    private void Start() => LoadJoystickSide();

    public void LoadJoystickSide()
    {
        string side;

        if (PlayerPrefs.HasKey("MainJoystickSide"))
        {
            side = PlayerPrefs.GetString("MainJoystickSide");
        }
        else 
        {
            side = "right";
        }

        SetJoysticks(side);
    }

    private void SetJoysticks(string mainSide)
    {
        if (mainSide != "right")
        {
            (_abilityJoystick.position, _movmentJoystick.position) = (_movmentJoystick.position, _abilityJoystick.position); 
        
            _abilityJoystick.SetParent(_leftJoystickContainer);
            _movmentJoystick.SetParent(_rightJoystickContainer);
        }
    }
}
