using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryNavigation : MonoBehaviour
{
    [SerializeField] private NavigationButton mainButton;

    [SerializeField] private NavigationButton additionalButton;

    private NavigationState currentState = NavigationState.optionsPanel;

    public enum NavigationState
    {
        optionsPanel,
        droneInventory,
        perkInventory
    }

    public void ChangeNavigationState(int state)
    {
        switch (state)
        {
            case 0: ChangeNavigationStateToOptionsPanel(); break;
            case 1: ChangeNavigationStateToDroneInventoryPanel(); break;
            case 2: ChangeNavigationStateToPerkInventoryPanel(); break;
        }
    }

    private void ChangeNavigationStateToOptionsPanel()
    {
        if (currentState != NavigationState.optionsPanel)
        {
            StartCoroutine(Wait());
            IEnumerator Wait()
            {
                yield return new WaitForSeconds(0.75f);
                Main.droneSelector.SetActive(true);
            }
            

            Close();

            mainButton.Change((int)NavigationState.droneInventory);

            additionalButton.Change((int)NavigationState.perkInventory);

            currentState = NavigationState.optionsPanel;
        } 
    }

    private void ChangeNavigationStateToPerkInventoryPanel()
    {
        if (currentState != NavigationState.perkInventory)
        {
            StartCoroutine(Wait());
            IEnumerator Wait()
            {
                yield return new WaitForSeconds(0.75f);
                Main.perkInventory.OpenInventory();
            }

            Close();

            mainButton.Change((int)NavigationState.optionsPanel);

            additionalButton.Change((int)NavigationState.droneInventory);

            currentState = NavigationState.perkInventory;
        } 
    }

    private void ChangeNavigationStateToDroneInventoryPanel()
    {
        if (currentState != NavigationState.droneInventory)
        { 
            StartCoroutine(Wait());
            IEnumerator Wait()
            {
                yield return new WaitForSeconds(0.75f);
                Main.droneInventory.OpenInventory();
            }

            Close();

            mainButton.Change((int)NavigationState.optionsPanel);

            additionalButton.Change((int)NavigationState.perkInventory);

            currentState = NavigationState.droneInventory;
        }    
    }

    private void Close()
    {
        switch (currentState)
        {
            case NavigationState.optionsPanel: Main.droneSelector.SetActive(false); break;
            case NavigationState.droneInventory: Main.droneInventory.CloseInventory(); break;
            case NavigationState.perkInventory: Main.perkInventory.CloseInventory(); break;
        }
    }

    public void DisableNavigation()
    {
        mainButton.DisableButton();

        additionalButton.DisableButton();
    }

    public void EnableNavigation()
    {
        mainButton.gameObject.SetActive(true);

        additionalButton.gameObject.SetActive(true);
    }
}
