using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    public float speed = 2f;
    public float rotationSpeed = 10f;
    public Animator an;
    public Transform camera;

    private Rigidbody rb;
    private Vector3 moveDirection;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true; // Prevent rigidbody from tipping over
    }

    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 inputDirection = new Vector3(horizontalInput, 0f, verticalInput).normalized;
        an.SetBool("walk", inputDirection.magnitude > 0);

        if (inputDirection.magnitude > 0.1f)
        {
            // Get direction relative to camera
            moveDirection = camera.TransformDirection(inputDirection);
            moveDirection.y = 0f;
            moveDirection.Normalize();
        }
        else
        {
            moveDirection = Vector3.zero;
        }
    }

    void FixedUpdate()
    {
        if (moveDirection != Vector3.zero)
        {
            // Move
            Vector3 targetPosition = rb.position + moveDirection * speed * Time.fixedDeltaTime;
            rb.MovePosition(targetPosition);

            // Rotate
            Quaternion targetRotation = Quaternion.LookRotation(moveDirection) * Quaternion.Euler(0, -90, 0);
            Quaternion smoothedRotation = Quaternion.Slerp(rb.rotation, targetRotation, rotationSpeed * Time.fixedDeltaTime);
            rb.MoveRotation(smoothedRotation);
        }
    }
}
