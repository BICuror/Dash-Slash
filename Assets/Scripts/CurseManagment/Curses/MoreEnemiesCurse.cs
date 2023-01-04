using UnityEngine;

public sealed class MoreEnemiesCurse : CurseBasis
{
    protected override void Activate() 
    {
        Main.enemySpawner.ChangeGroupSize(1);
    }

    public override void Deactivate() 
    {
        Main.enemySpawner.ChangeGroupSize(-1);
    }
}
