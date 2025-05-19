using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    private Player.Shield shieldUI;
    private Player.Health healthUI;

    private void Start()
    {
        StartCoroutine(WaitAndAssignBars());
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
}
