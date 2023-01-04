using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/PerkData")]

public class PerkData : ScriptableObject
{
    [Header("MainSettings")]

    public PerkBasis Perk;

    public DroneType Type = DroneType.None;

    public Sprite Icon;

    public string PerkName;

    public bool IsUpgradable;

    public bool IsMultiplyable;
}

