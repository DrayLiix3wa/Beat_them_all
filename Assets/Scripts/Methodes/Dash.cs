using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[AddComponentMenu("Dirk Dynamite/Dash")]
[DisallowMultipleComponent]

public class Dash : MonoBehaviour
{
    [SerializeField]
    public Rigidbody2D rb2d;
    public TrailRenderer trailRenderer;
    public AudioController audioController;

    [Header("Stats")]
    //public float dashRange;

    public float dashDuration = 0.3f;
    public float dashSpeed = 2f;
    public PlayerController playerManager;
    
    private float dashChrono = 0f;
    private Vector2 direction;

    [Header("HitBox")]
    public BoxCollider2D hitBox;

    [Header("Layers num")]
    public int playerLayer = 0;
    public int dashLayer = 1;


    void Start()
    {
        trailRenderer.emitting = false;
    }

    public void OnDashEnter()
    {
        dashChrono = 0f;
        trailRenderer.emitting = true;
        gameObject.layer = dashLayer;
        hitBox.enabled = false;



        if (rb2d.velocity.x != 0 || rb2d.velocity.y != 0)
        {
            direction = new Vector2(rb2d.velocity.x, rb2d.velocity.y);
        }
        else
        {
            direction = new Vector2(transform.localScale.x * dashSpeed, 0f);
        }

        audioController.PlayDashkSound();


    }

    public void OnDashPerform()
    {
        dashChrono += Time.deltaTime;

         rb2d.velocity = direction * dashSpeed;
       
        if (dashChrono >= dashDuration)
        {
            playerManager._isDashing = false;
        }
    }

    public void OnDashExit() 
    {
        rb2d.velocity = Vector2.zero;
        trailRenderer.emitting = false;
        trailRenderer.Clear();
        gameObject.layer = playerLayer;
        hitBox.enabled = true;
    }
}
