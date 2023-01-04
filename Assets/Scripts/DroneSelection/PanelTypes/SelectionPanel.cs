using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class SelectionPanel : MonoBehaviour
{
    [Header("Links")]
    
    [SerializeField] private Animator animator;

    private bool isChosen;
 
    public virtual void Close() => Debug.LogError("Close method wasn't overdrived in dronePanel");

    public void Deactivate() => animator.Play("SlotDisabled");
    

    public void Deselect()
    {
        if (isChosen == true)
        {
            animator.Play("Deselect");
            
            isChosen = false;
        } 
    }

    public void Click()
    {
        if (isChosen == true)
        {
            Select();

            animator.Play("SelectedSlotDisabled");
        } 
        else
        {
            Main.droneSelector.DeselectAllPanels();
         
            isChosen = true;

            animator.Play("Select");
        }
    }

    protected virtual void Select() => Debug.LogError("Select method wasn't overdrived in dronePanel");

    protected void SendForm(bool isObtained, string name)
    {
        string obtained = "";

        if (isObtained) obtained = "true"; else obtained = "false";

        StartCoroutine(obtained, name);
    }
}
