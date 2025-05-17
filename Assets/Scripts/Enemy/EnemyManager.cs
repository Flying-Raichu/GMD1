using System.Collections;
using System.Collections.Generic;
using Spawn;
using UnityEngine;

public class EnemyManager : MonoBehaviour, ISpawnable
{
    [SerializeField] public GameObject enemyPrefab;
    [SerializeField] private int enemyCount = 5;
    [SerializeField] private string objName = "EnemyManager";
    [SerializeField] private float waveDelay = 5f;
    private bool waveInProgress = false;
    
    private int currentWave = 1;
    private List<GameObject> activeEnemies = new List<GameObject>();
    public static event System.Action<int> OnWaveStarted;
    
    public string Name { get => objName; set => objName = value; }
    
    public void Initialize()
    {
        gameObject.name = "EnemyManager";
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Enemy"), false);
    }

    void Start()
    {
        waveInProgress = true;
        StartCoroutine(SpawnWave());
    }
    
    void Update()
    {
        activeEnemies.RemoveAll(enemy => enemy == null);

        if (activeEnemies.Count == 0 && !waveInProgress)
        {
            waveInProgress = true;
            StartCoroutine(SpawnWave());
        }
    }
    
    IEnumerator SpawnWave()
    {
        OnWaveStarted?.Invoke(currentWave);
        yield return new WaitForSeconds(waveDelay);

        enemyCount += currentWave + 1;
        for (int i = 0; i < enemyCount; i++)
        {
            var spawnPosition = GetRandomCornerPosition();
            GameObject enemy = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
            enemy.layer = LayerMask.NameToLayer("Enemy");
            activeEnemies.Add(enemy);
        }

        currentWave++;
        waveInProgress = false;
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
