using UnityEngine;
using System.Collections;

public class PlayerShoot : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform playerCamera;
    public Transform playerHand;
    public Animator playerAnimator;
    public float throwForce = 10f;
    public float cooldown = 0.5f;

    private bool canShoot = true;

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && canShoot)
        {
            StartCoroutine(ShootWithCooldown());
        }
    }

    IEnumerator ShootWithCooldown()
    {
        canShoot = false;
        ThrowBullet();
        yield return new WaitForSeconds(cooldown);
        canShoot = true;
    }

    void ThrowBullet()
    {
        GameObject bullet = Instantiate(bulletPrefab, playerHand.position, playerCamera.rotation);
        Rigidbody bulletRb = bullet.GetComponent<Rigidbody>();

        playerAnimator.SetTrigger("shoot");

        if (bulletRb != null)
        {
            bulletRb.AddForce(playerCamera.forward * throwForce, ForceMode.VelocityChange);
        }
    }
}
