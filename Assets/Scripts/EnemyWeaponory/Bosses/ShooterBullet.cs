using UnityEngine;
using UnityEngine.Events;

public sealed class ShooterBullet : MonoBehaviour
{
    [SerializeField] private float _timeBeforeInvoking;

    public UnityEvent<Vector3> HitSomething;

    private void Awake() => Invoke("Hit", _timeBeforeInvoking);

    private void OnTriggerEnter2D(Collider2D other) => Hit();

    private void Hit()
    {
        HitSomething.Invoke(transform.position);

        Destroy(gameObject);
    }
}
