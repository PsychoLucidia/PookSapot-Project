using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerTranslate : MonoBehaviour
{
    float _playerSpeed = 5.0f;

    [SerializeField] float _horizontalInput;
    [SerializeField] float _verticalInput;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        MovePlayer();
    }

    void MovePlayer()
    {
        _horizontalInput = Input.GetAxis("Horizontal");
        _verticalInput = Input.GetAxis("Vertical");

        transform.Translate(Vector3.right * _horizontalInput * _playerSpeed * Time.deltaTime);
        transform.Translate(Vector3.forward * _verticalInput * _playerSpeed * Time.deltaTime);
    }
}
