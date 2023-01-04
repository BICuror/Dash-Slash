using UnityEngine;

public class FlagSpawwner : ZoneSpawner
{
    private float _damageIncrease;

    public void SetDamageIncrease(float increase)
    {
        _damageIncrease += increase;
    }

    protected override void ModifyZone(GameObject zone) 
    {
        zone.GetComponent<Flag>().Setup(_damageIncrease);
    }
}
