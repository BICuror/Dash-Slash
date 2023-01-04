using UnityEngine;

public sealed class DroneCircleFollower : MonoBehaviour
{
    [Header("FollowingSettings")]
    [SerializeField] private Transform _target;
    [SerializeField] private float _followingSpeed;

    private void FixedUpdate()
    {
        transform.position = Vector3.Lerp(this.transform.position, _target.position, _followingSpeed);
    }
}
