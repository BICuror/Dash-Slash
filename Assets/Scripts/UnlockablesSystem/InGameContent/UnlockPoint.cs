using UnityEngine;

public sealed class UnlockPoint : MonoBehaviour
{
    [SerializeField] private Rigidbody2D _rb;

    [SerializeField] private float _speed;

    [SerializeField] private float _rotationSpeed;

    [SerializeField] private float _aceleration;

    [SerializeField] private UnlockPointsCounter _pointCounter;

    [SerializeField] private GameObject _explotionPrefab;

    private void FixedUpdate()
    {
        _speed *= _aceleration;

        _rotationSpeed *= _aceleration;

        if (Main.playerTransform.gameObject.activeSelf == false) OnTriggerEnter2D(null);

        float rotationAmount = Vector3.Cross((Main.playerTransform.position - transform.position).normalized, transform.right).z;

        _rb.angularVelocity = -rotationAmount * _rotationSpeed;

        _rb.velocity = transform.right * _speed;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        _pointCounter.AddPoints(1);

        Instantiate(_explotionPrefab, transform.position, Quaternion.identity);

        Destroy(gameObject);
    }
}
