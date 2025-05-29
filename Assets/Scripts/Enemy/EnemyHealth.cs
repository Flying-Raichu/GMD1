using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private float maxHealth = 100f;
    [SerializeField] private int scoreValue = 50;
    [SerializeField] private int xpValue = 50;
    private float currentHealth;

    private void Start()
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

    private void Die()
    {
        var scoreManager = FindFirstObjectByType<ScoreManager>();
        var playerXp = FindFirstObjectByType<PlayerXp>();

        if (scoreManager != null)
        {
            scoreManager.AddScore(scoreValue);
        }
        else
        {
            Debug.LogWarning("ScoreManager not found!");
        }

        if (playerXp != null)
        {
            playerXp.AddXP(xpValue);
        }
        else
        {
            Debug.LogWarning("PlayerXp not found!");
        }

        Destroy(gameObject);
    }
}
