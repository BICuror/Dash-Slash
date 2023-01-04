using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/CurseData")]
public sealed class CurseData : ScriptableObject 
{
    public readonly Sprite Icon;

    public readonly string Name;

    public readonly string ScriptName;
}

