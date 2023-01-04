using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "PlayerHealthStats", menuName = "ScriptableObjects/DTOs/PlayerHealthStats")]

public sealed class PlayerHealthStats : ScriptableObject 
{
    [SerializeField] private int _trueMaxHealthPoints;

    [SerializeField] private int _defaultMaxHealthPoints;

    private int _maxHealthPoints;

    private int _currentHealthPoints; 

    public UnityEvent HealthDecreasedEvent;
    public UnityEvent HealthIncreasedEvent;

    public UnityEvent<int> HealthChangedEvent; 

    public void SetDefaultHealthPoints() => SetMaxHealthPoints(_defaultMaxHealthPoints);

    public void SetMaxHealthPoints(int maxHealthPoints)
    {
        _maxHealthPoints = maxHealthPoints;

        _currentHealthPoints = _maxHealthPoints;
    }

    public void SetHealthPoints(int healthPoints)
    {
        HealthChange healthChange = HealthChange.None;

        if (_currentHealthPoints > healthPoints) healthChange = HealthChange.Decrease;
        else if (_currentHealthPoints < healthPoints) healthChange = HealthChange.Increase;
           
        if (healthChange != HealthChange.None)
        {
            _currentHealthPoints = healthPoints;

            if (healthChange == HealthChange.Decrease)
            {
                HealthDecreasedEvent.Invoke();
            }   
            else if (healthChange == HealthChange.Increase)
            {
                HealthIncreasedEvent.Invoke();
            }   
            
            HealthChangedEvent.Invoke(_currentHealthPoints);     
        }        
    }

    public int GetMaxHealthPoints() => _maxHealthPoints;
    public int GetCurrentHealthPoints() => _currentHealthPoints;
    public bool IsFullHealthPoints() => _maxHealthPoints == _currentHealthPoints;

    private enum HealthChange
    {
        None,
        Increase,
        Decrease
    }
}
