using UnityEngine;

public class WitchMovement : MonoBehaviour
{
    public float speed = 5f; // Movement speed
    public float acceleration = 10f; // How fast the player reaches max speed
    public float deceleration = 8f; // How quickly the player slows down
    public float jumpForce = 7f; // Jump force
    private Rigidbody rb;
    public Transform cameraTransform; // Reference to the camera for direction
    private Vector3 currentVelocity; // Stores current movement velocity

    void Start()
    {
        rb = GetComponent<Rigidbody>(); // Get the Rigidbody component
    }

    void FixedUpdate()
    {
        if (cameraTransform == null) return;

        // Get WASD input
        float moveX = Input.GetAxis("Horizontal"); // A/D (Left/Right)
        float moveZ = Input.GetAxis("Vertical");   // W/S (Forward/Backward)

        // Get camera forward direction but ignore Y-axis (to keep movement flat)
        Vector3 cameraForward = cameraTransform.forward;
        cameraForward.y = 0;
        cameraForward.Normalize();

        // Get the right direction relative to the camera
        Vector3 cameraRight = cameraTransform.right;
        cameraRight.y = 0;
        cameraRight.Normalize();

        // Compute desired movement direction
        Vector3 movement = (cameraForward * moveZ + cameraRight * moveX).normalized;

        if (movement.magnitude > 0)
        {
            // Accelerate towards the desired movement direction
            currentVelocity = Vector3.Lerp(currentVelocity, movement * speed, acceleration * Time.deltaTime);
            transform.forward = movement; // Rotate towards movement direction
        }
        else
        {
            // Gradually slow down when no keys are pressed
            currentVelocity = Vector3.Lerp(currentVelocity, Vector3.zero, deceleration * Time.deltaTime);
        }

        // Apply velocity while preserving gravity
        rb.linearVelocity = new Vector3(currentVelocity.x, rb.linearVelocity.y, currentVelocity.z);
    }

    void Update()
    {
        // Jump when pressing SPACE (no ground check)
        if (Input.GetKeyDown(KeyCode.Space))
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }
}
