using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravitoneDashAbility : MonoBehaviour
{
    [SerializeField] GameObject _gravitonePrefab;

    public void ObtainAbility()
    {
        Main.playerAbility.AbilityActivated.AddListener(Activate);
    }

    public void RemoveAbility()
    {
        Main.playerAbility.AbilityActivated.RemoveListener(Activate);
    }

    private void Activate(Vector3 direction)
    {
        Instantiate(_gravitonePrefab, Main.playerTransform.position, Main.playerTransform.rotation);
    }
}