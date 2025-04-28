using UnityEngine;

public class Asteroid : MonoBehaviour
{
    private float screenWidth, screenHeight;
    [SerializeField] private GameObject debrisPrefab;
    void Start()
    {
        Camera cam = Camera.main;
        if (cam != null)
        {
            Vector3 topRight = cam.ViewportToWorldPoint(new Vector3(1, 1, 0));
            screenWidth = topRight.x;
            screenHeight = topRight.y;
        }
    }

    void Update()
    {
        if (Mathf.Abs(transform.position.x) > screenWidth + 1 || Mathf.Abs(transform.position.y) > screenHeight + 1)
        {
            Destroy(gameObject);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            // TODO: damage and velocity change
        }
        
        BreakAsteroid();
        Destroy(collision.gameObject);
    }
    
    private void BreakAsteroid()
    {
        for (int i = 0; i < 3; i++)
        {
            GameObject fragment = Instantiate(debrisPrefab, transform.position, Quaternion.identity);
            Rigidbody2D rb = fragment.GetComponent<Rigidbody2D>();
            Vector2 forceDirection = Random.insideUnitCircle.normalized;
            rb.AddForce(forceDirection * 5f, ForceMode2D.Impulse);
            
            Destroy(fragment, 3);
        }

        Destroy(gameObject);
    }
}
