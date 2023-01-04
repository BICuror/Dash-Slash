using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public sealed class CurseManager : MonoBehaviour
{
    [Header("CurseSettings")]
    [SerializeField] private CurseData[] _cursesData;
    [Range(0f, 100f)] [SerializeField] private float _chanceOfCurseAppearing;

    

    [Header("Links")]
    [SerializeField] private CurseUIPresentator _curseUIPresentator;



    public void SetCursePropability(int сhanceValue) => _chanceOfCurseAppearing = сhanceValue;

    public bool TrySetCurse()
    {
        bool state = (Random.Range(0f, 100f) < _chanceOfCurseAppearing);

        if (state)
        {
            SetRandomCurse();
        }

        return state;
    }

    public void DestroyAllCurses()
    {
        CurseBasis[] activeCurses = gameObject.GetComponents<CurseBasis>();

        for (int i = 0; i < activeCurses.Length; i++)
        {
            activeCurses[i].Destroy();
        }
    }

    private void SetRandomCurse()
    {
        CurseData currentCurseData = _cursesData[Random.Range(0, _cursesData.Length)];

        gameObject.AddComponent(System.Type.GetType(currentCurseData.ScriptName));

        _curseUIPresentator.ShowCurse(currentCurseData);
    }
}
