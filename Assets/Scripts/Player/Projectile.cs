using UnityEngine;

public class Projectile : MonoBehaviour
{
    public GameObject shooter;
    public float damage;
    
    void Update()
    {
        if (IsOffScreen())
        {
            Destroy(gameObject);
        }
    }
    
    private bool IsOffScreen()
    {
        Vector3 screenPosition = Camera.main.WorldToViewportPoint(transform.position);
        return screenPosition.x < 0 || screenPosition.x > 1 || screenPosition.y < 0 || screenPosition.y > 1;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject == shooter) return;

        if (collision.gameObject.CompareTag("Player") &&
            collision.gameObject.TryGetComponent<PlayerHealth>(out var playerHealth))
        {
            playerHealth.ApplyDamage(damage);
            Destroy(gameObject);
        }
    }
}
