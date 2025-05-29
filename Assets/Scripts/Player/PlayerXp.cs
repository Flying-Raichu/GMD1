using UnityEngine;

public class PlayerXp : MonoBehaviour
{
    [SerializeField] private int baseXP = 100;
    [SerializeField] private float scaleFactor = 15f;
    [SerializeField] private float curve = 2f;

    private int currentXP = 0;
    private int level = 1;

    public void AddXP(int amount)
    {
        currentXP += amount;
        Debug.Log($"Gained {amount} XP. Total: {currentXP}/{GetXPToNextLevel()}");

        while (currentXP >= GetXPToNextLevel())
        {
            currentXP -= GetXPToNextLevel();
            LevelUp();
        }
    }

    private int GetXPToNextLevel()
    {
        return Mathf.RoundToInt(baseXP + level * scaleFactor + level * level * curve);
    }

    private void LevelUp()
    {
        level++;
        Debug.Log($"Leveled Up! New Level: {level}");
    }
}
