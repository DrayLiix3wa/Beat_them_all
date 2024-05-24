using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


[AddComponentMenu("Dirk Dynamite/PlayerController")]
public class PlayerController : MonoBehaviour
{
    
    //States
    public enum PlayerState
    {
        IDLE, WALK, ATTACK, BIG_ATTACK, HURT, DEATH, DASH, BLOCK
    }

    public PlayerState currentState;


    //References
    public Animator playerGraphics;
    public Move moveAction;
    public Dash dashAction;
 
    
    //Controller switches
    private bool _isAttacking;
    private bool _isBigAttacking;
    private bool _isMoving;
    public bool _isDashing;
    private bool _isHurting;
    private bool _isBlocking;
    private bool _isDead;
    private bool _isInteracting;

    private Vector2 _inputDirection;

    //Animation switches
    public bool _attackAnimation;
    public bool _deathAnimation;
    //public bool _dashAnimation;
    public bool _hurtAnimation;
    public bool _bigAttackAnimation;

    public bool _blockActive;

    //Start is called before the first frame update
    void Start()
    {
        
    }

    //Update is called once per frame
    void Update()
    {
        OnStateUpdate();
    }

    void FixedUpdate()
    {
       if  (!_isDashing && !_isDead && !_isHurting)
        {
            if (_isMoving)
            {
                playerGraphics.SetBool("isWalking", true);
            }
            else
            {
                playerGraphics.SetBool("isWalking", false);
            }
            
            moveAction.MoveProcess(_inputDirection);
        }


    }
    #region States
    void OnStateEnter()
    {
        switch (currentState)
        {
            case PlayerState.IDLE:
                break;
            case PlayerState.WALK:
                //playerGraphics.SetBool("isWalking", true);
                break;
            case PlayerState.ATTACK:
                _attackAnimation = true;
                playerGraphics.SetBool("isAttacking", true);
                break;
            case PlayerState.HURT:
                _hurtAnimation = true;
                playerGraphics.SetBool("isHurting", true);
                break;
            case PlayerState.DEATH:
                _deathAnimation = true;
                playerGraphics.SetBool("isDead", true);
                break;
            case PlayerState.DASH:
                //_dashAnimation = true;
                playerGraphics.SetBool("isDashing", true);
                dashAction.OnDashEnter();
                break;
            case PlayerState.BLOCK:
                _blockActive = true;
                playerGraphics.SetBool("isBlocknig", true);
                break;
            case PlayerState.BIG_ATTACK:
                _bigAttackAnimation = true;
                playerGraphics.SetBool("isBigAttacking", true);
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
                else if (!_isBlocking && _isMoving)
                {
                    TransitionToState(PlayerState.WALK);
                }
                else if (!_isBlocking)
                {
                    TransitionToState(PlayerState.IDLE);
                }
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
                break;
            case PlayerState.WALK:
                break;
            case PlayerState.ATTACK:
                _isAttacking = false;
                playerGraphics.SetBool("isAttacking", false);
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
                _blockActive = false;
                playerGraphics.SetBool("isBlocking", false);
                break;
            case PlayerState.BIG_ATTACK:
                _isBigAttacking = false;
                playerGraphics.SetBool("isBigAttacking", false);
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
                _isAttacking = true;
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
                _isBigAttacking = true;
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
                _isDashing = true;
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
                break;
            case InputActionPhase.Canceled:
                _isInteracting = false;
                break;
            default:
                break;
        }
    }
    #endregion
}
