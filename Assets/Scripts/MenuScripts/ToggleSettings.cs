using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public sealed class ToggleSettings : MonoBehaviour
{
    private bool _state;

    public UnityEvent<bool> StateChanged;

    [SerializeField] private Image _image;

    [SerializeField] private Sprite _activeSprite;
    [SerializeField] private Sprite _notActiveSprite;

    public void ChangeToOtherState() => SetState(!_state);

    public void SetState(bool state)
    {
        _state = state;

        StateChanged.Invoke(_state);

        SetImageState();
    }

    private void SetImageState()
    {
        if (_state) _image.sprite = _activeSprite;
        else _image.sprite = _notActiveSprite;
    }
}
