using UnityEngine;
using TMPro;

[CreateAssetMenu(fileName = "HealthUp", menuName = "ScriptableObjects/Perks/HealthUp")]

public sealed class HealthUp : PerkBasis
{
    [SerializeField] private PlayerHealthStats _playerHealthStats;

    public override void Obtain()
    {
        _playerHealthStats.SetMaxHealthPoints(_playerHealthStats.GetMaxHealthPoints() + 1);
    }

    public override void Remove()
    {
        _playerHealthStats.SetMaxHealthPoints(_playerHealthStats.GetMaxHealthPoints() - 1);
    }

    public override void SetDescription(TextMeshProUGUI textField) 
    {
        textField.text = "You gain additional health point";
    }
}
