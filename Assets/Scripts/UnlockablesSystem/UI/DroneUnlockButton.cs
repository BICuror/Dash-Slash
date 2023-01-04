using UnityEngine;

public sealed class DroneUnlockButton : UnlockButton
{
    [SerializeField] protected UnlockableDrone _unlockable;
    public override void SetUnlockInfo()
    {
        _unlockInfoPanel.DisplayDroneUnlockInfo(_unlockable);
    }

    protected override bool IsUnlocked() => _unlockable.IsUnlocked();
}
