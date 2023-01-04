using UnityEngine;
using UnityEngine.Events;

public class EnemyBullet : MonoBehaviour
{
    [SerializeField] private UnityEvent hitEvent;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player") == true || other.gameObject.CompareTag("Borders") == true)
        {
            hitEvent.Invoke();
        }
    }
}
