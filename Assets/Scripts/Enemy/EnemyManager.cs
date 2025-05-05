using Spawn;
using UnityEngine;

public class EnemyManager : MonoBehaviour, ISpawnable
{
    public GameObject enemyPrefab;
    public int enemyCount = 5;
    public float spawnRadius = 6f;
    [SerializeField] private string objName = "EnemyManager";
    
    public string Name { get => objName; set => objName = value; }
    
    public void Initialize()
    {
        gameObject.name = "EnemyManager";
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Enemy"), false);
    }

    void Start()
    {
        SpawnEnemies();
    }

    void SpawnEnemies()
    {
        for (int i = 0; i < enemyCount; i++)
        {
            var spawnPosition = GetRandomCornerPosition();
            GameObject enemy = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
            enemy.layer = LayerMask.NameToLayer("Enemy");
        }
    }

    Vector3 GetRandomCornerPosition()
    {
        Camera cam = Camera.main;
        float zPos = 0f;

        var bottomLeft = cam.ViewportToWorldPoint(new Vector3(0, 0, cam.nearClipPlane));
        var bottomRight = cam.ViewportToWorldPoint(new Vector3(1, 0, cam.nearClipPlane));
        var topLeft = cam.ViewportToWorldPoint(new Vector3(0, 1, cam.nearClipPlane));
        var topRight = cam.ViewportToWorldPoint(new Vector3(1, 1, cam.nearClipPlane));

        float offset = 2f;
        bottomLeft += new Vector3(-offset, -offset, zPos);
        bottomRight += new Vector3(offset, -offset, zPos);
        topLeft += new Vector3(-offset, offset, zPos);
        topRight += new Vector3(offset, offset, zPos);

        Vector3[] corners = { bottomLeft, bottomRight, topLeft, topRight };
        var spawnPosition = corners[Random.Range(0, corners.Length)];

        spawnPosition += new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0);

        return spawnPosition;
    }
}
