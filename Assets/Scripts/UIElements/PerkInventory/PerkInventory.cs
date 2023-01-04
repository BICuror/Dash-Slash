using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PerkInventory : MonoBehaviour
{
    [SerializeField] private GameObject _upgradablePerkPanelPrefab;
    [SerializeField] private GameObject _singlePerkPanelPrefab;

    [SerializeField] private Transform parentObject;

    [SerializeField] private Animator panelAnimator;

    private List<PerkInventoryPanel> _perkInventoryPanels = new List<PerkInventoryPanel>(); 

    public int GetPerksAmount() => _perkInventoryPanels.Count;

    public void AddPerkInventoryPanel(PerkData perkData)
    {
        GameObject panel = null;

        if (perkData.IsUpgradable == true)
        {
            panel = Instantiate(_upgradablePerkPanelPrefab, Vector3.zero, Quaternion.identity, parentObject);
        }
        else
        {
            panel = Instantiate(_singlePerkPanelPrefab, Vector3.zero, Quaternion.identity, parentObject);
        }

        _perkInventoryPanels.Add(panel.GetComponent<PerkInventoryPanel>());
        
        _perkInventoryPanels[_perkInventoryPanels.Count - 1].SetPerkData(perkData);

        _perkInventoryPanels[_perkInventoryPanels.Count - 1].Setup();
    }

    public void RemovePerkInventoryPanel(PerkData perkData)
    {
        for (int i = 0; i < _perkInventoryPanels.Count; i++)
        {
            if (perkData == _perkInventoryPanels[i].GetPerkData())
            {
                Destroy(_perkInventoryPanels[i]);

                _perkInventoryPanels.RemoveAt(i);
            }
        }
    }

    public void OpenInventory()
    {
        panelAnimator.gameObject.SetActive(true);

        CalculatePanelHeight();

        panelAnimator.Play("PerkInventoryAppear");
    }

    private void CalculatePanelHeight()
    {
        parentObject.gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(0f, (parentObject.childCount * 270) + ((parentObject.childCount - 1) * 20));
    }

    public void CloseInventory()
    {
        panelAnimator.Play("PerkInventoryDisappear");

        Invoke ("DisactivatePanel", 0.75f);
    }

    private void DisactivatePanel()
    {
        panelAnimator.gameObject.SetActive(false);
    }
}
