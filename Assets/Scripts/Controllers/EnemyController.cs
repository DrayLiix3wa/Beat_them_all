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

    public SO_objectPool pool;
    private PoolsManager poolManager;

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
    public bool _moveBuffer = true;

    //Animation switches
    public bool _attackAnimation;
    public bool _deathAnimation;
    public bool _hurtAnimation;

    public int damageTaken;

    public Vector2 _hurtDirection;
    public bool _hurtBuffer = false;
    public float _impulseSpeed;

    private Rigidbody2D _rb2d;
    public BoxCollider2D hurtBox;

    public UnityEvent onDeath;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        poolManager = GameObject.FindGameObjectWithTag("PoolsManager").GetComponent<PoolsManager>();
        _rb2d = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        OnStateUpdate();
    }

    private void OnEnable()
    {
        if (currentState == EnemyState.DEATH)
        {
            _isDead = false;
            TransitionToState(EnemyState.IDLE);
        }
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
                
            }

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
                moveAction.MoveProcess(Vector2.zero);
                _hurtAnimation = true;
                _moveBuffer = false;
                enemyGraphics.SetBool("isHurting", true);
                _hurtBuffer = true;
                healthManager.Hurt(damageTaken);
                hitAction.WeakStrikeDeactivate();
                break;
            case EnemyState.DEATH:
                _deathAnimation = true;
                hurtBox.enabled = false;
                moveAction.MoveProcess(Vector2.zero);
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
                    poolManager.ReturnObjectToPool(gameObject, pool.poolName);
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
                _moveBuffer = true;
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
