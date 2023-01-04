using UnityEngine;

[CreateAssetMenu(fileName = "UnlockablePerk", menuName = "ScriptableObjects/UnlockablePerk")]

public sealed class UnlockablePerk : Unlockable
{
    public PerkData Perk;
}
