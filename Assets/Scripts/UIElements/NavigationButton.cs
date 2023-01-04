using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NavigationButton : MonoBehaviour
{
    [SerializeField] private Sprite[] buttonSprites;
    [SerializeField] private Image image;
    [SerializeField] private int destanation;
    
    [SerializeField] private Animator animator;

    private void OnEnable()
    {
        animator.Play("ButtonAppear");
    }

    public void Change(int navigationDestanation)
    {
        if (destanation != navigationDestanation)
        {
            destanation = navigationDestanation;

            animator.Play("ButtonDisappear");
        
            Invoke("ChangeSprite", 0.75f);
        }
        else
        {
            animator.Play("ButtonDisabled");
        }
    }

    private void ChangeSprite()
    {
        image.sprite = buttonSprites[destanation];
    }

    public void ChangeState()
    {
        Main.inventoryNavigation.ChangeNavigationState(destanation);
    }

    public void DisableButton()
    {
        animator.Play("ButtonDisappear");

        Invoke("Disable", 0.75f);
    }

    private void Disable()
    {
        gameObject.SetActive(false);
    }
}
