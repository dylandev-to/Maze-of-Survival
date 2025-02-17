using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    public GameObject bulletPrefab;   // Bullet prefab to be instantiated
    public Transform playerCamera;    // The player's camera for direction
    public Transform playerHand;      // The player's hand position to spawn the bullet
    public float throwForce = 10f;    // The force applied to the bullet

    void Update()
    {
        // Shoot when the left mouse button is pressed
        if (Input.GetMouseButtonDown(0))
        {
            ThrowBullet();
        }
    }

    void ThrowBullet()
    {
        // Instantiate the bullet at the player's hand position
        // This will spawn the bullet slightly in front of the hand and align it with the camera's forward direction
        GameObject bullet = Instantiate(bulletPrefab, playerHand.position, playerCamera.rotation);

        // Get the Rigidbody component of the bullet
        Rigidbody bulletRb = bullet.GetComponent<Rigidbody>();

        if (bulletRb != null)
        {
            // Apply force to the bullet in the direction the camera is facing
            bulletRb.AddForce(playerCamera.forward * throwForce, ForceMode.VelocityChange);
        }
    }
}
