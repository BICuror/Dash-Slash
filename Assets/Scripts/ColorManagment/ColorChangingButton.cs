using UnityEngine;

public sealed class ColorChangingButton : MonoBehaviour
{
    private Animator _anim;

    public void Select()
    {
        _anim.SetBool("isSelected", true);
    }
    public void Unselect()
    {
        _anim.SetBool("isSelected", false);
    }
    
    private void Awake()
    {
        _anim = GetComponent<Animator>();
    }
}
