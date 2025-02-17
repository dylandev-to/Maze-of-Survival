using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int maxHealth = 10;
    private int currentHealth;

    private Animator animator;

    void Start()
    {
        currentHealth = maxHealth;
        animator = GetComponent<Animator>();
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        animator.SetTrigger("isDead");
        GetComponent<Collider>().enabled = false;
        this.enabled = false;
        Invoke(nameof(DestroySelf), 2f);
    }

    public void DestroySelf()
    {
        Destroy(gameObject);
    }
}
