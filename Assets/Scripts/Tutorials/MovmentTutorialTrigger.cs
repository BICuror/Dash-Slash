using UnityEngine;
using UnityEngine.Events;

public sealed class MovmentTutorialTrigger : MonoBehaviour
{
    public UnityEvent PlayerEntered;

    [SerializeField] private bool _worksWithInvincible;

    [SerializeField] private bool _destroyOnTouch;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.TryGetComponent(out PlayerHealth health))
        {
            if (_worksWithInvincible)
            {
                PlayerEntered.Invoke();

                if (_destroyOnTouch) Destroy(gameObject);
            }
            else if (health.IsVinsible())
            {
                PlayerEntered.Invoke();

                if (_destroyOnTouch) Destroy(gameObject);
            }
        }
    }
}
