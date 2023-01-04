using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIStateManager : MonoBehaviour
{
    [SerializeField] private PlayerHealthUI playerHealthUI;

    [SerializeField] private PlayerAbilityUI playerAbilityUI;

    [SerializeField] private Animator bordersAnimator;

    public void ChangeUIToArenaState()
    {
        Main.inventoryNavigation.DisableNavigation();

        bordersAnimator.Play("BordersDisappear");
    }

    public void ChangeUIToOptionsState()
    {
        Main.inventoryNavigation.EnableNavigation();

        bordersAnimator.Play("BordersAppear");
    }
}
