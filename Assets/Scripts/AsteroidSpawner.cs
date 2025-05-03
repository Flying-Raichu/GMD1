using Spawn;
using UnityEngine;

public class AsteroidSpawner : MonoBehaviour, ISpawnable
{
    [SerializeField] private GameObject asteroidPrefab;
    [SerializeField] private string objName = "AsteroidSpawner";
    [SerializeField] private float spawnRate = 5f; //time in seconds
    private float screenWidth, screenHeight;
    
    public string Name { get => objName; set => objName = value; }
    
    public void Initialize()
    {
        gameObject.name = "AsteroidSpawner";
        SetSpawnBoundary();
    }
    
    void Awake()
    {
        // gives delay of a few seconds for player to get their bearings
        InvokeRepeating(nameof(SpawnAsteroid), 5f, spawnRate);
    }

    void SetSpawnBoundary()
    {
        Camera cam = Camera.main;
        if (cam != null)
        {
            Vector3 topRight = cam.ViewportToWorldPoint(new Vector3(1, 1, 0));
            screenWidth = topRight.x;
            screenHeight = topRight.y;
        }
    }

    void SpawnAsteroid()
    {
        int edge = Random.Range(0, 4); // 0=top, 1=right, 2=bottom, 3=left
        float speed = Random.Range(1f, 3f);
        Vector2 velocity = Vector2.zero;
        Vector2 spawnPosition = Vector2.zero;

        switch (edge)
        {
            case 0:
                spawnPosition = new Vector2(Random.Range(-screenWidth, screenWidth), screenHeight + 1f);
                velocity = new Vector2(Random.Range(-1f, 1f), -1f);
                break;
            case 1:
                spawnPosition = new Vector2(screenWidth + 1f, Random.Range(-screenHeight, screenHeight));
                velocity = new Vector2(-1f, Random.Range(-1f, 1f));
                break;
            case 2:
                spawnPosition = new Vector2(Random.Range(-screenWidth, screenWidth), -screenHeight - 1f);
                velocity = new Vector2(Random.Range(-1f, 1f), 1f);
                break;
            case 3:
                spawnPosition = new Vector2(-screenWidth - 1f, Random.Range(-screenHeight, screenHeight));
                velocity = new Vector2(1f, Random.Range(-1f, 1f));
                break;
        }

        velocity *= speed;

        GameObject asteroid = Instantiate(asteroidPrefab, spawnPosition, Quaternion.identity);
        Rigidbody2D rb = asteroid.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.linearVelocity = velocity;
            rb.angularVelocity = Random.Range(1f, 100f);
        }
    }
}
