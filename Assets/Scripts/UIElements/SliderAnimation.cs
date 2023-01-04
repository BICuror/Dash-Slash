using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderAnimation : MonoBehaviour
{
    [Header("SliderSettings")]
    [SerializeField] private float valueForOneBar;

    [SerializeField] private Image image;


    [Header("IncreaseSettings")] 
    [SerializeField] protected float increaseDuration;
    private float increaseFillSpeed;
    

    [Header("DecreaseSettings")] 
    [SerializeField] private float decreaseDuration;
    private float decreaseFillSpeed;
    private int evaluateCharge;

    private float maxFill;

    private void Awake()
    {
        Setup();
    }

    public void SetMaxFill(int charges)
    {
        maxFill = charges * valueForOneBar;
    }

    private void Setup()
    {
        decreaseFillSpeed = decreaseDuration * valueForOneBar; 

        increaseFillSpeed = increaseDuration * valueForOneBar;
    }

    public void DecreaseValueTo(int charge)
    {
        evaluateCharge = charge;

        image.fillAmount = (charge + 1) * valueForOneBar;

        StopAllCoroutines();

        StartCoroutine(Decrease());
    }

    private IEnumerator Decrease()
    {
        yield return new WaitForFixedUpdate();

        image.fillAmount -= decreaseFillSpeed;

        if (image.fillAmount > evaluateCharge * valueForOneBar) StartCoroutine(Decrease());
        else 
        {
            int bars = (int)((image.fillAmount + valueForOneBar / 2) / valueForOneBar);

            image.fillAmount = valueForOneBar * bars;
        }
    }


    public void IncreaseValueTo(int charge)
    {
        evaluateCharge = charge;

        StopAllCoroutines();

        StartCoroutine(Increase());
    }

    private IEnumerator Increase()
    {
        yield return new WaitForFixedUpdate();

        image.fillAmount += increaseFillSpeed;

        if (image.fillAmount < evaluateCharge * valueForOneBar) StartCoroutine(Increase());
        else 
        {
            int bars = (int)(image.fillAmount / valueForOneBar);

            image.fillAmount = valueForOneBar * bars;
        }
    }
}
