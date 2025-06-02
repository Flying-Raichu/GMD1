using System.Collections;
using Player;
using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] private TMP_Text scoreText;
    private int score;
    private Coroutine scoreCoroutine;

    void Start()
    {
        scoreCoroutine = StartCoroutine(IncrementScoreOverTime());
        GameOverManager.OnPlayerDied += OnPlayerDied;
    }

    private IEnumerator IncrementScoreOverTime()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);
            AddScore(10);
        }
    }

    public void AddScore(int points)
    {
        score += points;
        UpdateScoreText();
    }
    
    private void OnPlayerDied()
    {
        if (scoreCoroutine != null)
        {
            StopCoroutine(scoreCoroutine);
            scoreCoroutine = null;
        }
    }
    
    public int GetScore()
    {
        return score;
    }
    
    void UpdateScoreText()
    {
        scoreText.text = "SCORE: " + score;
    }
}
