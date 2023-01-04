using UnityEngine;

public class CurseBasis : MonoBehaviour
{
    private void Start() => Activate();

    protected virtual void Activate() {}
    
    public void Destroy()
    {
        Deactivate();

        Destroy(this);
    }

    public virtual void Deactivate() {}
}
