using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialEffectsSetter : MonoBehaviour
{
    [SerializeField] private GameObject _menuPostProcessing;

    private bool _useSpecialEffects;

    private void Start()
    {
        if (PlayerPrefs.HasKey("UseSpecialEffects"))
        {   
            _useSpecialEffects = (PlayerPrefs.GetInt("UseSpecialEffects") == 1);

            _menuPostProcessing.SetActive(_useSpecialEffects);
        }
        else
        {
            PlayerPrefs.SetInt("UseSpecialEffects", 1);
        }
    }

    public void ChangeState()
    {
        _useSpecialEffects = !_useSpecialEffects;

        if (_useSpecialEffects == true) PlayerPrefs.SetInt("UseSpecialEffects", 1);
        else PlayerPrefs.SetInt("UseSpecialEffects", 0);

        _menuPostProcessing.SetActive(_useSpecialEffects);
    }
}
