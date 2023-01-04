using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sawblade : MonoBehaviour
{
    private float damage;

    private SawbladeDrone droneOwner;

    private bool canBePickedUp;

    [SerializeField] private Animator anim;

    [SerializeField] private float delayBeforePickingUp;

    public void Setup(float damageValue, SawbladeDrone owner)
    {
        damage = damageValue;

        droneOwner = owner;
    }

    private void OnEnable()
    {
        StartCoroutine(ActivateToPickUp());
    }

    private IEnumerator ActivateToPickUp()
    {
        yield return new WaitForSeconds(delayBeforePickingUp);

        canBePickedUp = true;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.TryGetComponent(out IDamagable health))
        {
            health.GetHurt(damage);
        }
        else if (other.gameObject.TryGetComponent(out PlayerHealth player) && canBePickedUp == true)
        {
            anim.SetBool("isCaptured", true);
        }
    }

    public void DeactivateSawblade()
    {   
        StopAllCoroutines();

        canBePickedUp = false;

        anim.SetBool("isCaptured", false);

        gameObject.SetActive(false);
    }
}
