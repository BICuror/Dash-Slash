using UnityEngine;

public abstract class MoveBehaviour: ScriptableObject 
{
    public abstract Vector3 GetPosition(Transform enemyTransform);     
}
