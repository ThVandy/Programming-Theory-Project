using UnityEngine;

public class XPTracker : MonoBehaviour
{
    //Encapsulated variables for the XP tracker
    public int level;
    public int xp;
    public int totalXp;
    private int baseLevelXp;
    private float growthRate = 1.3f;
    public float xpToLevel;
    //Finds and sets the MenuManager Script
    void Start()
    {
        baseLevelXp = 80;
    }
    //Updates the totalXP, XP to level and increased Level if needed when a tree is cut
    void Update()
    {
        {
            totalXp = totalXp + xp;
            GameManager.Instance.playerXp = GameManager.Instance.playerXp + xp;
            xp = 0;
            xpToLevel = baseLevelXp * Mathf.Pow(level, growthRate);
            if (totalXp >= xpToLevel)
                //Checks if player has leveled up
                if (GameManager.Instance.playerXp >= xpToLevel)
                {
                    level++;
                }
            totalXp = GameManager.Instance.playerXp;
        }
    }
}
