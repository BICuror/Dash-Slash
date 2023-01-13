using UnityEngine;

public sealed class Taurus : MonoBehaviour
{
    [SerializeField] private ParticleSystem _dumpParticleSystem;

    private DashingModule _dashingModule;

    private MoveAgent _moveAgent;

    private void Start()
    {
        _moveAgent = GetComponent<MoveAgent>();

        _dashingModule = GetComponent<DashingModule>();
    }
    
    public void Dash()
    {
        _moveAgent.enabled = false;

        Vector3 direction = (Main.playerTransform.position - transform.position).normalized;

        _dashingModule.StartDash(direction);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Borders"))
        {
            _dashingModule.StopDash();

            _moveAgent.enabled = true;

            _dumpParticleSystem.Play();
        }
    }
}
