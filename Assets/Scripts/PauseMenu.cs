using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    public Button resumeButton;
    public Button quitButton;

    private void Start()
    {
        resumeButton.onClick.AddListener(ResumeGame);
        quitButton.onClick.AddListener(QuitGame);
    }

    private void ResumeGame()
    {
        if (GameManager.instance != null)
        {
            Debug.Log("Resume button clicked!");
            PauseManager.Instance.TogglePause();
        }
    }

    private void QuitGame()
    {
        Debug.Log("Quit button clicked!");
        Application.Quit();
    }
}
