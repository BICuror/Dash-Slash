using UnityEngine;

public sealed class LaserBullet : MonoBehaviour
{
    private float _lifetime = 5f;

    private Rigidbody2D _rb;

    private int _damage;
    
    public void Setup(int newDamage) 
    {
        _damage = newDamage;

        _rb = GetComponent<Rigidbody2D>();

        Destroy(gameObject, _lifetime);

        Invoke("StartDisappearing", _lifetime - 0.75f);
    }

    private void StartDisappearing() => GetComponent<Animator>().Play("LaserBulletDisappear");

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.gameObject.TryGetComponent(out IDamagable health))
        {
            health.GetHurt(Main.combatStats.MultiplyDamage(_damage, DroneType.Area));

            other.gameObject.GetComponent<IKnockbackable>().KnockBack(transform.position, 2f);
        }
        else 
        {
            ChangeVelocity(other.gameObject);
        }
    }

    private void ChangeVelocity(GameObject gameObject)
    {
        if (gameObject.name == "downBarier" || gameObject.name == "upBarier")
        {
            _rb.velocity = new Vector2(_rb.velocity.x, -_rb.velocity.y); 
        }   
        else
        {
            _rb.velocity = new Vector2(-_rb.velocity.x, _rb.velocity.y); 
        } 
    }
}
