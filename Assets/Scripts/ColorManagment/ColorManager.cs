using UnityEngine;

public sealed class ColorManager : MonoBehaviour
{
    [Header("Materials")]
    [SerializeField] private Material _singleMaterial;
    [SerializeField] private Material _areaMaterial;
    [SerializeField] private Material _defenceMaterial;

    public Material GetSingleMaterial() => _singleMaterial; 
    public Material GetAreaMaterial() => _areaMaterial;
    public Material GetDefenceMaterial() => _defenceMaterial; 

    public Color GetSingleColor() => _singleMaterial.color; 
    public Color GetAreaColor() => _areaMaterial.color; 
    public Color GetDefenceColor() => _defenceMaterial.color; 
}
