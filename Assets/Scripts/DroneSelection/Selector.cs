using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public sealed class Selector : MonoBehaviour
{
    [SerializeField] private Transform[] _selectionPanelSlots;

    [Header("RerollSettings")]
    [SerializeField] private RerollButton rerollButton;

    [Header("PanelSettings")]
    [SerializeField] private int panelsAmount;

    [SerializeField] private DroneSelectionAnimation animator;

    [SerializeField] private PanelCreator _panelCreator;

    private int availableToUpgradeDronesCount;

    private List<SelectionPanel> panels; 

    public UnityEvent OptionChoosen;

    public void ActivateSelection()
    {
        rerollButton.Appear();

        StartSelection();
    }

    public void StartSelection()
    {
        panels = new List<SelectionPanel>();

        animator.SelectionAppear();

        _panelCreator.UpdateSelectionStats();

        for (int i = 0; i < panelsAmount; i++)
        {
            StartCoroutine(CreatePanel(i));;
        }
    }

    private IEnumerator CreatePanel(int index)
    {   
        yield return new WaitForSeconds(0.15f * index);

        GameObject panel = _panelCreator.InstantiatePanel(_selectionPanelSlots[index]);        

        panels.Add(panel.GetComponent<SelectionPanel>());
    }

    public void CloseSelector()
    {
        OptionChoosen.Invoke();
    }

    public void ClosePanels()
    {
        OptionChoosen.Invoke();

        animator.EndSelection();

        rerollButton.ButtonDisappear();

        DeactivatePanels();
    }

    public void DeselectAllPanels()
    {
        for (int i = 0; i < panelsAmount; i++)
        {
            panels[i].Deselect();
        }
    }

    public void SetActive(bool state)
    {
        DeselectAllPanels();

        if (state == true) animator.SelectionAppear();
        else 
        {
            animator.SelectionDisappear();

            DeactivatePanels();
        }
    }

    private void DeactivatePanels()
    {
        for (int i = 0; i < panelsAmount; i++) 
        {
            panels[i].Deactivate();
        }
    }

    public void RerollPanels()
    {
        DeactivatePanels();

        animator.EndSelection();

        StartCoroutine(Wait());
        IEnumerator Wait()
        {
            yield return new WaitForSeconds(0.75f);

            GameObject[] projectiles = GameObject.FindGameObjectsWithTag("Projectile");
            foreach (GameObject projectile in projectiles) { Destroy(projectile); }

            StartSelection();
        }
    }
}
