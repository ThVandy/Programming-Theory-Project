using UnityEngine;

public class XPTracker : MonoBehaviour
{
    //Encapsulated variables for the XP tracker
    private GameObject MenuManager;
    private MenuManager MenuManagerScript;
    public int cuttingLevel { get; private set; }
    public int cuttingXP { private get; set; }
    public int totalCuttingXP { get; private set; }
    private int baseLevelXP = 80;
    private float growthRate = 1.3f;
    public float xPToLevel { get; private set; }
    //Finds and sets the MenuManager Script
    void Start()
    {
        MenuManager = GameObject.Find("MenuManager");
        MenuManagerScript = MenuManager.GetComponent<MenuManager>();
    }

    //Updates the totalXP, XP to level and increased Level if needed when a tree is cut
    void Update()
    {
        MenuManagerScript.totalCuttingXP = MenuManagerScript.totalCuttingXP + cuttingXP;
        cuttingXP = 0;
        xPToLevel = baseLevelXP * Mathf.Pow(cuttingLevel, growthRate);
        //Checks if player has leveled up
        if (MenuManagerScript.totalCuttingXP >= xPToLevel)
        {
            cuttingLevel++;
        }
        totalCuttingXP = MenuManagerScript.totalCuttingXP;
    }
}
