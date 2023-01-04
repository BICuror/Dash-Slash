using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/DroneData")]
public sealed class DroneData : ScriptableObject
{  
   [SerializeField] private string name;
   [HideInInspector] public string Name { get { return name; } }

   [SerializeField] private DroneType type;
   [HideInInspector] public DroneType Type { get { return type; } }

   [SerializeField] private string description;
   [HideInInspector] public string Description { get { return description; } }

   [SerializeField] private PresentationData presentationData;
   public PresentationData PresentationData { get { return presentationData; } }

   [SerializeField] private string[] upgradesDescription;
   public string[] UpgradesDescription { get { return upgradesDescription; } }
}
