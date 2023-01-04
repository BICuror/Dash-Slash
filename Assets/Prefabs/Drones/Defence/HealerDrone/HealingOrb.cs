using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealingOrb : MonoBehaviour
{
    [SerializeField] private GameObject _pickupExplosion;

    public void Setup(float lifetime)
    {
        Invoke("Disappear", lifetime - 0.5f);
    }

    private void Disappear()
    {
        gameObject.GetComponent<Animator>().Play("HealingOrbDisappear");
    }

    public void DestroyOrb()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.TryGetComponent(out PlayerHealth health))
        {
            health.Heal();

            Instantiate(_pickupExplosion, transform.position, Quaternion.identity);

            Destroy(gameObject);
        }
    }
}
