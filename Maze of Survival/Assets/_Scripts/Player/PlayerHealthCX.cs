using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public static float Global_Health;

    public float maxHealth = 100f;
    private float currentHealth;

    void Start()
    {
        currentHealth = maxHealth;
        Global_Health = maxHealth;
    }

    public void TakeDamage(float amount)
    {
        currentHealth -= amount;
        Debug.Log("Player took damage: " + amount + " | Remaining HP: " + currentHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
        Global_Health = currentHealth;
    }

    void Die()
    {
        GameManager.Instance.RestartLevel();
    }

    public float GetHealth()
    {
        return currentHealth;
    }
}
