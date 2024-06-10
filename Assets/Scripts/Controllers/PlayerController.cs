using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;


[AddComponentMenu("Dirk Dynamite/PlayerController")]
public class PlayerController : MonoBehaviour
{
    
    //States
    public enum PlayerState
    {
        IDLE, WALK, ATTACK, BIG_ATTACK, HURT, DEATH, DASH, BLOCK, STAMINALESS,
    }

    public PlayerState currentState;


    //References
    public Animator playerGraphics;
    public Move moveAction;
    public Dash dashAction;
    public Block blockAction;
    public HealthManager healthManager;
    public StaminaManager staminaManager;
    public CircleCollider2D collectCollider;
    public Hit hitAction;

    public SO_player StatsPlayer; 
 
    
    //Controller switches
    private bool _isAttacking;
    private bool _isBigAttacking;
    private bool _isMoving;
    public bool _isDashing;
    public bool _isHurting;
    private bool _isBlocking;
    private bool _isDead;
    private bool _isInteracting;
    private bool _isStaminaless;

    private Vector2 _inputDirection;

    public bool _moveBuffer = true;

    //Animation switches
    public bool _attackAnimation;
    public bool _deathAnimation;
    //public bool _dashAnimation;
    public bool _hurtAnimation;
    public bool _bigAttackAnimation;

    //StaminaCosts
    [Header("Stamina costs")]
    public int weakHitStaminaCost = 10;
    public int strongHitStaminaCost = 20;
    public int dashCost = 10;
    public int blockCost = 10;

    public bool _blockActive;
    public int damageTaken;

    public UnityEvent onDeath;
    public UnityEvent onStaminaNotEnough;

    public Vector2 _hurtDirection;
    public bool _hurtBuffer = false;
    public float _impulseSpeed;

    private Rigidbody2D _rb2d;


    //Start is called before the first frame update
    void Start()
    {
        _rb2d = GetComponent<Rigidbody2D>();
    }

    //Update is called once per frame
    void Update()
    {
        OnStateUpdate();
        StatsPlayer.health = healthManager.current;
        StatsPlayer.stamina = staminaManager.current;
    }

    void FixedUpdate()
    {
       if  (!_isDashing && !_isDead && !_isHurting && _moveBuffer) 
        {
            /*if (_isMoving)
            {
                playerGraphics.SetBool("isWalking", true);
            }
            else
            {
                playerGraphics.SetBool("isWalking", false);
            }*/
            
            moveAction.MoveProcess(_inputDirection);
        }
        else
        {
            playerGraphics.SetBool("isWalking", false);

            if (_hurtBuffer)
            {

                    if (_hurtDirection.x > 0f && transform.localScale.x > 0f)
                    {
                        transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
                    }
                    else if (_hurtDirection.x < 0f && transform.localScale.x < 0f)
                    {
                        transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
                    }

                    _rb2d.AddForce(_hurtDirection * _impulseSpeed, ForceMode2D.Impulse);
                    _hurtBuffer = false;
                _moveBuffer = true;
            }

        }


    }
    #region States
    void OnStateEnter()
    {
        switch (currentState)
        {
            case PlayerState.IDLE:
                playerGraphics.SetBool("isIdle", true);
                break;
            case PlayerState.WALK:
                playerGraphics.SetBool("isWalking", true);
                break;
            case PlayerState.ATTACK:
                _attackAnimation = true;
                staminaManager.Consume(weakHitStaminaCost);
                playerGraphics.SetBool("isAttacking", true);
                _moveBuffer = false;
                moveAction.MoveProcess(Vector2.zero);
                break;
            case PlayerState.HURT:
                _hurtAnimation = true;
                playerGraphics.SetBool("isHurting", true);
                _hurtBuffer = true;
                healthManager.Hurt(damageTaken);
                hitAction.WeakStrikeDeactivate();
                _attackAnimation = false;
                hitAction.StrongStrikeDeactivate();
                _bigAttackAnimation = false;
                break;
            case PlayerState.DEATH:
                _deathAnimation = true;
                playerGraphics.SetBool("isDead", true);
                moveAction.MoveProcess(Vector2.zero);
                break;
            case PlayerState.DASH:
                //_dashAnimation = true;
                staminaManager.Consume(dashCost);
                playerGraphics.SetBool("isDashing", true);
                dashAction.OnDashEnter();
                break;
            case PlayerState.BLOCK:
                blockAction.OnBlockEnter();
                playerGraphics.SetBool("isBlocking", true);
                break;
            case PlayerState.BIG_ATTACK:
                _bigAttackAnimation = true;
                _moveBuffer = false;
                staminaManager.Consume(strongHitStaminaCost);
                playerGraphics.SetBool("isBigAttacking", true);
                moveAction.MoveProcess(Vector2.zero);
                break;
            case PlayerState.STAMINALESS:
                playerGraphics.SetTrigger("isStaminaless");
                _isStaminaless = false;
                break;
            default:
                break;
        }
    }


