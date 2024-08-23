using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    [Header("Components (Public)")]
    public Animator animator;
    public EnemyManager enemyManager;
    public bool isEnemy = false;

    [Header("Components (Private)")]
    [SerializeField] float _horizonMove;
    [SerializeField] float _verticalMove;


    // Start is called before the first frame update
    void Awake()
    {
        Transform rootObj = this.gameObject.transform;

        animator = rootObj.transform.Find("Model").GetComponent<Animator>();
        enemyManager = this.gameObject.GetComponent<EnemyManager>();
    }

    // Update is called once per frame
    void Update()
    {
        FloatValues();

        animator.SetFloat("Turn", _horizonMove);
        animator.SetFloat("Walk", _verticalMove);
        if (_horizonMove >= 0.1f || _horizonMove <= -0.1f || _verticalMove >= 0.1f || _verticalMove <= -0.1f)
        {
            animator.SetBool("IsMoving", true);
        }
        else
        {
            animator.SetBool("IsMoving", false);
        }

    }

    void FloatValues()
    {
        if (!isEnemy)
        {
            _horizonMove = Input.GetAxis("Horizontal");
            _verticalMove = Input.GetAxis("Vertical");
        }
        else
        {
            _horizonMove = Mathf.Lerp(_horizonMove, enemyManager.horizonMove, 10f * Time.deltaTime);
            _verticalMove = Mathf.Lerp(_verticalMove, enemyManager.verticalMove, 10f * Time.deltaTime);
        }

    }
}
