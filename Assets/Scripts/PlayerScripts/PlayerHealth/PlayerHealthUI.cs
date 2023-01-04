using UnityEngine.UI;
using UnityEngine;
using UnityEngine.Events;

public class PlayerHealthUI : MonoBehaviour
{
    [SerializeField] private Image _hpBar;

    [Range(0f, 1f)] [SerializeField] private float _fillForOneHp;

    [SerializeField] private Animator anim;

    [SerializeField] private PlayerHealthStats _playerHealthStats;

    private void Awake()
    {
        _playerHealthStats.HealthDecreasedEvent.AddListener(DecreaseHp);
        _playerHealthStats.HealthIncreasedEvent.AddListener(IncreaseHp);
    }

    private void SetHp(int hitPoints)
    {
        _hpBar.fillAmount = hitPoints * _fillForOneHp;
    }

    public void IncreaseHp()
    {
        int _currentHealthPoints = _playerHealthStats.GetCurrentHealthPoints();

        SetHp(_currentHealthPoints - 1);

        transform.GetChild(0).rotation = Quaternion.Euler(0f, 0f, (_currentHealthPoints - 2) * -90f);

        anim.Play("HealthBarSegmentAppear");
    }

    public void DecreaseHp()
    {
        int _currentHealthPoints = _playerHealthStats.GetCurrentHealthPoints();

        SetHp(_currentHealthPoints);
        
        transform.GetChild(0).rotation = Quaternion.Euler(0f, 0f, (_currentHealthPoints - 1) * -90f);

        anim.Play("HealthBarSegmentDisappear");
    }

    public void IncreaseHpBarValue()
    {
        _hpBar.fillAmount += _fillForOneHp;
    }

    public void DeactivateSegment()
    {
        transform.GetChild(0).gameObject.SetActive(false);
    }
}
