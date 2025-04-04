using UnityEngine;

public class CameraController : MonoBehaviour
{
    // Assign the plane object (or any target) in the Unity Editor.
    public Transform target;

    // Speed settings
    public float rotationSpeed = 5f;
    public float zoomSpeed = 10f;
    
    // Minimum and maximum zoom distances
    public float minDistance = 2f;
    public float maxDistance = 20f;
    
    // Current distance from the target
    private float currentDistance;

    void Start()
    {
        if (target == null)
        {
            Debug.LogError("Target is not set. Please assign a plane object to 'target'.");
            return;
        }
        // Initialize the current distance from the target
        currentDistance = Vector3.Distance(transform.position, target.position);
    }

    void Update()
    {
        if (target == null) return;
        HandleRotation();
        HandleZoom();
    }

    void HandleRotation()
    {
            float mouseX = Input.GetAxis("Mouse X") * rotationSpeed;
            float mouseY = Input.GetAxis("Mouse Y") * rotationSpeed;

            // Rotate horizontally around the target's up axis
            transform.RotateAround(target.position, Vector3.up, mouseX);
            
            // Rotate vertically around the camera's right axis
            transform.RotateAround(target.position, transform.right, -mouseY);
    }

    void HandleZoom()
    {
        // Zoom using the scroll wheel.
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        if (Mathf.Abs(scroll) > 0.01f)
        {
            currentDistance -= scroll * zoomSpeed;
            currentDistance = Mathf.Clamp(currentDistance, minDistance, maxDistance);
            
            // Update the camera position while keeping it looking at the target
            transform.position = target.position - transform.forward * currentDistance;
        }
    }
}
