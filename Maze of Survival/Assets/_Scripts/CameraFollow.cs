using UnityEngine;
using System.Collections.Generic;

public class CameraFollow : MonoBehaviour
{
    public Transform player; // Reference to the player's transform
    public Vector3 offset = new Vector3(0, 5, -7); // Camera position behind the player
    public float smoothSpeed = 5f; // Speed of the delayed movement
    public float rotationSpeed = 2.0f; // Speed for rotating the camera
    public float delayTime = 0.5f; // Delay time in seconds

    private float yaw = 0.0f; // Horizontal rotation
    private float pitch = 0.0f; // Vertical rotation
    private Queue<Vector3> positionHistory = new Queue<Vector3>(); // Stores past positions

    void Start()
    {
        if (offset == Vector3.zero)
        {
            offset = transform.position - player.position;
        }
    }

    void LateUpdate()
    {
        if (player == null) return;

        // Store the player's current position in history
        positionHistory.Enqueue(player.position);

        // Maintain the delay time
        if (positionHistory.Count > Mathf.CeilToInt(delayTime / Time.deltaTime))
        {
            // Use the old position from 0.5 seconds ago
            Vector3 delayedPosition = positionHistory.Dequeue();

            // Handle camera rotation when the right mouse button is held down
            if (Input.GetMouseButton(1)) // Right mouse button
            {
                yaw += Input.GetAxis("Mouse X") * rotationSpeed;
                pitch -= Input.GetAxis("Mouse Y") * rotationSpeed;

                // Clamp the pitch to prevent excessive rotation
                pitch = Mathf.Clamp(pitch, -35, 60);
            }

            // Calculate the new rotation and position
            Quaternion rotation = Quaternion.Euler(pitch, yaw, 0);
            Vector3 rotatedOffset = rotation * offset;
            Vector3 targetPosition = delayedPosition + rotatedOffset;

            // Smoothly move the camera to the delayed position
            transform.position = Vector3.Lerp(transform.position, targetPosition, smoothSpeed * Time.deltaTime);

            // Ensure the camera looks at the player
            transform.LookAt(player.position + Vector3.up * 2); // Look slightly above the player
        }
    }
}
