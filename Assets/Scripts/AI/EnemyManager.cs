using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyManager : MonoBehaviour
{
    [Header("Debug")]
    [SerializeField] float _threshold;
    [SerializeField] float _timer = 0f;
    [SerializeField] int _action = 0;
    [SerializeField] Vector3 _enemyMovement;
    [SerializeField] float _playerDistance; 
    [SerializeField] float _tempTimer = 0f;
    
    [Header("Movement")]
    public float horizonMove = 0f;
    public float verticalMove = 0f;
    public float speed = 5f;
    public float dashSpeed = 3f;
    public float gravity = -9.81f;
    public float enemyHeight = 0f;

    [Header("Automated Movement")]
    float _horizonMoveTimer;
    float _verticalMoveTimer;
    float _horizonMoveStateThreshold;
    float _verticalMoveStateThreshold;


    [Header("Components (Private)")]
    [SerializeField] Transform _playerTransform;
    [SerializeField] Vector3 _vel;
    [SerializeField] bool _isGrounded = false;
    [SerializeField] float _horizonMoveIndex;
    [SerializeField] float _verticalMoveIndex;

    [Header("Components (Public)")]
    public float moveMaxDistance;
    public CharacterController controller;
    public LayerMask groundLayer;
    public bool isAttacking;

    [Header("Enum States")]
    public EnemyState state;

    [Header("Attack Settings")]
    public float atkMinDistance;
    public float atkMaxDistance;
    public float minAttackProbability;
    public float maxAttackProbability;

    void Awake()
    {
        controller = this.gameObject.GetComponent<CharacterController>();
        _playerTransform = GameObject.Find("Player").transform;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.instance.gameState == GameState.InGame)
        {
            EnemyGravity();
            MoveIndexRandomizer();
            PlayerDistance();
            EnemyPosition();

            if (state != EnemyState.Attack || state != EnemyState.ForceForward || state != EnemyState.ForceBackward) { _timer += Time.deltaTime; }
            if (state == EnemyState.ForceBackward) { _tempTimer -= Time.deltaTime; }

            _threshold = Random.Range(0.5f, 2f);
            if (_timer > _threshold)
            {
                Randomizer(Random.Range(0, 4));
                _timer = 0f;
            }

            switch (state)
            {
                case EnemyState.None: EnemyIdle(); break;
                case EnemyState.Move: Move(); break;
                case EnemyState.MoveClose: MoveClose(); break;
                case EnemyState.MoveFar: MoveFar(); break;
                case EnemyState.ForceForward: ForceForward(); break;
                case EnemyState.ForceBackward: ForceBackward(); break;
                default: break;
            }
        }
    }

    void Randomizer(int action)
    {
        _action = action;

        switch (action)
        {
            case 0: state = EnemyState.None; break;
            case 1: state = EnemyState.Move; break;
            case 2: state = EnemyState.MoveClose; break;
            case 3: state = EnemyState.MoveFar; break;
            default: state = EnemyState.None; break;
        }
    }

    void EnemyIdle()
    {
        horizonMove = 0f;
        verticalMove = 0f;
    }

    void Move()
    {
        if (_horizonMoveIndex > 0)
        {
            horizonMove = 1f;
        }
        else
        {
            horizonMove = -1f;
        }

        verticalMove = 0f;

        _enemyMovement = transform.right * horizonMove + transform.forward * verticalMove;
        EnemyMovement(_enemyMovement);
    }

    void MoveClose()
    {
        if (_horizonMoveIndex > 0)
        {
            horizonMove = 1f;
        }
        else
        {
            horizonMove = -1f;
        }

        if (_verticalMoveIndex > -2f)
        {
            verticalMove = 1f;
        }
        else
        {
            verticalMove = -1f;
        }

        _enemyMovement = transform.right * horizonMove + transform.forward * verticalMove;
        EnemyMovement(_enemyMovement);
    }

    void MoveFar()
    {
        if (_horizonMoveIndex > 0)
        {
            horizonMove = 1f;
        }
        else
        {
            horizonMove = -1f;
        }

        if (_verticalMoveIndex > 3f)
        {
            verticalMove = 1f;
        }
        else
        {
            verticalMove = -1f;
        }

        _enemyMovement = transform.right * horizonMove + transform.forward * verticalMove;
        EnemyMovement(_enemyMovement);
    }

    void ForceForward()
    {
        if (_horizonMoveIndex > 0)
        {
            horizonMove = 1f;
        }
        else
        {
            horizonMove = -1f;
        }

        verticalMove = 1f;

        _enemyMovement = transform.right * horizonMove + transform.forward * verticalMove;
        EnemyMovement(_enemyMovement);
    }

    void ForceBackward()
    {
        if (_horizonMoveIndex > 0)
        {
            horizonMove = 1f;
        }
        else
        {
            horizonMove = -1f;
        }

        verticalMove = -1f;

        _enemyMovement = transform.right * horizonMove + transform.forward * verticalMove;
        EnemyMovement(_enemyMovement);
    }

    void EnemyGravity()
    {
        if (_isGrounded)
        {
            _vel.y = gravity;
        }
        else
        {
            _vel.y += gravity * Time.deltaTime;
            controller.Move(_vel * Time.deltaTime);
        }
    }

    void EnemyMovement(Vector3 value)
    {
        controller.Move(value * speed * Time.deltaTime);
    }

    void PlayerDistance()
    {
        _playerDistance = Vector3.Distance(_playerTransform.position, this.transform.position);

        if (moveMaxDistance < _playerDistance)
        {
            state = EnemyState.ForceForward;
        }
        else if (state == EnemyState.ForceForward)
        {
            state = EnemyState.Move;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            state = EnemyState.None;
        }
    }

    void MoveIndexRandomizer()
    {
        _horizonMoveTimer += Time.deltaTime;
        _verticalMoveTimer += Time.deltaTime;
        _horizonMoveStateThreshold = Random.Range(0.5f, 1.5f);
        _verticalMoveStateThreshold = Random.Range(0.5f, 1.5f);

        if (_horizonMoveTimer > _horizonMoveStateThreshold)
        {
            _horizonMoveIndex = Random.Range(-5f, 5f);
            _horizonMoveTimer = 0f;
        }

        if (_verticalMoveTimer > _verticalMoveStateThreshold)
        {
            _verticalMoveIndex = Random.Range(-5f, 5f);
            _verticalMoveTimer = 0f;
        }
    }

    void EnemyPosition()
    {
        if (this.transform.position.x > 15f)
        {
            state = EnemyState.ForceBackward;
            _tempTimer = Random.Range(0.5f, 1.5f);
        }

        if (_tempTimer <= 0f && state == EnemyState.ForceBackward)
        {
            Debug.Log("Backward");
            state = EnemyState.Move;
        }
    }
}


public enum EnemyState
{
    None,
    Move,
    MoveClose,
    MoveFar,
    Attack,
    ForceForward,
    ForceBackward
}
