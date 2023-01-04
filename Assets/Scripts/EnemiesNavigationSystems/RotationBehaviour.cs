using UnityEngine;

public abstract class RotationBehaviour : ScriptableObject 
{
    public abstract Vector3 GetRotationPosition(Vector3 targetPosition);
}