using UnityEngine;

public sealed class EnemyDamageDealer : MonoBehaviour
{
    [SerializeField] private EnemyProfile _currentEnemyProfile;

    private void OnTriggerEnter2D(Collider2D other)
    {
        CheckIfPlayerAndDamage(other.gameObject);
    }   

    private void OnColliderEnter2D(Collision2D other)
    {
        CheckIfPlayerAndDamage(other.gameObject);
    } 

    private void CheckIfPlayerAndDamage(GameObject collidedObject)
    {
        if (collidedObject.CompareTag("Player"))
        {
            Main.playerHealth.TryToDealDamage(_currentEnemyProfile);
        }
    }

    public void SetNewEnemyProfile(EnemyProfile newProfile) => _currentEnemyProfile = newProfile;
}
