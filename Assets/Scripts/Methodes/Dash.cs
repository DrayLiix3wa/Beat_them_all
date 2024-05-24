using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("Dirk Dynamite/Dash")]
[DisallowMultipleComponent]

public class Dash : MonoBehaviour
{
    [SerializeField]
    public Rigidbody2D rb2d;
    public TrailRenderer trailRenderer;

    [Header("TestBool")]
    public bool isDashing = false;
    private bool canDash = false;

    [Header("Stamina")]
    public float dashStaminaCost;
    public float actualStamina;

    [Header("Stats")]
    public float dashRange;
    public float dashDuration = 0.3f;
    public float dashSpeed = 15f;
    private float dashChrono = 0f;

    void Start()
    {
        trailRenderer.emitting = false;
    }

    public void OnDashEnter()
    {
        if (isDashing)
        {
            dashChrono = 0f;
            rb2d.gravityScale = 0f;
            canDash = false;
            isDashing = false;
            trailRenderer.emitting = true;
        }
    }

    public void OnDashPerform()
    {
        if(isDashing)
        {
            dashChrono += Time.deltaTime;
            rb2d.velocity = new Vector2(transform.localScale.x * dashSpeed, 0f);
            isDashing = true;
            actualStamina -= dashStaminaCost;
        }
    }

    public void OnDashExit() 
    {
        if (isDashing)
        {
            rb2d.velocity = Vector2.zero;
            rb2d.gravityScale = 1f;
            trailRenderer.emitting = false;
        } 
    }
}
