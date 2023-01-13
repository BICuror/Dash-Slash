using UnityEngine;

public sealed class ColorManager : MonoBehaviour
{
    [Header("Materials")]
    [SerializeField] private Material _singleMaterial;
    [SerializeField] private Material _areaMaterial;
    [SerializeField] private Material _defenceMaterial;
    [SerializeField] private Material _enemyMaterial;
    [SerializeField] private Material _highlightMaterial;

    public Material GetSingleMaterial() => _singleMaterial; 

    public Material GetAreaMaterial() => _areaMaterial;
    
    public Material GetDefenceMaterial() => _defenceMaterial; 
    
    public Material GetEnemyMaterial() => _enemyMaterial;
    
    public Material GetHighlightMaterial() => _highlightMaterial;

    public Color GetSingleColor() => _singleMaterial.color; 
    
    public Color GetAreaColor() => _areaMaterial.color; 
    
    public Color GetDefenceColor() => _defenceMaterial.color;
    
    public Color GetEnemyColor() => _enemyMaterial.color; 
    
    public Color GetHighlightColor() => _highlightMaterial.color;
}
