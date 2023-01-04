using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[CreateAssetMenu(fileName = "SuperHot", menuName = "ScriptableObjects/Perks/SuperHot")]

public class SuperHotPerk : PerkBasis
{
    [SerializeField] private GameObject _superHotPerkObjectPrefab;

    private GameObject _superHotPerkGameObject;

    public override void Obtain()
    {
        _superHotPerkGameObject = Instantiate(_superHotPerkObjectPrefab, Vector3.zero, Quaternion.identity);
    }

    public override void Remove()
    {
        Destroy(_superHotPerkGameObject);

        Time.timeScale = 1f;
    }

    public override void SetDescription(TextMeshProUGUI textField) 
    {
        textField.text = "(just like ur dad... i mean he is super hot and stuff) Slow donw time when not moving";
    }
}
