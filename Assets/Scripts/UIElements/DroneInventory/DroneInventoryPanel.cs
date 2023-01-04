using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DroneInventoryPanel : MonoBehaviour
{
    [Header("TextFields")]

    [SerializeField] private TextMeshProUGUI nameTextField;

    [SerializeField] private TextMeshProUGUI levelTextField;

    [SerializeField] private TextMeshProUGUI destroyText;

    [SerializeField] private Image renderer;

    [SerializeField] private DroneInventoryRefreshAnimator animator;

    [Header("Links")]

    [SerializeField] private GameObject infoPanel;

    [SerializeField] private GameObject destroyPanel;

    [SerializeField] private GameObject justificationPanel;

    private DroneBasis drone;

    private Button button;

    private enum PanelState 
    {
        infoPanel,
        destroyPanel,
        justificationPanel
    }

    private PanelState currentState = PanelState.infoPanel;

    public void Setup(DroneBasis newDrone)
    {
        button = gameObject.GetComponent<Button>();

        drone = newDrone;

        nameTextField.text = drone.GetDroneData().Name;

        levelTextField.text = drone.GetLevel().ToString();

        destroyText.text = "Destroy " + drone.GetDroneData().Name + "?";

        renderer.sprite = drone.GetComponent<SpriteRenderer>().sprite;

        renderer.material = drone.GetComponent<SpriteRenderer>().material;

        Color droneColor = drone.gameObject.GetComponent<SpriteRenderer>().material.color;

        levelTextField.color = droneColor;  
    }

    public void Click()
    {
        if (animator.GetRefreshPossibility() == true)
        {
            if (currentState == PanelState.infoPanel)
            {
                Main.droneInventory.SetDrone(drone);

                currentState = PanelState.destroyPanel;
            }
            else if (currentState == PanelState.destroyPanel)
            {
                currentState = PanelState.justificationPanel;
            }
            else if (currentState == PanelState.justificationPanel)
            {
                Main.droneInventory.DestroyPanel(drone);
            } 
            
            Main.droneInventory.DisactivateButtonsForRefresh();

            animator.StartRefreshing();
        }
    }

    public void ClosePanel()
    {
        if (currentState != PanelState.infoPanel)
        {
            animator.StartRefreshing();

            currentState = PanelState.infoPanel;
        }   
    }

    public void Display()
    {
        if (animator.GetRefreshPossibility() == true)
        {
            Main.droneInventory.SetDrone(drone);
        }
    }

    public void ChangeState()
    {
        justificationPanel.SetActive(false);
        infoPanel.SetActive(false);
        destroyPanel.SetActive(false);

        switch (currentState)
        {
            case PanelState.infoPanel: infoPanel.SetActive(true); break;
            case PanelState.justificationPanel: justificationPanel.SetActive(true); break;
            case PanelState.destroyPanel: destroyPanel.SetActive(true); break;
        }
    }

    public void DisableForChange()
    {   
        button.interactable = false;

        StartCoroutine(WaitToActivate());
        IEnumerator WaitToActivate()
        {
            yield return new WaitForSeconds(0.5f);

            button.interactable = true;
        }
    }
}
