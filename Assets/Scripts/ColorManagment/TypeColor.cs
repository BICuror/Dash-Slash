using UnityEngine;

[CreateAssetMenu(fileName = "TypeColor", menuName = "ScriptableObjects/ColorManagment/TypeColor")]

public sealed class TypeColor : ScriptableObject 
{
    [SerializeField] private Color _color;

    [SerializeField] private Material[] _materials;

    [SerializeField] private Material[] _shadergraphMaterials;

    [SerializeField] private Color _defaultColor;

    public void LoadColor()
    {
        if (PlayerPrefs.HasKey(this.name + "r") == true)
        {
            float r = PlayerPrefs.GetFloat(this.name + "r");
            float g = PlayerPrefs.GetFloat(this.name + "g");
            float b = PlayerPrefs.GetFloat(this.name + "b");

            _color = new Color(r, g, b, 1f);
        }
        else
        {
            _color = _defaultColor;
        }

        UpdateColor();
    }

    public void SaveColor()
    {
        PlayerPrefs.SetFloat(this.name + "r", _color.r);
        PlayerPrefs.SetFloat(this.name + "g", _color.g);
        PlayerPrefs.SetFloat(this.name + "b", _color.b);
    }

    public void SetColor(Color newColor)
    {
        _color = newColor;

        UpdateColor(); 
    } 

    public void SetDefaultColor()
    {
        _color = _defaultColor;

        UpdateColor();
    }

    public Color GetColor() => _color;

    private void UpdateColor()
    {
        for (int i = 0; i < _materials.Length; i++)
        {
            _materials[i].color = _color;
        }

        for (int i = 0; i < _shadergraphMaterials.Length; i++)
        {
            _shadergraphMaterials[i].SetColor("MainColor", _color);
        }
    }
}
