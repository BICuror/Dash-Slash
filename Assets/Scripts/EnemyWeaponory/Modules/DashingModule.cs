using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashingModule : MonoBehaviour 
{
    [Header("DashSettings")]
    [SerializeField] private float dashDuration;

    [Constant] [SerializeField] private float dashSpeed;

    protected Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void StartDash(Vector2 direction)
    {
        rb.AddForce(direction * dashSpeed, ForceMode2D.Impulse);

        StartCoroutine(WaitToStopDash());
    }

    private IEnumerator WaitToStopDash()
    {
        yield return new WaitForSeconds(dashDuration);

        StopDash();
    }
    
    public void StopDash()
    {
        StopAllCoroutines();

        rb.velocity = new Vector2(0f, 0f);
    }
}
