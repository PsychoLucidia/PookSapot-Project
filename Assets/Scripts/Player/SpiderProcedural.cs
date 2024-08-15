using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderProcedural : MonoBehaviour
{
    public float speed = 5f;
    public float arcRadius = 1f, arcAngle = 45f;
    public float rotationSpeed = 5f; 
    public LayerMask groundLayer;
    [SerializeField] private int arcResolution = 6;
    private Animator animator;
    public Vector3 moveDirection;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");

        moveDirection = new Vector3(x, 0, y).normalized;
        // animator.SetBool("isWalking", moveDirection.magnitude > 0);

        if (moveDirection.magnitude > 0)
        {
            arcRadius = speed * Time.deltaTime;

            if (ArcCast(transform.position, Quaternion.LookRotation(moveDirection), arcAngle, arcRadius, arcResolution, groundLayer, out RaycastHit hit))
            {
                transform.position = hit.point;
                Quaternion targetRotation = Quaternion.FromToRotation(Vector3.up, hit.normal) * Quaternion.LookRotation(moveDirection);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
            }
            else
            {
                transform.position += moveDirection * speed * Time.deltaTime;
                Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
            }
        }
    }

    public bool ArcCast(Vector3 center, Quaternion rotation, float angle, float radius, int resolution, LayerMask layer, out RaycastHit hit)
    {
        rotation *= Quaternion.Euler(-angle / 2, 0, 0);

        for (int i = 0; i < resolution; i++)
        {
            Vector3 A = center + rotation * Vector3.forward * radius;
            rotation *= Quaternion.Euler(angle / resolution, 0, 0);
            Vector3 B = center + rotation * Vector3.forward * radius;
            Vector3 AB = B - A;

            
            if (Physics.Raycast(A, AB, out hit, AB.magnitude * 1.001f, layer))
                return true;
        }
        hit = new RaycastHit();
        return false;
    }
}