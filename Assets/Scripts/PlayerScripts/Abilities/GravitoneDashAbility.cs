using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravitoneDashAbility : MonoBehaviour
{
    [SerializeField] GameObject _gravitonePrefab;

    public void ObtainAbility()
    {
        Main.playerAbility.AbilityActivated += _ => Activate();
    }

    public void RemoveAbility()
    {
        Main.playerAbility.AbilityActivated -= _ => Activate();
    }

    private void Activate()
    {
        Instantiate(_gravitonePrefab, Main.playerTransform.position, Main.playerTransform.rotation);
    }
}