    private void OnStateUpdate()
    {
        switch (currentState)
        {
            case PlayerState.IDLE:
                if (_isHurting)
                {
                    TransitionToState(PlayerState.HURT);
                }
                else if (_isDashing)
                {
                    TransitionToState(PlayerState.DASH);
                }
                else if (_isAttacking)
                {
                    TransitionToState(PlayerState.ATTACK);
                }
                else if (_isBigAttacking)
                {
                    TransitionToState(PlayerState.BIG_ATTACK);
                }
                else if (_isBlocking)
                {
                    TransitionToState(PlayerState.BLOCK);
                }
                else if (_isMoving)
                {
                    TransitionToState(PlayerState.WALK);
                }
                else if (_isStaminaless)
                {
                    TransitionToState(PlayerState.STAMINALESS);
                }
                break;
            case PlayerState.WALK:
                if (_isHurting)
                {
                    TransitionToState(PlayerState.HURT);
                }
                else if (_isDashing)
                {
                    TransitionToState(PlayerState.DASH);
                }
                else if (_isAttacking)
                {
                    TransitionToState(PlayerState.ATTACK);
                }
                else if (_isBigAttacking)
                {
                    TransitionToState(PlayerState.BIG_ATTACK);
                }
                else if (_isStaminaless)
                {
                    TransitionToState(PlayerState.STAMINALESS);
                }
                else if (_isBlocking)
                {
                    TransitionToState(PlayerState.BLOCK);
                }
                else if (!_isMoving)
                {
                    TransitionToState(PlayerState.IDLE);
                }
                break;
            case PlayerState.ATTACK:
                if (_isHurting)
                {
                    TransitionToState(PlayerState.HURT);
                }

                if (!_attackAnimation)
                {
                    if (_isDashing)
                    {
                        TransitionToState(PlayerState.DASH);
                    }
                    else if (_isBlocking)
                    {
                        TransitionToState(PlayerState.BLOCK);
                    }
                    else if (_isMoving)
                    {
                        TransitionToState(PlayerState.WALK);
                    }
                    else
                    {
                        TransitionToState(PlayerState.IDLE);
                    }
                }
                break;
            case PlayerState.BIG_ATTACK:
                if (_isHurting)
                {
                    TransitionToState(PlayerState.HURT);
                }

                if (!_bigAttackAnimation)
                {
                    if (_isDashing)
                    {
                        TransitionToState(PlayerState.DASH);
                    }
                    else if (_isBlocking)
                    {
                        TransitionToState(PlayerState.BLOCK);
                    }
                    else if (_isMoving)
                    {
                        TransitionToState(PlayerState.WALK);
                    }
                    else
                    {
                        TransitionToState(PlayerState.IDLE);
                    }
                }
                break;
            case PlayerState.HURT:

                if (healthManager.current == 0)
                {
                    _isDead = true;
                }

                if (!_hurtAnimation)
                {
                    if (_isDead)
                    {
                        TransitionToState(PlayerState.DEATH);
                    }
                    else if (_isDashing)
                    {
                        TransitionToState(PlayerState.DASH);
                    }
                    else if (_isAttacking)
                    {
                        TransitionToState(PlayerState.ATTACK);
                    }
                    else if (_isBigAttacking)
                    {
                        TransitionToState(PlayerState.BIG_ATTACK);
                    }
                    else if (_isBlocking)
                    {
                        TransitionToState(PlayerState.BLOCK);
                    }
                    else if (_isMoving)
                    {
                        TransitionToState(PlayerState.WALK);
                    }
                    else
                    {
                        TransitionToState(PlayerState.IDLE);
                    }
                }
                break;
            case PlayerState.DEATH:
                if (!_deathAnimation)
                {
                    onDeath.Invoke();
                }
                break;
            case PlayerState.DASH:

                dashAction.OnDashPerform();


                if (!_isDashing)
                {
                    if (_isHurting)
                    {
                        TransitionToState(PlayerState.HURT);
                    }
                    else if (_isAttacking)
                    {
                        TransitionToState(PlayerState.ATTACK);
                    }
                    else if (_isBigAttacking)
                    {
                        TransitionToState(PlayerState.BIG_ATTACK);
                    }
                    else if (_isBlocking)
                    {
                        TransitionToState(PlayerState.BLOCK);
                    }
                    else if (_isMoving)
                    {
                        TransitionToState(PlayerState.WALK);
                    }
                    else
                    {
                        TransitionToState(PlayerState.IDLE);
                    }
                }
                break;
            case PlayerState.BLOCK:
                if (_isHurting)
                {
                    TransitionToState(PlayerState.HURT);
                }
                else if (_isDashing)
                {
                    TransitionToState(PlayerState.DASH);
                }
                else if (_isAttacking)
                {
                    TransitionToState(PlayerState.ATTACK);
                }
                else if (_isStaminaless)
                {
                    TransitionToState(PlayerState.STAMINALESS);
                }
                else if (!_isBlocking && _isMoving)
                {
                    TransitionToState(PlayerState.WALK);
                }
                else if (!_isBlocking)
                {
                    TransitionToState(PlayerState.IDLE);
                }
                break;
            case PlayerState.STAMINALESS:
                if (_isHurting)
                {
                    TransitionToState(PlayerState.HURT);
                }
                else if (_isDashing)
                {
                    TransitionToState(PlayerState.DASH);
                }
                else if (_isAttacking)
                {
                    TransitionToState(PlayerState.ATTACK);
                }
                else if (_isBigAttacking)
                {
                    TransitionToState(PlayerState.BIG_ATTACK);
                }
                else if (_isMoving)
                {
                    TransitionToState(PlayerState.WALK);
                }
                else if (_isBlocking)
                {
                    TransitionToState(PlayerState.BLOCK);
                }
                else if (!_isMoving)
                {
                    TransitionToState(PlayerState.IDLE);
                }
                break;
                break;
            default:
                break;
        }
    }


