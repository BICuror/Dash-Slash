using UnityEngine;

public class Chaser : MonoBehaviour
{
    private DashingModule _dashingModule;

    private void Start() => _dashingModule = GetComponent<DashingModule>();
    
    public void Dash()
    {
        _dashingModule.StartDash(transform.right);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Borders"))
        {
            _dashingModule.StopDash();
        }
    }
}
