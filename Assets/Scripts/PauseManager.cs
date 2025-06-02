using Spawn;
using UnityEngine;

public class PauseManager : MonoBehaviour, ISpawnable
{
    public static PauseManager Instance { get; private set; }

    [SerializeField] private GameObject pauseMenuPrefab;

    private GameObject pauseMenuInstance;
    private GameObject player;
    private bool isPaused;
    private Vector2 storedVelocity;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    public void Initialize(GameObject playerInstance)
    {
        player = playerInstance;
    }

    public void Update()
    {
        if (InputManager.instance.PausePressed()) TogglePause();
    }

    public void TogglePause()
    {
        isPaused = !isPaused;

        if (isPaused)
        {
            StoreVelocity();
            Time.timeScale = 0f;
            ShowPauseMenu();
        }
        else
        {
            Time.timeScale = 1f;
            RestoreVelocity();
            HidePauseMenu();
        }

        Debug.Log("Pause toggled. isPaused: " + isPaused);
    }

    private void StoreVelocity()
    {
        if (player == null) return;
        var rb = player.GetComponent<Rigidbody2D>();
        storedVelocity = rb.linearVelocity;
        rb.linearVelocity = Vector2.zero;
    }

    private void RestoreVelocity()
    {
        if (player == null) return;
        var rb = player.GetComponent<Rigidbody2D>();
        rb.linearVelocity = storedVelocity;
    }

    private void ShowPauseMenu()
    {
        if (pauseMenuInstance == null)
        {
            pauseMenuInstance = Instantiate(pauseMenuPrefab);
            pauseMenuInstance.GetComponent<Canvas>().worldCamera = Camera.main;
        }
        pauseMenuInstance.SetActive(true);
    }

    private void HidePauseMenu()
    {
        if (pauseMenuInstance != null)
            pauseMenuInstance.SetActive(false);
    }

    public string Name { get; set; }
    public void Initialize()
    {
        gameObject.name = "PauseManager";
    }
}