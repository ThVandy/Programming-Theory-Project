using UnityEngine;
using TMPro;
using Unity.VisualScripting;


public class UIScript : MonoBehaviour
{
    public GameObject levelText;
    public TextMeshProUGUI levelTextMessage;
    public GameObject xPToLevelText;
    public TextMeshProUGUI xPToLevelTextMessage;
    public GameObject mainManager;
    public XPTracker XPTracker;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        levelText = GameObject.Find("Level Text");
        levelTextMessage = levelText.GetComponent<TextMeshProUGUI>();
        xPToLevelText = GameObject.Find("XP to Level Text");
        xPToLevelTextMessage = xPToLevelText.GetComponent<TextMeshProUGUI>();
        mainManager = GameObject.Find("MainManager");
        XPTracker = mainManager.GetComponent<XPTracker>();
    }

    // Update is called once per frame
    void Update()
    {
        levelTextMessage.text = "Level: " + XPTracker.cuttingLevel;
        xPToLevelTextMessage.text = "XP to Level: " + Mathf.Ceil(XPTracker.xPToLevel - XPTracker.totalCuttingXP);
    }
}
