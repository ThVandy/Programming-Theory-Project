using UnityEngine;
using TMPro;
using Unity.VisualScripting;
using UnityEngine.SceneManagement;
using JetBrains.Annotations;


public class UIScript : MonoBehaviour
{
    private GameObject levelText;
    private TextMeshProUGUI levelTextMessage;
    private GameObject xpToLevelText;
    private TextMeshProUGUI xpToLevelTextMessage;
    private GameObject mainManager;
    private XPTracker XpTracker;
    public bool gamePause;
    private GameObject pauseMenu;
    public GameObject controlsPopUp;

    //Finds and sets the player's level, XP to the next level, XP tracker script and pause menu game object
    void Start()
    {
        levelText = GameObject.Find("Level Text");
        levelTextMessage = levelText.GetComponent<TextMeshProUGUI>();
        xpToLevelText = GameObject.Find("XP to Level Text");
        xpToLevelTextMessage = xpToLevelText.GetComponent<TextMeshProUGUI>();
        mainManager = GameObject.Find("MainManager");
        XpTracker = mainManager.GetComponent<XPTracker>();
        pauseMenu = GameObject.Find("PauseMenu");
        pauseMenu.SetActive(false);
        if(GameManager.Instance.controlsPopUp == true)
        {
            controlsPopUp.SetActive(false);
        }
        else
        {
            controlsPopUp.SetActive(true);
        }
        if (controlsPopUp.activeSelf == true)
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
        else
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
    }

    //Updates the UI for the players level and XP to level and checks if the pause screen should be active
    void Update()
    {
        levelTextMessage.text = "Level: " + XpTracker.level;
        xpToLevelTextMessage.text = "XP to Level: " + Mathf.Ceil(XpTracker.xpToLevel - XpTracker.totalXp);
        PauseScreen();
        
    }
    public void ControlsPopUp()
    {
        controlsPopUp.SetActive(false);
        GameManager.Instance.controlsPopUp = true;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    //Method for the button to return to the menu
    public void BackToMenu()
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
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
            }
            else
            {
                pauseMenu.SetActive(false);
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;
                gamePause = false;
            }
        }
    }
}

