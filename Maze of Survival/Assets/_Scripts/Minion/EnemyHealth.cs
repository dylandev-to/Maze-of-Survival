using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int maxHealth = 10;
    private int currentHealth;

    private Animator animator;

    [SerializeField] private AudioSource hurtFx;

    void Start()
    {
        currentHealth = maxHealth;
        animator = GetComponent<Animator>();
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        hurtFx.Play();
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        animator.SetTrigger("isDead");
        GetComponent<Collider>().enabled = false;
        Invoke(nameof(DestroySelf), 2f);
    }

    public void DestroySelf()
    {
        this.enabled = false;
        Destroy(gameObject);
    }
}
