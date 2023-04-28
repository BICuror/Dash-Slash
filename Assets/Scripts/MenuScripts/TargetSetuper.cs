using UnityEngine;

public sealed class TargetSetuper : MonoBehaviour
{
    public void SetTarget()
    {
        gameObject.SetActive(true);

        FindObjectOfType<CameraManager>().ChangeTarget(transform);
    }
}
