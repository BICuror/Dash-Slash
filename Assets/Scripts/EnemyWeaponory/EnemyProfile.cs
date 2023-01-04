using UnityEngine;

[CreateAssetMenu(fileName = "Profile", menuName = "ScriptableObjects/EnemyProfile")]

public class EnemyProfile : ScriptableObject
{
    public string Name;

    public string Description;

    public Sprite Icon;
}
