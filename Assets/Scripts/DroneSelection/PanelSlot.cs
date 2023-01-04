using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelSlot : MonoBehaviour
{
    private Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    public void Appear()
    {   
        EnableAnimator();

        anim.Play("SlotAppear");
    }

    public void Disappear()
    {
        anim.Play("SlotDisappear");

        Invoke("DisableAnimator", 0.5f);
    }

    public void Destroy()
    {
        Disappear();
        
        Invoke("ClosePanel", 0.7f);
    }

    private void EnableAnimator()
    {
        gameObject.SetActive(true);
    }

    private void DisableAnimator()
    {
        gameObject.SetActive(false);
    }

    private void ClosePanel()
    {
        transform.GetChild(0).gameObject.GetComponent<SelectionPanel>().Close();
    }
}