    void OnStateExit()
    {
        switch (currentState)
        {
            case PlayerState.IDLE:
                playerGraphics.SetBool("isIdle", false);
                break;
            case PlayerState.WALK:
                playerGraphics.SetBool("isWalking", false);
                break;
            case PlayerState.ATTACK:
                _isAttacking = false;
                playerGraphics.SetBool("isAttacking", false);
                _moveBuffer = true;
                _attackAnimation = false;
                hitAction.WeakStrikeDeactivate();
                break;
            case PlayerState.HURT:
                _isHurting = false;
                playerGraphics.SetBool("isHurting", false);
                break;
            case PlayerState.DEATH:
                break;
            case PlayerState.DASH:
                dashAction.OnDashExit();
                playerGraphics.SetBool("isDashing", false);
                break;
            case PlayerState.BLOCK:
                blockAction.OnBlockExit();
                playerGraphics.SetBool("isBlocking", false);
                break;
            case PlayerState.BIG_ATTACK:
                _isBigAttacking = false;
                playerGraphics.SetBool("isBigAttacking", false);
                _moveBuffer = true;
                break;
            case PlayerState.STAMINALESS:
                break;
            default:
                break;
        }
    }

    void TransitionToState(PlayerState newState)
    {
        OnStateExit();
        currentState = newState;
        OnStateEnter();
    }
    #endregion

    #region InputMethod
    public void MoveInput(InputAction.CallbackContext context) 
    {
        switch (context.phase)
        {
            case InputActionPhase.Performed:
                _isMoving = true;
                _inputDirection = context.ReadValue<Vector2>();
                break;
            case InputActionPhase.Canceled:
                _isMoving = false;
                _inputDirection = new Vector2(0, 0);
                break;
            default:
                break;
        }
    }

    public void WeakAttackInput(InputAction.CallbackContext context)
    {
        switch (context.phase)
        {
            case InputActionPhase.Performed:
                //Stamina Check
                
                if (staminaManager.current >= weakHitStaminaCost)
                {
                    _isAttacking = true;
                    
                }
                else
                {
                    onStaminaNotEnough.Invoke();
                    _isStaminaless = true;
                }
                break;
            case InputActionPhase.Canceled:
                _isAttacking = false;
                break;
            default:
                break;
        }
    }

    public void StrongAttackInput(InputAction.CallbackContext context)
    {
        switch (context.phase)
        {
            case InputActionPhase.Performed:
                //Stamina Check
                
                if (staminaManager.current >= strongHitStaminaCost)
                {
                    _isBigAttacking = true;
                    
                }
                else
                {
                    onStaminaNotEnough.Invoke();
                    _isStaminaless = true;
                }
                break;
            case InputActionPhase.Canceled:
                _isBigAttacking = false;
                break;
            default:
                break;
        }
    }

    public void DashInput(InputAction.CallbackContext context)
    {
        switch (context.phase)
        {
            case InputActionPhase.Performed:
                //Stamina Check
                if (staminaManager.current >= dashCost)
                {
                    _isDashing = true;
                    
                }
                else
                {
                    onStaminaNotEnough.Invoke();
                    _isStaminaless = true;
                }
                break;
            case InputActionPhase.Canceled:
                //_isDashing = false;
                break;
            default:
                break;
        }
    }

    public void BlockInput(InputAction.CallbackContext context)
    {
        switch (context.phase)
        {
            case InputActionPhase.Performed:
                _isBlocking = true;
                break;
            case InputActionPhase.Canceled:
                _isBlocking = false;
                break;
            default:
                break;
        }
    }

    public void InteractInput(InputAction.CallbackContext context)
    {
        switch (context.phase)
        {
            case InputActionPhase.Performed:
                _isInteracting = true;
                collectCollider.enabled = true;
                break;
            case InputActionPhase.Canceled:
                _isInteracting = false;
                collectCollider.enabled = false;
                break;
            default:
                break;
        }
    }
    #endregion
}
