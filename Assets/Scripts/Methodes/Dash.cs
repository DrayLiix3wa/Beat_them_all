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
        dashChrono = 0f;
        trailRenderer.emitting = true;
        
    }

    public void OnDashPerform()
    {
        dashChrono += Time.deltaTime;
        rb2d.velocity = new Vector2(transform.localScale.x * dashSpeed, 0f);
    }

    public void OnDashExit() 
    {
        rb2d.velocity = Vector2.zero;
        trailRenderer.emitting = false;
    }
}
