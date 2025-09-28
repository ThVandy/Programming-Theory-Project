using UnityEngine;
using TMPro;
using Unity.VisualScripting;
using UnityEngine.SceneManagement;


public class UIScript : MonoBehaviour
{
    private GameObject levelText;
    private TextMeshProUGUI levelTextMessage;
    private GameObject xPToLevelText;
    private TextMeshProUGUI xPToLevelTextMessage;
    private GameObject mainManager;
    private XPTracker XPTracker;
    private bool gamePause;
    private GameObject pauseMenu;

    //Finds and sets the player's level, XP to the next level, XP tracker script and pause menu game object
    void Start()
    {
        levelText = GameObject.Find("Level Text");
        levelTextMessage = levelText.GetComponent<TextMeshProUGUI>();
        xPToLevelText = GameObject.Find("XP to Level Text");
        xPToLevelTextMessage = xPToLevelText.GetComponent<TextMeshProUGUI>();
        mainManager = GameObject.Find("MainManager");
        XPTracker = mainManager.GetComponent<XPTracker>();
        pauseMenu = GameObject.Find("PauseMenu");
        pauseMenu.SetActive(false);
    }

    //Updates the UI for the players level and XP to level and checks if the pause screen should be active
    void Update()
    {
        levelTextMessage.text = "Level: " + XPTracker.cuttingLevel;
        xPToLevelTextMessage.text = "XP to Level: " + Mathf.Ceil(XPTracker.xPToLevel - XPTracker.totalCuttingXP);
        PauseScreen();
    }
    //Method for the button to return to the menu
    private void BackToMenu()
    {
        SceneManager.LoadScene(0);
    }
    //Method for bringing up the pause screen
    private void PauseScreen()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            if (!gamePause)
            {
                gamePause = true;
                pauseMenu.SetActive(true);
            }
            else
            {
                gamePause = false;
                pauseMenu.SetActive(false);
            }
        }
    }
}

