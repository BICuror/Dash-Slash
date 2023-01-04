using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "UnlockPointsCounter", menuName = "ScriptableObjects/UnlockPointsCounter")]

public sealed class UnlockPointsCounter : ScriptableObject
{
    private int _points;

    public UnityEvent PointsUpdated;

    public void LoadPoints() => _points = PlayerPrefs.GetInt("UnlockPoints");
    public void SavePoints() => PlayerPrefs.SetInt("UnlockPoints", _points);

    public void AddPoints(int pointsAmount)
    {
        _points += pointsAmount;
        SavePoints();

        PointsUpdated.Invoke();
    }
    public void SubstractPoints(int pointsAmount)
    {
        _points -= pointsAmount;
        SavePoints();

        PointsUpdated.Invoke();
    }

    public int GetPoints() => _points;
}

