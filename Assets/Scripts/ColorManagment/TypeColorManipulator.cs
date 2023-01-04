using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TypeColorManipulator : MonoBehaviour
{
    [SerializeField] private TypeColor[] _colors;

    private void Start()
    {
        for (int i = 0; i < _colors.Length; i++)
        {
            _colors[i].LoadColor();
        }
    }

    public void SetDefaultColors()     
    {
        for (int i = 0; i < _colors.Length; i++)
        {
            _colors[i].SetDefaultColor();
        }
    }

    private void OnDestroy()     
    {
        for (int i = 0; i < _colors.Length; i++)
        {
            _colors[i].SaveColor();
        }
    }
}
