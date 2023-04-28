using UnityEngine;

public sealed class CameraManager : MonoBehaviour
{
    [SerializeField] private float _transitionSpeed;

    private Transform _target;
    
    private void FixedUpdate()
    {
        gameObject.transform.position = Vector3.Lerp(transform.position, _target.position, _transitionSpeed);
    }

    public void ChangeTarget(Transform newTarget)
    {
        enabled = true;
        
        _target = newTarget;
    }
}
