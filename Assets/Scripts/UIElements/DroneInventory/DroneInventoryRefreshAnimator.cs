using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DroneInventoryRefreshAnimator : MonoBehaviour
{
    private bool canBeRefreshed = true;

    [SerializeField] private Animator anim;

    [SerializeField] private UnityEvent refreshEvent;

    public void StartRefreshing()
    {
        gameObject.SetActive(true);

        if (canBeRefreshed == true)
        {
            canBeRefreshed = false;

            Invoke("Refresh", 0.5f);

            anim.Play("RefreshInventory");
        }        
    }

    public bool GetRefreshPossibility()
    {
        return canBeRefreshed;
    }

    public void Refresh()
    {
        canBeRefreshed = true;
    }

    public void RefreshPanel()
    {
        refreshEvent.Invoke();
    }
}
