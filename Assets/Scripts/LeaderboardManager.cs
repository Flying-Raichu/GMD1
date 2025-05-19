using System;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;

public class LeaderboardManager : MonoBehaviour
{
    private string savePath;
    private const int MaxEntries = 10;
    private List<int> leaderboard;
    [SerializeField] private TMP_Text leaderboardText;
    
    private string SavePath // lazy loading cuz Awake() isn't fast enough to come before the 
    {
        get
        {
            if (string.IsNullOrEmpty(savePath))
                savePath = Path.Combine(Application.persistentDataPath, "leaderboard.json");
            return savePath;
        }
    }

    public void TryAddScore(int score)
    {
        LoadLeaderboard();
        leaderboard.Add(score);
        leaderboard.Sort((a, b) => b.CompareTo(a));

        if (leaderboard.Count > MaxEntries)
            leaderboard.RemoveAt(leaderboard.Count - 1);

        SaveLeaderboard();
    }

    private void SaveLeaderboard()
    {
        var data = new LeaderboardData { scores = leaderboard };
        File.WriteAllText(SavePath, JsonUtility.ToJson(data));
    }

    private void LoadLeaderboard()
    {
        if (File.Exists(SavePath))
        {
            string json = File.ReadAllText(SavePath);
            var data = JsonUtility.FromJson<LeaderboardData>(json);
            leaderboard = data.scores ?? new List<int>();
        }
        else
        {
            leaderboard = new List<int>();
        }
    }
    
    public void UpdateLeaderboardText()
    {
        if (leaderboardText == null)
        {
            Debug.LogWarning("Leaderboard Text is not assigned!");
            return;
        }

        String text = "Leaderboard\n";
        for (int i = 0; i < leaderboard.Count; i++)
        {
            text += $"{i + 1}. {leaderboard[i]}\n";
        }
        
        leaderboardText.text = text;
        
    }
}

[Serializable]
public class LeaderboardData // can't deserialize to a basic list of ints, so it needs a wrapper class
{
    public List<int> scores = new List<int>();
}