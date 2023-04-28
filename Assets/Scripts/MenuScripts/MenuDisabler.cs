using UnityEngine;

public sealed class MenuDisabler : MonoBehaviour
{
    [SerializeField] private GameObject[] _menus;

    public void DisableAllMenus(GameObject exceptObject)
    {
        for (int i = 0; i < _menus.Length; i++)
        {
            if (_menus[i] != exceptObject)
            {
                _menus[i].SetActive(false);
            }
        }
    }
}
