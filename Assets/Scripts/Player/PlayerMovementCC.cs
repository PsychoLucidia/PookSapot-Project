using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementCC : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] float _horizonMove;
    [SerializeField] float _verticalMove;
    [SerializeField] float _gravity = -9.81f;
    [SerializeField] float _speed = 5f;

    [Header("Components (Private)")]
    [SerializeField] Vector3 _playerMovement;
    [SerializeField] Vector3 _vel;
    [SerializeField] bool _isGrounded = false;
    [SerializeField] bool _isCamFlipped = false;

    [Header("Components (Public)")]
    public CharacterController controller;
    public LayerMask layerMask;
    public float playerHeight = 2f;

    // Start is called before the first frame update
    void Start()
    {
        controller = this.gameObject.GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        MovePlayer();
        SetGravity();
        GroundCheck();
    }

    public void MovePlayer()
    {
        _horizonMove = Input.GetAxis("Horizontal");
        _verticalMove = Input.GetAxis("Vertical");


        if (this.transform.position.x >= 0.1f)
        {
            _playerMovement = transform.right * _horizonMove + transform.forward * _verticalMove;

            
        }
        else
        {
            if (this.transform.position.x <= 0.1f)
            _playerMovement = transform.right * -_horizonMove + transform.forward * _verticalMove;
        }
        
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
}
