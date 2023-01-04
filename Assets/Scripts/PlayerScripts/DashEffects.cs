using UnityEngine;

public sealed class DashEffects : MonoBehaviour
{
    private ParticleSystem partSystem;

    private void Awake() => partSystem = gameObject.GetComponent<ParticleSystem>();

    public void StartDash(Vector3 dashVector)
    {
        ActivateParticles(dashVector);

        Main.cameraEffects.ActivateDashEffect();
    }

    private void ActivateParticles(Vector3 dashVector)
    {
        gameObject.transform.up = -dashVector;

        partSystem.Play();
    }
}
