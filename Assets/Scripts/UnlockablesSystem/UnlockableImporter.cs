using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlockableImporter : MonoBehaviour
{
    [SerializeField] private UnlockableDrone[] _unlockableDrones;
    [SerializeField] private UnlockablePerk[] _unlockablePerks;

    [SerializeField] private SelectionStorage _selectionStorage;

    private void OnEnable() 
    {
        for (int i = 0; i < _unlockableDrones.Length; i++)
        {
            if (_unlockableDrones[i].IsUnlocked() == true)
            {
                _selectionStorage.AddDrone(_unlockableDrones[i].Drone);
            }
        } 

        for (int i = 0; i < _unlockablePerks.Length; i++)
        {
            if (_unlockablePerks[i].IsUnlocked() == true)
            {
                _selectionStorage.ReturnPerk(_unlockablePerks[i].Perk);
            }
        }     
    }
}
