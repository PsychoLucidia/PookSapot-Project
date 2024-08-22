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
    
    [Header("Movement")]
    public float horizonMove = 0f;
    public float verticalMove = 0f;
    public float speed = 5f;
    public float dashSpeed = 3f;
    public float gravity = -9.81f;
    public float enemyHeight = 0f;

    [Header("Automated Movement")]
    float _moveTimer;
    float _horizonMoveStateThreshold;
    float _verticalMoveStateThreshold;


    [Header("Components (Private)")]
    [SerializeField] Transform _playerTransform;
    [SerializeField] Vector3 _vel;
    [SerializeField] bool _isGrounded = false;
    [SerializeField] float _horizonMoveIndex;
    [SerializeField] float _verticalMoveIndex; 


    [Header("Components (Public)")]
    public CharacterController controller;

    [Header("Enum States")]
    public EnemyState state;
    public GameState gameState;

    void Awake()
    {
        controller = this.gameObject.GetComponent<CharacterController>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        EnemyGravity();
        MoveIndexRandomizer();

        _timer += Time.deltaTime;
        _threshold = Random.Range(0.5f, 2f);
        if (_timer > _threshold && gameState == GameState.InGame)
        {
            Randomizer(Random.Range(0, 4));
            _timer = 0f;
        }

        switch (state)
        {
            case EnemyState.None:
                break;
            case EnemyState.Move:
                Move();
                break;
            case EnemyState.MoveClose:
                break;
            case EnemyState.MoveFar:
                break;
            case EnemyState.Attack:
                break;
            default:
                break;
        }
    }

    void Randomizer(int action)
    {
        _action = action;

        switch (action)
        {
            case 0:
                state = EnemyState.None;
                break;
            case 1:
                state = EnemyState.Move;
                break;
            case 2:
                state = EnemyState.MoveClose;
                break;
            case 3:
                state = EnemyState.MoveFar;
                break;
            case 4:
                state = EnemyState.Attack;
                break;
            default:
                break;
        }
    }

    void Move()
    {
        _enemyMovement = transform.right * horizonMove + transform.forward * verticalMove;
        controller.Move(_enemyMovement * speed * Time.deltaTime);
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

    void OnCollisionEnter(Collision collision)
    {

    }

    void MoveIndexRandomizer()
    {
        _moveTimer += Time.deltaTime;
        _horizonMoveStateThreshold = Random.Range(0.5f, 1.5f);

        if (_moveTimer > _horizonMoveStateThreshold)
        {
            _horizonMoveIndex = Random.Range(0, 1);
            _moveTimer = 0f;
        }

        if (_moveTimer > _verticalMoveStateThreshold)
        {
            _verticalMoveIndex = Random.Range(0, 1);
            _moveTimer = 0f;
        }
    }


    void HorizontalMovement()
    {

    }
}


public enum EnemyState
{
    None,
    Move,
    MoveClose,
    MoveFar,
    Attack
}
