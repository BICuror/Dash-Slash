using UnityEngine;

public class PlayerMovmentParticles : MonoBehaviour
{
    [SerializeField] private ParticleSystem _movmentParticles;

    [SerializeField] private float _defaultEmmision;

    private void FixedUpdate() 
    {
        ParticleSystem.EmissionModule emission = _movmentParticles.emission;
 
        emission.rateOverTime = Mathf.Lerp(0f, _defaultEmmision, Main.playerController.GetMoveDirection().magnitude);
    }
}
