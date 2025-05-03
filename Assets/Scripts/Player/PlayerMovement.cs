using Spawn;
using UnityEngine;
using UnityEngine.InputSystem;

public class ShipMovement : MonoBehaviour, ISpawnable
{
    [SerializeField] private float thrustPower = 5f;
    [SerializeField] private float rotationSpeed = 5f;
    [SerializeField] private float dragFactor = 0.99f;

    public float forceMultiplier = 5f;

    private Rigidbody2D rigidBody;
    
    [SerializeField] private string objName = "Player";

    public string Name { get => objName; set => objName = value; }

    public void Initialize()
    {
        gameObject.name = "Player";
    }

    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        rigidBody.linearDamping = 0;
    }

    void Update()
    {
        if (Gamepad.current != null && InputManager.instance.GetMovement() != Vector2.zero) //if joystick has force applied
        {
            RotateWithController();
        }
        else
        {
            RotateTowardsMouse();
        }
        
        CheckScreenBounds();
    }

    void FixedUpdate()
    {
        ApplyThrust();
        ApplyDrag();
    }

    void RotateTowardsMouse()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(InputManager.instance.GetMousePosition());
        mousePosition.z = 0f;

        Vector3 direction = (mousePosition - transform.position).normalized;
        float targetAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;

        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, 0, targetAngle), rotationSpeed * Time.deltaTime);
    }

    void RotateWithController()
    {
        Vector2 input = InputManager.instance.GetMovement();
        
        float targetAngle = Mathf.Atan2(input.y, input.x) * Mathf.Rad2Deg - 90f;
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, 0, targetAngle), rotationSpeed * Time.deltaTime);

    }

    void ApplyThrust()
    {
        if (InputManager.instance.APressed())
        {
            rigidBody.linearVelocity += thrustPower * Time.fixedDeltaTime * (Vector2)transform.up;
        }
    }

    void ApplyDrag()
    {
        if (rigidBody.linearVelocity.magnitude > 0.01f)
        {
            rigidBody.linearVelocity *= dragFactor;
        }
    }

    void CheckScreenBounds()
    {
        var screenLeft = Camera.main.ScreenToWorldPoint(Vector3.zero).x;
        var screenRight = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, 0, 0)).x;
        var screenBottom = Camera.main.ScreenToWorldPoint(Vector3.zero).y;
        var screenTop = Camera.main.ScreenToWorldPoint(new Vector3(0, Screen.height, 0)).y;

        var shipWidth = GetComponent<SpriteRenderer>().bounds.extents.x * 0.9f;
        var shipHeight = GetComponent<SpriteRenderer>().bounds.extents.y * 0.9f;

        Vector3 newPosition = transform.position;

        // Wrap Horizontally
        if (newPosition.x < screenLeft - shipWidth)
            newPosition.x = screenRight + shipWidth * 0.8f;
        else if (newPosition.x > screenRight + shipWidth)
            newPosition.x = screenLeft - shipWidth * 0.8f;

        // Wrap Vertically
        if (newPosition.y < screenBottom - shipHeight)
            newPosition.y = screenTop + shipHeight * 0.8f;
        else if (newPosition.y > screenTop + shipHeight)
            newPosition.y = screenBottom - shipHeight * 0.8f;

        transform.position = newPosition;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            if (collision.gameObject.TryGetComponent<Rigidbody2D>(out var enemyRb))
            {
                ApplyPhysicsForces(rigidBody, enemyRb, collision);
                Debug.Log("Newtonian collision applied!");
            }
        }
    }

    void ApplyPhysicsForces(Rigidbody2D rb1, Rigidbody2D rb2, Collision2D collision)
    {
        float m1 = rb1.mass;
        float m2 = rb2.mass;

        Vector2 impactDirection = (rb1.position - rb2.position).normalized;
        Vector2 relativeVelocity = rb1.linearVelocity - rb2.linearVelocity;

        Vector2 forceOnPlayer = (m2 / (m1 + m2)) * forceMultiplier * relativeVelocity.magnitude * impactDirection;
        Vector2 forceOnEnemy = (m1 / (m1 + m2)) * forceMultiplier * relativeVelocity.magnitude * -impactDirection;

        float maxForce = 10f;
        forceOnPlayer = Vector2.ClampMagnitude(forceOnPlayer, maxForce);
        forceOnEnemy = Vector2.ClampMagnitude(forceOnEnemy, maxForce);

        // apply force
        rb1.AddForce(forceOnPlayer, ForceMode2D.Impulse);
        rb2.AddForce(forceOnEnemy, ForceMode2D.Impulse);

        Debug.Log($"Forces applied: Player {forceOnPlayer}, Enemy {forceOnEnemy}");
    }
}
