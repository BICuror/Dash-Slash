using UnityEngine;
using TMPro;

public sealed class JostickSideChanger : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _sideText; 

    private void Start()
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

        SetSideText(side);
    }

    public void ChangeSide()
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

        if (side == "right") side = "left";
        else side = "right";

        PlayerPrefs.SetString("MainJoystickSide", side);

        SetSideText(side);
    }

    private void SetSideText(string side)
    {
        _sideText.text = side;
    }
}
