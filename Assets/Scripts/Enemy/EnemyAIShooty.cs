using Player;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public float speed = 2f;
    public float desiredDistance = 3f;
    public float separationDistance = 1.5f;
    public float avoidanceForce = 3f;
    public float maxSpeed = 3f;

    private Transform player;

    void Start()
    {
        if (PlayerManager.Instance.GetPlayer()!= null)
        {
            player = PlayerManager.Instance.GetPlayer().transform;
        }
    }

    void Update()
    {
        if (!player) return;
        
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        Vector2 moveDirection = Vector2.zero;

        if (distanceToPlayer > desiredDistance + 0.5f)
        {
            moveDirection = (player.position - transform.position).normalized;
        }
        else if (distanceToPlayer < desiredDistance - 0.5f)
        {
            moveDirection = (transform.position - player.position).normalized;
        }

        moveDirection += GetSeparationForce();
        TurnToPlayer();

        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        rb.AddForce(moveDirection * speed, ForceMode2D.Force);

        rb.linearVelocity = Vector2.ClampMagnitude(rb.linearVelocity, maxSpeed);
    }

    private void TurnToPlayer()
    {
        Vector2 direction = (player.position - transform.position).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, angle - 90f);
    }

    Vector2 GetSeparationForce()
    {
        Vector2 separationForce = Vector2.zero;
        Collider2D[] nearbyEnemies = Physics2D.OverlapCircleAll(transform.position, separationDistance);

        foreach (Collider2D enemy in nearbyEnemies)
        {
            if (enemy.gameObject != this.gameObject && enemy.CompareTag("Enemy"))
            {
                Vector2 pushAway = (transform.position - enemy.transform.position).normalized;
                separationForce += pushAway * avoidanceForce;
            }
        }

        return separationForce.normalized;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Enemy collided with: " + collision.gameObject.name + " (Layer: " + LayerMask.LayerToName(collision.gameObject.layer) + ")");
    }
}
