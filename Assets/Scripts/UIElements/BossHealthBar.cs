using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class BossHealthBar : MonoBehaviour
{
    [SerializeField] private Slider _healthSlider;

    [SerializeField] private Slider _healthDifferenceSlider;

    [SerializeField] private float _decreaseDifferenceSpeed;

    [SerializeField] private float _decreaseSpeedAcseleration;

    private float _currentDecreaseSpeed;

    [SerializeField] private Animator _animator;

    public void ActivateHealthBar()
    {
        _healthSlider.value = 1f;
        _healthDifferenceSlider.value = 1f;

        _animator.gameObject.SetActive(true);

        _animator.Play("HealthBarAppear");
    }

    public void UpdateHealthBar(float healthPerccent)
    {
        _healthSlider.value = healthPerccent;

        _currentDecreaseSpeed = _decreaseDifferenceSpeed;

        StopAllCoroutines();

        StartCoroutine(DecreaseDifference());
    }

    private IEnumerator DecreaseDifference()
    {
        yield return new WaitForFixedUpdate();

        _currentDecreaseSpeed *= _decreaseSpeedAcseleration;

        _healthDifferenceSlider.value -= _currentDecreaseSpeed;

        if (_healthDifferenceSlider.value > _healthSlider.value) 
        {
            StartCoroutine(DecreaseDifference());
        }
    }

    public void DeactivateHealthBar()
    {
        _animator.Play("HealthBarDisappear");
    }
}
