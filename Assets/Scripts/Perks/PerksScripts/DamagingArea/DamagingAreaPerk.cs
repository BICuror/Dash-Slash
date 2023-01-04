using UnityEngine;
using TMPro;

[CreateAssetMenu(fileName = "DamagingArea", menuName = "ScriptableObjects/Perks/DamagingArea")]

public sealed class DamagingAreaPerk : PerkBasis
{
    [SerializeField] private float _auraDamage = 8f;

    [SerializeField] private GameObject _damagingAuraPrefab;
 
    private DamagingArea _damagingAura;

    public override void Obtain()
    {
        GameObject damagingAreaObject = Instantiate(_damagingAuraPrefab, Main.droneContainer.gameObject.transform.position, Quaternion.identity, Main.droneContainer.gameObject.transform);
        
        _damagingAura = damagingAreaObject.GetComponent<DamagingArea>();
        
        _damagingAura.SetDamage(GetLevel() * _auraDamage);
    }

    public override void Remove() => Destroy(_damagingAura.gameObject);
    
    protected override void Upgrade()
    {
        _damagingAura.SetDamage(GetLevel() * _auraDamage);

        float scale = 0.2f * GetLevel();

        _damagingAura.transform.localScale = new Vector3(scale, scale, scale);
    }

    public override void SetDescription(TextMeshProUGUI textField) 
    {
        textField.text = "Creates damaging area around player";
    }

    public override void SetUpgradeDescription(TextMeshProUGUI textField)
    {
        string descripion = "";
        
        switch (GetLevel())
        {
            case 1: descripion = "Increases damaging area size and damage"; break;
            case 2: descripion = "Increases damaging area size and damage"; break;
        }

        textField.text = descripion;
    }
}
