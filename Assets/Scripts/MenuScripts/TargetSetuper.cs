using UnityEngine;

public sealed class TargetSetuper : MonoBehaviour
{
    public void SetTarget()
    {
        FindObjectOfType<CameraManager>().ChangeTarget(this.transform);
    }
}
