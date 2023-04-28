using UnityEngine;

public sealed class ShineSettingsLoader : MonoBehaviour
{
    [SerializeField] private GameObject[] _shineObjects;

    [SerializeField] private GameObject[] _shineReplaceObjects;

    private void Awake()
    {
        bool useShineUI = (PlayerPrefs.GetInt("UseShineUI") == 1);
    
        for (int i = 0; i < _shineObjects.Length; i++)
        {
            _shineObjects[i].SetActive(useShineUI);
        }

        for (int i = 0; i < _shineReplaceObjects.Length; i++)
        {
            _shineReplaceObjects[i].SetActive(useShineUI == false);
        }

        Destroy(this);
    }
}
