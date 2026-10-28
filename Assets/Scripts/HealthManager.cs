using UnityEngine;

public class HealthManager : MonoBehaviour
{
    public float maxHealth = 100;
    private float currentHealth;
    public CharacterStats otherStats;
    public CharacterStats selfStats;
    public bool isPlayer = false;

    void Start()
    {
        selfStats = GetComponent<CharacterStats>();
        if (gameObject.CompareTag("Player"))
        {
            isPlayer = true;
        }
        else
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (player != null)
            {
                otherStats = player.GetComponent<CharacterStats>();
            }
        }
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
        if (!isPlayer)
        {
            otherStats.AddKill();   
        }
        selfStats.AddDeath();
        gameObject.SetActive(false);
    }

    public void Heal(float amount)
    {
        currentHealth += amount;
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
    }

    public void SetMaxHealth(float newMaxHealth)
    {
        maxHealth = newMaxHealth;
        currentHealth = maxHealth;
    }
}