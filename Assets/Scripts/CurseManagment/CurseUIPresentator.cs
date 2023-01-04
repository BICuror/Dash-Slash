using UnityEngine;
using UnityEngine.UI;
using TMPro;

public sealed class CurseUIPresentator : MonoBehaviour
{
    [Header("Presentation")]
    [SerializeField] private TextMeshProUGUI _curseNameField;

    [SerializeField] private Image _curseIconImage;

    [SerializeField] private Animator _anim;


    public void ShowCurse(CurseData currentCurseData)
    {
        _curseNameField.text = currentCurseData.Name;

        _curseIconImage.sprite = currentCurseData.Icon;

        _anim.Play("ShowCurse");
    }
}
