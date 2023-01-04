using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zone : MonoBehaviour
{
    [SerializeField] private Animator anim;

    public void Activate(float lifetime)
    {
        gameObject.SetActive(true);

        Invoke("PrepeareToDeactivate", lifetime);
    }

    private void PrepeareToDeactivate()
    {
        anim.Play("ZoneDisappear");
    }

    private void Deactivate()
    {
        gameObject.SetActive(false);
    }

    private void OnEnabled()
    {
        anim.Play("ZoneAppear");
    }
}
