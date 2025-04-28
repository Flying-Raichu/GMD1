using UnityEngine;
using UnityEngine.EventSystems;

public class GameManager : MonoBehaviour
{
    //TODO We need to further divide this, it's a bit too much already and it's just 2 features
    // TODO: private SerializeField
    public static GameManager instance;

    public GameObject playerPrefab;
    public GameObject pauseMenuPrefab;
    public GameObject eventSystemPrefab;
    public GameObject cameraPrefab;
    public GameObject bgPrefab;
    public GameObject lightPrefab;
    public GameObject asteroidSpawnerPrefab;
    public GameObject mainMenuPrefab;
    public GameObject enemyManagerPrefab;

    private Vector2 storedVelocity;
    private GameObject playerInstance;
    private GameObject pauseMenuInstance;
    private bool isPaused = false;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    void Start()
    {
        SpawnMainUI();
        SpawnLightSource();
        SpawnCamera();
        SpawnBackground();
        SpawnPlayer();
        EnsureEventSystem();
        SpawnAsteroidSpawner();

        if (enemyManagerPrefab != null && FindFirstObjectByType<EnemyManager>() == null)
        {
            Instantiate(enemyManagerPrefab);
            Debug.Log("EnemyManager instantiated.");
            Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Enemy"), false);
        }
    }

    void Update()
    {
        if (InputManager.instance.PausePressed())
        {
            TogglePause();
        }
    }

    private void EnsureEventSystem()
    {
        if (FindFirstObjectByType<EventSystem>() == null)
        {
            Instantiate(eventSystemPrefab);
            Debug.Log("EventSystem was missing. Instantiating a new one.");
        }
    }

    public void SpawnLightSource()
    {
        if (FindFirstObjectByType<Light>() == null)
        {
            Instantiate(lightPrefab);
        }
    }

    public void SpawnBackground()
    {
        if (bgPrefab != null)
        {
            GameObject bgObject = Instantiate(bgPrefab);
            Canvas bgCanvas = bgObject.GetComponent<Canvas>();
            if (bgCanvas != null)
            {
                Camera sceneCamera = FindFirstObjectByType<Camera>();
                if (sceneCamera != null)
                {
                    bgCanvas.worldCamera = sceneCamera;
                }
                else
                {
                    Debug.LogError("No camera found in the scene to assign.");
                }
            }
            
        }
    }

    public void SpawnCamera()
    {
        if (FindFirstObjectByType<Camera>() == null) 
        {
            GameObject camera = Instantiate(cameraPrefab);
            
        }
    }

    public void SpawnPlayer()
    {
        if (playerPrefab != null)
        {
            playerInstance = Instantiate(playerPrefab, Vector3.zero, Quaternion.identity);
            playerInstance.layer = LayerMask.NameToLayer("Player");
        }
    }

    public void SpawnAsteroidSpawner()
    {
        if (asteroidSpawnerPrefab != null)
        {
            asteroidSpawnerPrefab = Instantiate(asteroidSpawnerPrefab);
        }
    }

    public void SpawnMainUI()
    {
        if (mainMenuPrefab != null)
        {
            mainMenuPrefab = Instantiate(mainMenuPrefab);
        }
    }

    public void TogglePause()
    {
        isPaused = !isPaused;

        if (isPaused)
        {
            //Makes sure to store current player velocity
            if (playerInstance != null)
            {
                Rigidbody2D rigidBody = playerInstance.GetComponent<Rigidbody2D>();
                storedVelocity = rigidBody.linearVelocity; 
                rigidBody.linearVelocity = Vector2.zero;   
            }

            Time.timeScale = 0f;
            ShowPauseMenu();
        }
        else
        {
            Time.timeScale = 1f;

            if (playerInstance != null)
            {
                Rigidbody2D rigidBody = playerInstance.GetComponent<Rigidbody2D>();
                rigidBody.linearVelocity = storedVelocity;
            }

            HidePauseMenu();
        }

        Debug.Log("Pause toggled. isPaused: " + isPaused);
    }

    private void ShowPauseMenu()
    {
        if (pauseMenuInstance == null)
        {
            pauseMenuInstance = Instantiate(pauseMenuPrefab, Vector3.zero, Quaternion.identity);
            pauseMenuInstance.GetComponent<Canvas>().worldCamera = Camera.main;
        }
        pauseMenuInstance.SetActive(true);
    }

    private void HidePauseMenu()
    {
        if (pauseMenuInstance != null)
        {
            pauseMenuInstance.SetActive(false);
        }
    }

    public GameObject GetPlayerInstance()
    {
        return playerInstance;
    }
}
