using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public float maxHealth = 100;
    private float currentHealth;
    public CharacterStats playerStats;
    public CharacterStats selfStats;

    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        playerStats.AddKill();
        selfStats.AddDeath();
        gameObject.SetActive(false);
    }
}