using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class PlayerMovementCC : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] float _horizonMove;
    [SerializeField] float _verticalMove;
    [SerializeField] float _gravity = -9.81f;
    [SerializeField] float _speed = 5f;

    [Header("Components (Private)")]
    [SerializeField] float _moveX;
    [SerializeField] float _moveZ;
    [SerializeField] Vector3 _playerMovement;
    [SerializeField] Vector3 _vel;
    [SerializeField] bool _isGrounded = false;

    [Header("Components (Public)")]
    public CharacterController controller;
    public LayerMask layerMask;
    public float playerHeight = 2f;
    public CameraPos cameraPos;
    public ControlFlip controlFlip;

    // Start is called before the first frame update
    void Start()
    {
        controller = this.gameObject.GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        SetCamEnum();
        MovePlayer();
        SetGravity();
        GroundCheck();
    }

    public void MovePlayer()
    {
        _horizonMove = Input.GetAxis("Horizontal");
        _verticalMove = Input.GetAxis("Vertical");

        if (cameraPos == CameraPos.Left)
        {
            _moveX = _horizonMove;
        }
        else
        {
            _moveX = -_horizonMove;
        }

        if (cameraPos == CameraPos.Left)
        {
            if (controlFlip == ControlFlip.Flipped && _verticalMove <= 0.9f)
            {
                controlFlip = ControlFlip.NoFlip;
                _moveZ = 0;
            }

            if (controlFlip == ControlFlip.NoFlip)
            {
                _moveZ = _verticalMove;
            }
        }
        else
        {
            if (controlFlip == ControlFlip.NoFlip && _verticalMove <= 0.9f)
            {
                controlFlip = ControlFlip.Flipped;
                _moveZ = 0;
            }

            if (controlFlip == ControlFlip.Flipped)
            {
                _moveZ = -_verticalMove;
            }
        }

        _playerMovement = transform.right * _moveX + transform.forward * _moveZ;
        
        controller.Move(_playerMovement * _speed * Time.deltaTime);
    }

    void SetGravity()
    {
        if (_isGrounded)
        {
            _vel.y = -9.81f;
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
        if (this.transform.position.x > 0)
        {
            cameraPos = CameraPos.Left;
        }
        else
        {
            cameraPos = CameraPos.Right;
        }
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