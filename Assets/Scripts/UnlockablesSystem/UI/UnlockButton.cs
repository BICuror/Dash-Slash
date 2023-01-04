using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class UnlockButton : MonoBehaviour
{
    [SerializeField] protected UnlockInfoPanel _unlockInfoPanel;

    [SerializeField] private Unlockable[] _dependetUnlocks;

    [SerializeField] private GameObject _explotionPrefab;

    [SerializeField] protected GameObject _particleObject; 

    public abstract void SetUnlockInfo(); 

    public void Awake() => CheckToActivate();

    protected virtual bool IsUnlocked() => false;

    public void CheckToActivate()
    {
        bool shouldExplode = _particleObject.activeSelf;
        
        bool shouldActivate = true;

        for (int i = 0; i < _dependetUnlocks.Length; i++)
        {
            if (_dependetUnlocks[i].IsUnlocked() == false) 
            {
                shouldActivate = false;

                break;
            }
        }

        _particleObject.SetActive(shouldActivate == true && IsUnlocked() == false);

        if (_particleObject.activeSelf == false && shouldExplode == true) Instantiate(_explotionPrefab, transform.position, Quaternion.identity);

        gameObject.GetComponent<UnityEngine.UI.Button>().interactable = shouldActivate;
    }
}  
