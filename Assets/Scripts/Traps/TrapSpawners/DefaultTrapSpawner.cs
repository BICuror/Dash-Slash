using UnityEngine;

public class DefaultTrapSpawner : MonoBehaviour
{
    [SerializeField] private float _delayAfterTrap;

    [Range(0f, 100f)] [SerializeField] private float _appearanceChance;
 
    public virtual void Spawn(Vector3 spawnPosition, float difficulty){}

    public float GetDelayTime() => _delayAfterTrap;

    public float GetAppearanceChance() => _appearanceChance;

    protected bool GetRandomBool() => (Random.Range(0, 2) == 0);
}

