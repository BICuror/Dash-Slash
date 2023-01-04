using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class PlayerAbility : MonoBehaviour
{
    [Header("RechargeSettings")]

    [SerializeField] private float abilityRechargeSpeed;

    [SerializeField] private float abilityCooldown;

    [Range(1, 4)] [SerializeField] private int maxAbilityCharges;

    [SerializeField] private float fillForOneActivation;
    
    private int currentAbilityCharges;

    private bool abilityIsActive;

    private float currentFill;

    [Header("LineRendererSettings")]

    [SerializeField] private float _defaultTime;

    [SerializeField] private TrailRenderer _trailRenderer;

    [SerializeField] private Gradient _trailGradient;
 
    [Header("Links")]

    [SerializeField] private PlayerAbilityUI playerAbilityUI;

    [SerializeField] private ParticleSystem gainChargePartSystem;

    public delegate void EventHandler(Vector3 direction); 
    public event EventHandler AbilityActivated;

    private void OnEnable()
    {
        abilityIsActive = false;
        
        currentFill = 0f;
        
        currentAbilityCharges = 0;

        UpdateTrail();

        StartCoroutine(IncreaseFillAmount());
        
        AbilityActivated += _ => Main.cameraEffects.StartSmallScreenShake();
    }

    public void ActivateAbility(Vector3 activationDirection)
    {
        if (abilityIsActive == false && currentAbilityCharges > 0)
        {
            currentAbilityCharges--; 

            abilityIsActive = true;

            AbilityActivated.Invoke(activationDirection);

            StopAllCoroutines();

            StartCoroutine(Decrease((currentFill - currentAbilityCharges * fillForOneActivation) / (abilityCooldown / 2 * 50f)));

            StartCoroutine(AbilityColldownDash());

            UpdateTrail();
        }
    }  

    private IEnumerator AbilityColldownDash()
    {
        yield return new WaitForSeconds(abilityCooldown);

        StopAllCoroutines();

        StartCoroutine(IncreaseFillAmount());

        abilityIsActive = false;
    }

    private void RecoverCharge()
    {
        playerAbilityUI.SemgmentAppear(currentAbilityCharges);

        gainChargePartSystem.Play();
        
        currentAbilityCharges++;
    }

    private IEnumerator IncreaseFillAmount()
    {
        yield return new WaitForFixedUpdate();

        currentFill += abilityRechargeSpeed;
        
        playerAbilityUI.SetFillAmount(currentFill);

        if (currentFill < (currentAbilityCharges + 1) * fillForOneActivation)
        {
            StartCoroutine(IncreaseFillAmount());
        } 
        else if (currentFill <= maxAbilityCharges * fillForOneActivation)
        {
            RecoverCharge();

            UpdateTrail();

            StartCoroutine(IncreaseFillAmount());
        }
        else
        {
            RecoverCharge();

            UpdateTrail();

            currentFill = fillForOneActivation * maxAbilityCharges;

            playerAbilityUI.SetFillAmount(currentFill);
        }
    }

    private void UpdateTrail()
    {
        float abilityCharge = (float)currentAbilityCharges / maxAbilityCharges;

        _trailRenderer.time = _defaultTime * abilityCharge;

        _trailRenderer.startColor = _trailGradient.Evaluate(abilityCharge);
        _trailRenderer.endColor = _trailGradient.Evaluate(abilityCharge);
    }

    private IEnumerator Decrease(float decreaseFillSpeed)
    {
        yield return new WaitForFixedUpdate();

        currentFill -= decreaseFillSpeed;
        
        playerAbilityUI.SetFillAmount(currentFill);

        if (currentFill > currentAbilityCharges * fillForOneActivation) 
        {
            StartCoroutine(Decrease(decreaseFillSpeed));
        }
        else 
        {
            playerAbilityUI.SetFillAmount(fillForOneActivation * currentAbilityCharges);
        }
    }

    public void SetMaxAbilityCharge(int value) => maxAbilityCharges += value;
    public void IncreaseRechargeSpeed(float value) => abilityRechargeSpeed += value;
}
