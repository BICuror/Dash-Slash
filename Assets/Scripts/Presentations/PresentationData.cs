using UnityEngine;

[CreateAssetMenu(fileName = "PresentationData", menuName = "ScriptableObjects/PresenationData")]

public sealed class PresentationData : ScriptableObject
{
    [System.Serializable]
    public struct PresentationEnemy
    {
        public Vector2 EnemyLocalPosition;
        public EnemyType EnemyType;
    }

    [Header("EnemiesSettings")]

    public PresentationEnemy[] PresentationEnemies;

    public float RespawnTime = 1f;
    public bool UseRandomizedPosition;

    [Header("DroneSettings")]
    public Vector3 DroneLocalPosition;
    
    public enum EnemyType
    {
        Random,
        Small,
        Medium,
        Big
    }
}
