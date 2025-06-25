using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 2f;
    public float rotationSpeed = 10f;
    public Animator an;
    public Transform camera;

    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 inputDirection = new Vector3(horizontalInput, 0f, verticalInput).normalized;
        an.SetBool("walk", inputDirection.magnitude > 0);

        if (inputDirection.magnitude > 0.1f)
        {
            // Convert input direction relative to camera
            Vector3 moveDirection = camera.TransformDirection(inputDirection);
            moveDirection.y = 0f;
            moveDirection.Normalize();

            // Move the character in world space
            transform.position += moveDirection * speed * Time.deltaTime;

            // Apply an offset rotation (-90° here) so the model faces correctly.
            // Adjust the angle if your model requires a different offset.
            Quaternion targetRotation = Quaternion.LookRotation(moveDirection) * Quaternion.Euler(0, -90, 0);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
    }
}
