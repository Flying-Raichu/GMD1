using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerXp : MonoBehaviour
{
    [Header("XP Settings")]
    [SerializeField] private int baseXP = 100;
    [SerializeField] private float scaleFactor = 15f;
    [SerializeField] private float curve = 2f;

    [Header("UI")]
    [SerializeField] private Image xpBarFill;       // Reference to Filled image
    [SerializeField] private TMP_Text levelText;  // Optional: "LEVEL 1"

    private int currentXP = 0;
    private int level = 1;
    private float displayedXP = 0f;

    private void Start()
    {
        if (levelText != null)
            levelText.text = $"LEVEL {level}";
    }

    void Update()
    {
        int xpToNextLevel = GetXPToNextLevel();

        displayedXP = Mathf.Lerp(displayedXP, currentXP, Time.deltaTime * 10f);
        if (xpBarFill != null)
            xpBarFill.fillAmount = displayedXP / xpToNextLevel;
    }

    public void AddXP(int amount)
    {
        currentXP += amount;
        Debug.Log($"Gained {amount} XP. Total: {currentXP}/{GetXPToNextLevel()}");

        while (currentXP >= GetXPToNextLevel())
        {
            currentXP -= GetXPToNextLevel();
            LevelUp();
        }

        UpdateLevelText();
    }

    private void LevelUp()
    {
        level++;
        Debug.Log($"Leveled Up! New Level: {level}");
    }

    private int GetXPToNextLevel()
    {
        return Mathf.RoundToInt(baseXP + level * scaleFactor + level * level * curve);
    }

    private void UpdateLevelText()
    {
        if (levelText != null)
            levelText.text = $"LEVEL {level}";
    }
}
