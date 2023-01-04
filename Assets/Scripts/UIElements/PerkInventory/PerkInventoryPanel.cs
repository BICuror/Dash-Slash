using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class PerkInventoryPanel : MonoBehaviour
{
    [SerializeField] private Image _iconImage;
    
    [SerializeField] private TextMeshProUGUI _nameTextField;

    [SerializeField] private Image _shine;

    private PerkData _perkData;

    public void SetPerkData(PerkData newPerkData) => _perkData = newPerkData;

    public PerkData GetPerkData() => _perkData;

    protected void SetShine(PerkData data)
    {
        if (data.Type == DroneType.None) return;

        _shine.material = Main.droneSelector.gameObject.GetComponent<UIMaterialFactory>().GetPanelMaterial(_perkData.Type, 0);
    }

    protected void SetNameAndIcon(PerkData data)
    {
        _nameTextField.text = _perkData.PerkName;
        
        _iconImage.sprite = _perkData.Icon; 
    }

    public virtual void Setup() {}

    public virtual int GetPerkLevel() => 0;
}
