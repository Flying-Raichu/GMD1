using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    [SerializeField] private GameObject gameOverScreen;
    [SerializeField] private TMP_Text gameOverScoreText;
    [SerializeField] private LeaderboardManager leaderboardManager;

    private void Start()
    {
        gameOverScreen.SetActive(false);
    }

    public void TriggerGameOver()
    {
        gameOverScreen.SetActive(true);
        
        ScoreManager scoreManager = GameManager.instance.GetComponentInPrefab<ScoreManager>();
        int score = scoreManager.GetScore();
        
        gameOverScoreText.text = "Final Score: " + score;
        LoadLeaderboard(score);
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

    private void LoadLeaderboard(int score)
    {
        leaderboardManager.TryAddScore(score);
        leaderboardManager.UpdateLeaderboardText();
    }
}
