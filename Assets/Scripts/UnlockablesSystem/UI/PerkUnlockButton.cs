using UnityEngine;

public sealed class PerkUnlockButton : UnlockButton
{
    [SerializeField] protected UnlockablePerk _unlockable;

    public override void SetUnlockInfo()
    {
        _unlockInfoPanel.DisplayPerkUnlockInfo(_unlockable);
    }

    protected override bool IsUnlocked() => _unlockable.IsUnlocked();
}
