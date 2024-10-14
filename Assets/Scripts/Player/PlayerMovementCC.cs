using System.Collections;
using System.Collections.Generic;
using FIMSpace.GroundFitter;
using UnityEditor;
using UnityEngine;

public class PlayerMovementCC : MonoBehaviour
{
    [Header("Movement Variables")]
    [SerializeField] float _horizonMove;
    [SerializeField] float _verticalMove;
    [SerializeField] float _gravity = -9.81f;
    [SerializeField] float _speed = 5f;

    [Header("Components And Layers")]
    public FGroundFitter groundFitter;
    public Rigidbody rb;
    public GameObject playerHitbox;
    public Transform lookAtPos;
    public CharacterController controller;
    public LayerMask layerMask;
    public GameObject onFlipObj;
    public float playerHeight = 2f;
    public bool isAttacking = false;

    [Header("Sounds")]
    public AudioSource soundMove;
    public AudioSource[] soundAttack;
    public AudioSource soundDeath;

    [Header("Spider Stat")]
    public SpiderStat spiderStat;

    [Header("State Enums")]
    public CameraPos cameraPos;
    public ControlFlip controlFlip;
    public PlayerState playerState;

    [Header("Private Variables")]
    [SerializeField] float _moveX;
    [SerializeField] float _moveZ;
    [SerializeField] Vector3 _playerMovement;
    [SerializeField] Vector3 _vel;
    [SerializeField] float _hitboxActiveDelay = 0.7f;
    [SerializeField] bool _isGrounded = false;
    [SerializeField] bool _isAttackCoroutineRunning = false;
    Coroutine _attackCoroutine;
    // Start is called before the first frame update
    void Start()
    {
        GameObject root = GameObject.Find("CanvasStatic");
        Transform battleUI = root.transform.Find("BattleUI");
        onFlipObj = battleUI.transform.Find("WarnText").gameObject;

        spiderStat = this.gameObject.GetComponent<SpiderStat>();
        controller = this.gameObject.GetComponent<CharacterController>();
        rb = this.gameObject.GetComponent<Rigidbody>();
        groundFitter = this.gameObject.GetComponent<FGroundFitter>();
        lookAtPos = GameObject.Find("LookAt").transform;
        playerHitbox = this.gameObject.transform.Find("Hitbox").gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.instance.pauseState == PauseState.Unpaused)
        {
            SetGravity();
            GroundCheck();
            PlayerGameOver();

            if (GameManager.instance.gameState == GameState.InGame)
            {
                if (playerState != PlayerState.Attack)
                {
                    MovePlayer();
                    CurrentPlayerState(); 
                }
                SetCamEnum();
            }

        }
    }

    public void MovePlayer()
    {
        _horizonMove = Input.GetAxis("Horizontal");
        _verticalMove = Input.GetAxis("Vertical");

        // Flip left and right conrtols when the camera has been flipped
        if (cameraPos == CameraPos.Left && playerState == PlayerState.Moving)
        {
            _moveX = _horizonMove;
        }
        else if (cameraPos == CameraPos.Right && playerState == PlayerState.Moving)
        {
            _moveX = -_horizonMove;
        }

        // Flip up and down controls when the camera is flipped and forward value is less than 0.9
        if (cameraPos == CameraPos.Left)
        {
            if (controlFlip == ControlFlip.Flipped && _verticalMove <= 0.9f)
            {
                controlFlip = ControlFlip.NoFlip;
                _moveZ = 0;
            }

            if (controlFlip != ControlFlip.NoFlip)
            {
                onFlipObj.SetActive(true);
            }

            if (controlFlip == ControlFlip.NoFlip)
            {
                _moveZ = _verticalMove;
                onFlipObj.SetActive(false);
            }
        }
        else
        {
            if (controlFlip == ControlFlip.NoFlip && _verticalMove <= 0.9f)
            {
                controlFlip = ControlFlip.Flipped;
                _moveZ = 0;
            }

            if (controlFlip != ControlFlip.Flipped)
            {
                onFlipObj.SetActive(true);
            }

            if (controlFlip == ControlFlip.Flipped)
            {
                _moveZ = -_verticalMove;
                onFlipObj.SetActive(false);
            }
        }

        _playerMovement = transform.right * _moveX + transform.forward * _moveZ;
        
        controller.Move(_playerMovement * _speed * Time.deltaTime);

        if (Input.GetMouseButtonDown(0) && playerState != PlayerState.Attack && !_isAttackCoroutineRunning && spiderStat.currentStamina > 15)
        {
            playerState = PlayerState.Attack;
            _attackCoroutine = StartCoroutine(Attack());
            _isAttackCoroutineRunning = true;
            spiderStat.Attack();
            if (soundAttack != null) { soundAttack[Random.Range(0, soundAttack.Length)].Play(); }
        }

        if (Input.GetKeyDown(KeyCode.Escape) && GameManager.instance.gameState == GameState.InGame)
        {
            Fader.instance.gameObject.SetActive(true);
            Fader.instance.FadeEnable(1, 1f, true, 1);
        }
    }

    void CurrentPlayerState()
    {
        Vector3 movementValue = new Vector3(_horizonMove, 0, _verticalMove);

        if (movementValue.magnitude == 0)
        {
            playerState = PlayerState.Idle;
        }
        else if (movementValue.magnitude > 0)
        {
            playerState = PlayerState.Moving;
        }
    }

    void SetGravity()
    {
        if (_isGrounded)
        {
            _vel.y = _gravity;

            controller.Move(_vel * Time.deltaTime);
        }
        else
        {
            _vel.y += _gravity * Time.deltaTime;

            controller.Move(_vel * Time.deltaTime);
        }
    }

    void GroundCheck()
    {
        _isGrounded = Physics.Raycast(transform.position, Vector3.down, playerHeight, layerMask);
    }
    
    void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawRay(transform.position, Vector3.down * playerHeight);
    }

    void SetCamEnum()
    {
        if (this.transform.position.x > lookAtPos.position.x)
        {
            cameraPos = CameraPos.Left;
        }
        else
        {
            cameraPos = CameraPos.Right;
        }
    }

    public void PlayerGameOver()
    {
        if (spiderStat.health <= 0)
        {
            groundFitter.enabled = false;
            controller.enabled = false;
            rb.isKinematic = false;
            rb.AddForce(Vector3.up * 10, ForceMode.Impulse);
            GameManager.instance.gameState = GameState.GameOver;
            playerState = PlayerState.Dead;

            if (soundDeath != null) { soundDeath.Play(); }

            BattleManager.instance.GameOver();
            Debug.Log("Player Defeated");
        }
    }
    
    IEnumerator Attack()
    {
        _moveX = 0;
        _moveZ = 0;

        if (playerState == PlayerState.Attack) { Debug.Log("Attacking"); }
        isAttacking = true;
        yield return new WaitForSeconds(0.1f);
        isAttacking = false;

        yield return new WaitForSeconds(_hitboxActiveDelay);
        playerHitbox.SetActive(true);
        yield return new WaitForSeconds(0.05f);
        playerHitbox.SetActive(false);

        yield return new WaitForSeconds(_hitboxActiveDelay);
        playerState = PlayerState.Idle;
        _isAttackCoroutineRunning = false;
        yield break;
    }
}

public enum CameraPos
{
    Left,
    Right
}

public enum ControlFlip
{
    NoFlip,
    Flipped
}


