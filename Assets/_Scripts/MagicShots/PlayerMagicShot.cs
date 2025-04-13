using UnityEngine;
using System.Collections;

public class PlayerMagicShot : MonoBehaviour
{
    public float duration = 5f;
    public int damage = 5;

    void Start()
    {
        StartCoroutine(DestroyAfterTime(duration));
    }

    IEnumerator DestroyAfterTime(float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            EnemyHealth enemyHealth = other.GetComponent<EnemyHealth>();
            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(damage);
            }

            //Destroy(gameObject);
        }
    }
}
