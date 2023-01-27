using UnityEngine;

public sealed class GravitoneBullet : MonoBehaviour
{
    [SerializeField] private GameObject _gravitoneField;

    private float _areaDamage;

    public void SetDamage(float damage) => _areaDamage = damage;

    private void Awake() => Destroy(gameObject, 4f);

    private void OnTriggerEnter2D(Collider2D other) 
    {
        GameObject gravitone = Instantiate(_gravitoneField, transform.position, transform.rotation);

        gravitone.transform.GetChild(0).GetChild(0).gameObject.GetComponent<WeaponArea>().Setup(_areaDamage);

        Destroy(gameObject);
    }
}
