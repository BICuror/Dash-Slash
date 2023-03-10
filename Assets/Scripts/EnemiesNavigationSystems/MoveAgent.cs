using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]

public class MoveAgent : MonoBehaviour, IStunable, IKnockbackable
{
    [Header("BaseSettings")]

    [SerializeField] protected float _moveSpeed;

    private float _defaultSpeed;

    [SerializeField] protected float _rotationSpeed;

    [SerializeField] private float _acceleration;

    [SerializeField] private float _dumping;

    [SerializeField] private bool _isKnockbackable;

    private Vector3 _behaviouralOffset;

    [Header("Behaviour")]

    [SerializeField] private MoveBehaviour _moveBehaviour;

    [SerializeField] private RotationBehaviour _rotationBehaviour;

    private Rigidbody2D _rb;
 
    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();

        _defaultSpeed = _moveSpeed;

        _behaviouralOffset = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0f);
    }

    public void SetMoveSpeed(float speed) => _moveSpeed = speed;

    private void FixedUpdate()
    {
        Vector3 targetPosition = _moveBehaviour.GetPosition(transform) + _behaviouralOffset;

        MoveTowards(CalculateDirection(targetPosition));

        RotateTowards(CalculateDirection(_rotationBehaviour.GetRotationPosition(targetPosition)));
    }
    
    private Vector2 CalculateDirection(Vector3 targetPosition) => (targetPosition - transform.position).normalized;

    private void MoveTowards(Vector2 direction)
    {
        direction *= _moveSpeed;

        float x = Mathf.Lerp(_rb.velocity.x, direction.x, _dumping);
        float y = Mathf.Lerp(_rb.velocity.y, direction.y, _dumping);
        
        _rb.velocity = new Vector2(x * _acceleration, y * _acceleration);
    }

    private void RotateTowards(Vector2 direction)
    {
        float rotationAmount = Vector3.Cross(direction, transform.right).z;

        _rb.angularVelocity = -rotationAmount * _rotationSpeed;
    }

    public void Stun(float stunDuration)
    {
        StartCoroutine(StunEnd(stunDuration));

        _rb.velocity = _rb.velocity * 0.5f;

        _moveSpeed = 0f;
    }
    
    public void SlowDown(float slowDuration, float slowStrength)
    {
        StartCoroutine(StunEnd(slowDuration));

        _moveSpeed *= slowStrength; 
    }

    private IEnumerator StunEnd(float stunDuration)
    {
        yield return new WaitForSeconds(stunDuration);

        _moveSpeed = _defaultSpeed;

        StopAllCoroutines();
    }   

    public void KnockBack(Vector3 knockBackSourcePosition, float knockBackStrength)
    {
        if (_isKnockbackable == true)
        {
            Stun(0.2f);

            _rb.AddForce(CalculateDirection(knockBackSourcePosition) * -knockBackStrength, ForceMode2D.Impulse);
        }
    }
}
