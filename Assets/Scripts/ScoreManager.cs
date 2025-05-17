using System.Collections;
using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] private TMP_Text scoreText;
    private int score;

    void Start()
    {
        StartCoroutine(IncrementScoreOverTime());
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
    
    public int GetScore()
    {
        return score;
    }
    
    void UpdateScoreText()
    {
        scoreText.text = "SCORE: " + score;
    }
}
