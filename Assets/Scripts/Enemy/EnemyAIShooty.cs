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
        if (GameManager.instance.GetPlayerInstance()!= null)
        {
            player = GameManager.instance.GetPlayerInstance().transform;
        }
    }

    void Update()
    {
        if (player == null) return;

        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        Vector2 moveDirection = Vector2.zero;

        if (distanceToPlayer > desiredDistance + 0.5f)
        {
            moveDirection = (player.position - transform.position).normalized; // Move closer
        }
        else if (distanceToPlayer < desiredDistance - 0.5f)
        {
            moveDirection = (transform.position - player.position).normalized; // Move away
        }

        // Add separation force from other enemies
        moveDirection += GetSeparationForce();

        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        rb.AddForce(moveDirection * speed, ForceMode2D.Force);

        rb.linearVelocity = Vector2.ClampMagnitude(rb.linearVelocity, maxSpeed);
    }

    Vector2 GetSeparationForce()
    {
        Vector2 separationForce = Vector2.zero;
        Collider2D[] nearbyEnemies = Physics2D.OverlapCircleAll(transform.position, separationDistance);

        foreach (Collider2D enemy in nearbyEnemies)
        {
            if (enemy.gameObject != this.gameObject && enemy.CompareTag("Enemy"))
            {
                Vector2 pushAway = (Vector2)(transform.position - enemy.transform.position).normalized;
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
