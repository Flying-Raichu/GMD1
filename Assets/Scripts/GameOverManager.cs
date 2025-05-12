using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    [SerializeField] private GameObject gameOverScreen;

    private void Start()
    {
        gameOverScreen.SetActive(false);
    }

    public void TriggerGameOver()
    {
        Debug.Log("Game Over triggered.");
        gameOverScreen.SetActive(true);
    }

    public void OnNewGameButtonPressed()
    {
        Debug.Log("New game button clicked");

        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void OnQuitButtonPressed()
    {
        Debug.Log("Quit button clicked (implement quit logic)");
        // TODO: Add quit to main menu or quit application here
    }
}
