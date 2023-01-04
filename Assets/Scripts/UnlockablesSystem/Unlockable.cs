using UnityEngine;

[CreateAssetMenu(fileName = "Unlockable", menuName = "ScriptableObjects/Unlockable")]

public abstract class Unlockable : ScriptableObject
{
    [SerializeField] private string _unlockableKey;

    public int Price;

    public void Unlock() => PlayerPrefs.SetInt(_unlockableKey, 1);

    public bool IsUnlocked() => PlayerPrefs.GetInt(_unlockableKey) == 1;
}
