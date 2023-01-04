using UnityEngine;

[CreateAssetMenu(fileName = "UnlockableDrone", menuName = "ScriptableObjects/UnlockableDrone")]

public sealed class UnlockableDrone : Unlockable
{    
    public DroneBasis Drone;
}
