using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    private Player.Shield shieldUI;
    private Player.Health healthUI;
    [SerializeField] private AudioClip dmgSound;
    private AudioSource audioSource;

    private void Start()
    {
        StartCoroutine(WaitAndAssignBars());
        audioSource = GetComponent<AudioSource>();
    }

    private System.Collections.IEnumerator WaitAndAssignBars()
    {
        while (shieldUI == null || healthUI == null)
        {
            shieldUI = FindFirstObjectByType<Player.Shield>();
            healthUI = FindFirstObjectByType<Player.Health>();

            yield return null;
        }

        if (shieldUI != null && healthUI != null)
        {
            Debug.Log("Bar UI successfully assigned to PlayerHealth.");
        }
    }

    public void ApplyDamage(float damage)
    {
        PlayWithRandomPitch();
        if (shieldUI != null || healthUI != null)
        {
            if (shieldUI.shield != 0)
            {
                shieldUI.TakeDamage(damage);
            }

            else healthUI.TakeDamage(damage);

            if (healthUI.health <= 0)
            {
                Destroy(gameObject);

                var gameOverManager = FindFirstObjectByType<GameOverManager>();

                gameOverManager.TriggerGameOver();
            }

        }
        else
        {
            Debug.LogWarning("Shield/Health UI is not assigned in PlayerHealth!");
        }
    }
    
    public void PlayWithRandomPitch()
    {
        audioSource.pitch = Random.Range(0.9f, 1.1f);
        audioSource.PlayOneShot(dmgSound);
    }
}
