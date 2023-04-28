using UnityEngine;

public sealed class CanvasGroup : MonoBehaviour
{
    private Animator _animator;

    private void Awake() => _animator = GetComponent<Animator>();

    private void OnEnable()
    {
        _animator.Play("Appear");
    }
}
