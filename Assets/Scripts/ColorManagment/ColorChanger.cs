using UnityEngine;
using UnityEngine.UI;

public sealed class ColorChanger : MonoBehaviour
{
    [Header("UiLinks")]
    [SerializeField] private Slider _redSlider;
    [SerializeField] private Slider _greenSlider;
    [SerializeField] private Slider _blueSlider;

    [SerializeField] private ColorChangingButton[] _colorSelectButtons;

    [Header("ColorLinks")]
    [SerializeField] private TypeColor _currentTypeColor;

    public void UpdateCurrentColor()
    {
        Color currentColor = new Color(_redSlider.value, _greenSlider.value, _blueSlider.value);

        _currentTypeColor.SetColor(currentColor);
    }

    public void UpdateSliders()
    {
        Color currentColor = _currentTypeColor.GetColor();

        _redSlider.value = currentColor.r;
        _greenSlider.value = currentColor.g;
        _blueSlider.value = currentColor.b; 
    }

    public void UnselectAllButtons()
    {
        for (int i = 0; i < _colorSelectButtons.Length; i++)
        {
            _colorSelectButtons[i].Unselect();
        }
    }

    public void SetTypeColor(TypeColor typeColor) => _currentTypeColor = typeColor; 
}
