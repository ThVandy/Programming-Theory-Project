using UnityEngine;

public class XPTracker : MonoBehaviour
{
    public int cuttingLevel;
    public int cuttingXP;
    public int totalCuttingXP;
    public int baseLevelXP = 80;
    public float growthRate = 1.5f;
    public float xPToLevel;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
         baseLevelXP = 80;
    }

    // Update is called once per frame
    void Update()
    {
        totalCuttingXP = totalCuttingXP + cuttingXP;
        cuttingXP = 0;
        xPToLevel = baseLevelXP * Mathf.Pow(cuttingLevel, growthRate);
        if (totalCuttingXP >= xPToLevel)
        {
            cuttingLevel++;
        }
    }
}
