using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DroneInventory : MonoBehaviour
{
    [SerializeField] private Transform parentTransform;
    [SerializeField] private GameObject inventoryPanel;
    [SerializeField] private PresentationManager presentationManager;

    [SerializeField] private GameObject inventoryDronePanel;
    [SerializeField] private GameObject emptyInventoryDronePanel;

    [SerializeField] private GameObject inventoryInfoPanel;

    [Header("TextFields")]

    [SerializeField] private TextMeshProUGUI nameTextField;
    
    [SerializeField] private TextMeshProUGUI levelTextField;

    [SerializeField] private TextMeshProUGUI descriptionTextField;

    [SerializeField] private TextMeshProUGUI upgradeTextField;

    private DroneBasis displayedDrone;

    [SerializeField] private DroneInventoryRefreshAnimator refreshAnimator;

    [SerializeField] private Animator anim;

    private List<DroneInventoryPanel> panels;

    public void OpenInventory()
    {
        anim.Play("DroneInventoryAppear");

        SetupSlots();
    }

    private void SetupSlots()
    {
        panels = new List<DroneInventoryPanel>();

        int droneAmount = Main.droneContainer.GetAmountOfDrones();

        int cellsAmonut = Main.droneContainer.GetAmountOfCells();

        for (int i = 0; i < cellsAmonut; i++)
        {
            if (i < droneAmount)
            {
                panels.Add(Instantiate(inventoryPanel, Vector3.zero, Quaternion.identity, parentTransform).GetComponent<DroneInventoryPanel>());

                panels[i].GetComponent<DroneInventoryPanel>().Setup(Main.droneContainer.GetDrone(i));
            }
            else
            {
                Instantiate(emptyInventoryDronePanel, Vector3.zero, Quaternion.identity, parentTransform);
            }
        }

        CalculatePanelHeight(cellsAmonut);
    }

    private void CalculatePanelHeight(int slots)
    {
        float height = (slots * 270f) + ((slots - 1) * 20f);

        parentTransform.gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(0f, height);
    }

    public void DestroyPanels()
    {
        for (int i = 0; i < parentTransform.childCount; i++)
        {
            Destroy(parentTransform.GetChild(i).gameObject);
        }
    }

    public void CloseInventory()
    {
        if (displayedDrone != null)
        {
            refreshAnimator.StartRefreshing();
        }

        displayedDrone = null;

        anim.Play("DroneInventoryDisappear");
    }

    public void SetDrone(DroneBasis newDrone)
    {
        if (displayedDrone != newDrone && refreshAnimator.GetRefreshPossibility() == true)
        {
            displayedDrone = newDrone;

            refreshAnimator.StartRefreshing();

            foreach (DroneInventoryPanel panel in panels)
            {
                panel.ClosePanel();
            }
        } 
    }

    public void DisactivateButtonsForRefresh()
    {
        for (int i = 0; i < panels.Count; i++)
        {
            panels[i].DisableForChange();
        }
    }

    public void DestroyPanel(DroneBasis drone)
    {
        for (int i = 0; i < parentTransform.childCount; i++)
        {
            Destroy(parentTransform.GetChild(i).gameObject);
        }

        //Main.droneContainer.DestroyDrone(drone.GetDroneData(), drone.GetLevel());

        SetupSlots();

        displayedDrone = null;

        refreshAnimator.StartRefreshing();
    }

    public void ClearPanel()
    {
        if (displayedDrone == null)
        {
            inventoryInfoPanel.SetActive(false);

            nameTextField.text = "";
            descriptionTextField.text = "";
            levelTextField.text = "";
            upgradeTextField.text = ""; 

            presentationManager.StopSession();
        }
    }

    public void RefreshDisplayDrone()
    {
        if (displayedDrone != null)
        {
            inventoryInfoPanel.SetActive(true);

            presentationManager.StartSession(displayedDrone, displayedDrone.GetLevel());

            nameTextField.text = displayedDrone.GetDroneData().Name;

            descriptionTextField.text = displayedDrone.GetDroneData().Description;

            levelTextField.text = displayedDrone.GetLevel().ToString(); 

            SetColor(displayedDrone.gameObject.GetComponent<SpriteRenderer>().material.color);

            DisplayUpgrades();
        }
    }

    private void SetColor(Color newColor)
    {
        levelTextField.color = newColor;
    }

    private void DisplayUpgrades()
    {   /*
        string upgradesText = "";

        for(int i = 1; i < 5; i++)
        {
            upgradesText += (i).ToString() + " > " + (i + 1).ToString() + ": " + displayedDrone.GetUpgradeDescription(i) + "@";
        }

        upgradesText = upgradesText.Replace("@", System.Environment.NewLine);

        upgradeTextField.text = upgradesText;*/
    }
}
