using UnityEngine;

public sealed class Sniper : MonoBehaviour
{
    private ShootingModule _shootingModule;

    private MoveAgent _moveAgent;

    private void Awake()
    {
        _moveAgent = GetComponent<MoveAgent>();

        _shootingModule = GetComponent<ShootingModule>();
    }

    public void Shoot()
    {
        _moveAgent.KnockBack(transform.right, 2f);

        _shootingModule.ShootFromVector(transform.right);
    }  
}
