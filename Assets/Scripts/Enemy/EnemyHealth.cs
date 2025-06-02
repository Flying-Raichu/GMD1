using Player;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private float maxHealth = 100f;
    [SerializeField] private int scoreValue = 50;
    [SerializeField] private int xpValue = 50;
    [SerializeField] private AudioClip deathClip;
    [SerializeField] private GameObject explosionEffect;
    private float currentHealth;
    private bool dying;

    private void Start()
    {
        currentHealth = maxHealth;
        dying = false;
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            Die();
            dying = true;
        }
    }

    private void Die()
    {
        if (dying) return;
        
        var scoreManager = FindFirstObjectByType<ScoreManager>();
        var playerXp = FindFirstObjectByType<PlayerXp>();

        if (scoreManager != null)
        {
            scoreManager.AddScore(scoreValue);
        }
        
        if (playerXp != null)
        {
            playerXp.AddXP(xpValue);
        }
        
        GetComponent<AudioSource>().PlayOneShot(deathClip);
        GameObject bomb = Instantiate(explosionEffect, transform.position, Quaternion.identity);
        bomb.GetComponent<ExplosionEffect>().maxScale = 2f;
        
        Destroy(gameObject, deathClip.length / 3);
    }
}
