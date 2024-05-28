using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using static UnityEngine.GraphicsBuffer;

[AddComponentMenu("Dirk Dynamite/EnemyController")]
[DisallowMultipleComponent]

public class EnemyController : MonoBehaviour
{
    //States
    public enum EnemyState
    {
        IDLE, WALK, ATTACK, HURT, DEATH,
    }

    public EnemyState currentState;


    //References
    [Header("References")]
    public Animator enemyGraphics;
    public Move moveAction;
    public HealthManager healthManager;
    private Transform player;
    public Hit hitAction;


    [Header("PlayerChecker")]
    public Transform playerDetector;
    public Vector2 playerDetectorSize = new Vector2(0.6f, 0.1f);
    private Collider2D _playerCollider;
    public LayerMask _playerLayerMask;


    //Controller switches
    private bool _isAttacking;
    private bool _isMoving;
    public bool _isHurting;
    private bool _isDead;
    private bool _moveBuffer = true;

    //Animation switches
    public bool _attackAnimation;
    public bool _deathAnimation;
    public bool _hurtAnimation;

    public int damageTaken;

    public UnityEvent onDeath;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        OnStateUpdate();
    }

    void FixedUpdate()
    {
        if (!_isDead && !_isHurting && _moveBuffer)
        {
            if (_isMoving)
            {
                enemyGraphics.SetBool("isWalking", true);
                Vector2 direction = new Vector2(player.position.x - transform.position.x, player.position.y - transform.position.y);
                moveAction.MoveProcess(direction.normalized);
            }
            else
            {
                enemyGraphics.SetBool("isWalking", false);
            }

        }
        else
        {
            enemyGraphics.SetBool("isWalking", false);
        }

        _playerCollider = Physics2D.OverlapBox(playerDetector.position, playerDetectorSize, 0f, _playerLayerMask);
        if (_playerCollider)
        {
            _isAttacking = true;
            _isMoving = false;
        }
        else
        {
            _isAttacking = false;
            _isMoving = true;
        }


    }
    #region States
    void OnStateEnter()
    {
        switch (currentState)
        {
            case EnemyState.IDLE:
                enemyGraphics.SetBool("isIdle", true);
                break;
            case EnemyState.WALK:
                break;
            case EnemyState.ATTACK:
                _attackAnimation = true;
                _moveBuffer = false;
                moveAction.MoveProcess(Vector2.zero);
                enemyGraphics.SetBool("isAttacking", true);
                break;
            case EnemyState.HURT:
                Debug.Log("Hurt");
                _hurtAnimation = true;
                enemyGraphics.SetBool("isHurting", true);
                healthManager.Hurt(damageTaken);
                moveAction.MoveProcess(Vector2.zero);
                hitAction.WeakStrikeDeactivate();
                break;
            case EnemyState.DEATH:
                _deathAnimation = true;
                enemyGraphics.SetBool("isDead", true);
                break;
            default:
                break;
        }
    }


    private void OnStateUpdate()
    {
        switch (currentState)
        {
            case EnemyState.IDLE:
                if (_isHurting)
                {
                    TransitionToState(EnemyState.HURT);
                }
                else if (_isAttacking)
                {
                    TransitionToState(EnemyState.ATTACK);
                }
                else if (_isMoving)
                {
                    TransitionToState(EnemyState.WALK);
                }
                break;
            case EnemyState.WALK:
                if (_isHurting)
                {
                    TransitionToState(EnemyState.HURT);
                }
                else if (_isAttacking)
                {
                    TransitionToState(EnemyState.ATTACK);
                }
                else if (!_isMoving)
                {
                    TransitionToState(EnemyState.IDLE);
                }
                break;
            case EnemyState.ATTACK:
                if (_isHurting)
                {
                    TransitionToState(EnemyState.HURT);
                }

                if (!_attackAnimation)
                {
                    if (_isMoving)
                    {
                        TransitionToState(EnemyState.WALK);
                    }
                    else
                    {
                        TransitionToState(EnemyState.IDLE);
                    }
                }
                break;
            case EnemyState.HURT:

                if (healthManager.current == 0)
                {
                    _isDead = true;
                }

                if (!_hurtAnimation)
                {
                    if (_isDead)
                    {
                        TransitionToState(EnemyState.DEATH);
                    }
                    else if (_isAttacking)
                    {
                        TransitionToState(EnemyState.ATTACK);
                    }
                    else if (_isMoving)
                    {
                        TransitionToState(EnemyState.WALK);
                    }
                    else
                    {
                        TransitionToState(EnemyState.IDLE);
                    }
                }
                break;
            case EnemyState.DEATH:
                if (!_deathAnimation)
                {
                    onDeath.Invoke();
                    Destroy(gameObject);
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
            case EnemyState.IDLE:
                enemyGraphics.SetBool("isIdle", false);
                break;
            case EnemyState.WALK:
                break;
            case EnemyState.ATTACK:
                _isAttacking = false;
                _moveBuffer = true;
                enemyGraphics.SetBool("isAttacking", false);
                break;
            case EnemyState.HURT:
                _isHurting = false;
                enemyGraphics.SetBool("isHurting", false);
                break;
            case EnemyState.DEATH:
                break;
        }
    }

    void TransitionToState(EnemyState newState)
    {
        OnStateExit();
        currentState = newState;
        OnStateEnter();
    }
    #endregion

    private void OnDrawGizmos()
    {
        Gizmos.DrawCube(playerDetector.position, playerDetectorSize);
    }
}
