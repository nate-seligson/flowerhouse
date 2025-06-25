using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    public float speed = 2f;
    public float rotationSpeed = 10f;
    public Animator an;
    public Transform camera;

    private CharacterController controller;

    void Start()
    {
        controller = GetComponent<CharacterController>();
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
            Vector3 moveDirection = camera.TransformDirection(inputDirection);
            moveDirection.y = 0f;
            moveDirection.Normalize();

            // Move using CharacterController
            controller.SimpleMove(moveDirection * speed);

            // Rotate character to face movement direction (adjust offset if needed)
            Quaternion targetRotation = Quaternion.LookRotation(moveDirection) * Quaternion.Euler(0, -90, 0);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
    }
}
