using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashAbility : MonoBehaviour
{
    [SerializeField] private float dashSpeed;

    [SerializeField] private float dashDuration;

    [SerializeField] private float invincibilityTime;

    [SerializeField] private float stopForce;

    [Range(0.1f, 0.9f)] [SerializeField] private float sensetivity;

    [SerializeField] private DashEffects effects;

    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        Main.playerAbility.AbilityActivated.AddListener(Dash);
    }

    public void Dash(Vector3 direction)
    {
        direction = direction.normalized;

        if ((Mathf.Abs(direction.x) + Mathf.Abs(direction.y)) < sensetivity) return;

        direction = direction.normalized;

        effects.StartDash(direction);

        rb.AddForce(direction * dashSpeed, ForceMode2D.Impulse);

        Main.playerHealth.GiveInvincibilityTime(invincibilityTime);

        StopAllCoroutines();

        StartCoroutine(WaitToStopDash());
    }  

    private IEnumerator WaitToStopDash()
    {
        yield return new WaitForSeconds(dashDuration);

        StartCoroutine(StopingDash());
    }

    private IEnumerator StopingDash()
    {
        yield return new WaitForFixedUpdate();

        Vector3 direction = Main.playerController.GetMoveDirection();

        rb.velocity = rb.velocity + new Vector2(direction.x * stopForce , direction.y * stopForce);

        if (rb.velocity.magnitude > 1.5f) StartCoroutine(StopingDash());
        else
        {
            rb.velocity = new Vector2(direction.x, direction.y);
        }
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.gameObject.CompareTag("Borders") == true)
        {
            StopAllCoroutines();
        }
    }
}
