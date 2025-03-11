using UnityEngine;

public class ShipMovement : MonoBehaviour
{
    public float thrustPower = 5f;
    public float rotationSpeed = 5f;
    public float dragFactor = 0.99f;

    private Rigidbody2D rigidBody;
    private bool usingMouse = true;

    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        rigidBody.linearDamping = 0;
    }

    void Update()
    {
        DetectInputMethod();

        if (usingMouse)
            RotateTowardsMouse();
        else
            RotateWithController();

        CheckScreenBounds();
    }

    void FixedUpdate()
    {
        ApplyThrust();
        ApplyDrag();
    }

    void RotateTowardsMouse()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0f;

        Vector3 direction = (mousePosition - transform.position).normalized;
        float targetAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;

        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, 0, targetAngle), rotationSpeed * Time.deltaTime);
    }

    void RotateWithController()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveY = Input.GetAxis("Vertical");

        if (moveX != 0 || moveY != 0)
        {
            Vector3 direction = new Vector3(moveX, moveY, 0).normalized;
            float targetAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;

            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, 0, targetAngle), rotationSpeed * Time.deltaTime);
        }
    }

    void ApplyThrust()
    {
        if (Input.GetKey(KeyCode.JoystickButton0) || Input.GetMouseButton(0)) //TODO Check this against the arcade machine.
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

    void DetectInputMethod()
    {
        if (Mathf.Abs(Input.GetAxis("Horizontal")) > 0.1f || Mathf.Abs(Input.GetAxis("Vertical")) > 0.1f)
        {
            usingMouse = false;
        }
        if (Input.GetAxis("Mouse X") != 0 || Input.GetAxis("Mouse Y") != 0)
        {
            usingMouse = true;
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
}
