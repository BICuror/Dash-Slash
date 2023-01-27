using UnityEngine;

public sealed class WeaponArea : MonoBehaviour
{
    [SerializeField] private float timeBeforeDestroyed;

    [SerializeField] private DroneType _type;

    private float _damage = 3f; 

    public void Setup(float damageValue)
    {
        _damage = damageValue;

        if (timeBeforeDestroyed != 0f) Destroy(gameObject, timeBeforeDestroyed);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.TryGetComponent(out IDamagable health))
        {
            health.GetHurt(Main.combatStats.MultiplyDamage(_damage, _type));
        }
    }
}
