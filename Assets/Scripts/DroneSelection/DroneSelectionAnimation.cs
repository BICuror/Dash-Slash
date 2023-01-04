using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;

public class DroneSelectionAnimation : MonoBehaviour
{
    [SerializeField] private PanelSlot[] slots;

    [SerializeField] private float waitingTime;

    public void SelectionAppear()
    {
        StopAllCoroutines();

        for (int i = 0; i < slots.Length; i++)
        {
            StartCoroutine(SlotAppear(i));
        }
    }
    private IEnumerator SlotAppear(int index)
    {
        yield return new WaitForSeconds(index * waitingTime);

        slots[index].Appear();
    }

    public void SelectionDisappear()
    {
        StopAllCoroutines();

        for (int i = 0; i < slots.Length; i++)
        {
            StartCoroutine(SlotDisappear(i));
        }
    }
    
    private IEnumerator SlotDisappear(int index)
    {
        yield return new WaitForSeconds(index * waitingTime);

        slots[index].Disappear();
    }

    public void EndSelection()
    {
        StopAllCoroutines();

        for (int i = 0; i < slots.Length; i++)
        {
            StartCoroutine(DestroySlot(i));
        }
    }
    
    private IEnumerator DestroySlot(int index)
    {
        yield return new WaitForSeconds(index * waitingTime);

        slots[index].Destroy();
    }
}
