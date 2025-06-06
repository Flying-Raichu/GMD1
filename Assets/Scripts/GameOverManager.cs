using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverManager : MonoBehaviour
{
    [SerializeField] private GameObject gameOverScreen;
    [SerializeField] private TMP_Text gameOverScoreText;
    [SerializeField] private LeaderboardManager leaderboardManager;
    [SerializeField] private AudioClip btnSound;
    public static event Action OnPlayerDied;

    private void Start()
    {
        gameOverScreen.SetActive(false);
    }

    public void TriggerGameOver()
    {
        gameOverScreen.SetActive(true);
        GameObject playButton = gameOverScreen.transform.Find("NewGameButton").gameObject;
        GameManager.instance.GetComponentInPrefab<EventSystem>().SetSelectedGameObject(playButton);
        
        OnPlayerDied?.Invoke();
        
        ScoreManager scoreManager = GameManager.instance.GetComponentInPrefab<ScoreManager>();
        int score = scoreManager.GetScore();
        
        gameOverScoreText.text = "Final Score: " + score;
        LoadLeaderboard(score);
    }

    public void OnNewGameButtonPressed(Button button)
    {
        PressButton("NewGame", button);
    }

    public void OnQuitButtonPressed(Button button)
    {
        PressButton("Quit", button);
    }

    private void LoadLeaderboard(int score)
    {
        leaderboardManager.TryAddScore(score);
        leaderboardManager.UpdateLeaderboardText();
    }

    private void PressButton(String command, Button button)
    {
        GetComponent<AudioSource>().PlayOneShot(btnSound);
        StartCoroutine(WaitForSound(command, button));
    }

    IEnumerator WaitForSound(String command, Button button)
    {
        Vector3 originalScale = button.transform.localScale;
        button.transform.localScale = originalScale * 0.9f;
        yield return new WaitForSeconds(0.1f);
        button.transform.localScale = originalScale;
        yield return new WaitForSeconds(btnSound.length);

        if (command == "NewGame")
        {
            Time.timeScale = 1f;
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        else
        {
            Application.Quit();
        }
    }
}
