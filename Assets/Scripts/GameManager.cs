using UnityEngine;
using UnityEngine.EventSystems;

public class GameManager : MonoBehaviour
{
    //TODO We need to further divide this, it's a bit too much already and it's just 2 features
    public static GameManager instance;

    public GameObject playerPrefab;
    public GameObject pauseMenuPrefab;
    public GameObject eventSystemPrefab;
    public GameObject cameraPrefab;
    public GameObject lightPrefab;

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
        SpawnLightSource();
        SpawnCamera();
        SpawnPlayer();
        EnsureEventSystem();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
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

    public void SpawnCamera()
    {
        if (FindFirstObjectByType<Camera>() == null) 
        {
            Instantiate(cameraPrefab);
        }
    }

    public void SpawnPlayer()
    {
        if (playerPrefab != null)
        {
            playerInstance = Instantiate(playerPrefab, Vector3.zero, Quaternion.identity);
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
}
