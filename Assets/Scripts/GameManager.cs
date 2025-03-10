using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject playerPrefab;

    private GameObject playerInstance;

    void Start()
    {
        SpawnPlayer();
    }

    void SpawnPlayer()
    {
        if (playerPrefab != null)
        {
            // Spawn the player at (0,0) in the world
            playerInstance = Instantiate(playerPrefab, Vector3.zero, Quaternion.identity);
        }
        else
        {
            Debug.LogError("Player prefab is not assigned in GameManager!");
        }
    }
}
