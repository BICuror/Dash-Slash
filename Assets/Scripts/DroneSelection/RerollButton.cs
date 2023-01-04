using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RerollButton : MonoBehaviour
{
    [SerializeField] private bool isActivated;

    [SerializeField] private Animator anim;

    public void Activate() => isActivated = true;
    public void Dectivate() => isActivated = false;

    public void Appear()
    {
        if (isActivated == true)
        {
            anim.gameObject.transform.parent.gameObject.SetActive(true);

            anim.Play("RerollButtonAppear");
        }    
    }

    public void Click()
    {
        if (Random.Range(0, 2) == 0)
        {
            ButtonDisappear();
        }   
    } 

    public void ButtonDisappear()
    {
        if (isActivated == true)
        {
            anim.Play("RerollButtonDisappear");

            Invoke("DisableButton", 0.6f);
        }
    }

    public void DisableButton()
    {
        anim.gameObject.transform.parent.gameObject.SetActive(false);
    }
}
