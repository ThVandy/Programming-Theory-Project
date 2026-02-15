using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuUIScript : MonoBehaviour
{
    public APIManager apiManager;
    public MenuManager menuManager;
    public GameObject usernameInputObject;
    public GameObject passwordInputObject;
    public TMP_InputField usernameInput;
    public TMP_InputField passwordInput;
    public GameObject playButton;
    public GameObject saveButton;
    public GameObject loginButton;
    public GameObject registerButton;
    public GameObject welcome;
    public TextMeshProUGUI welcomeMessage;
    public bool startedToggle;

    private void Start()
    {
        playButton.SetActive(false);
        saveButton.SetActive(false);
        welcome.SetActive(false);
        usernameInputObject.SetActive(true);
        passwordInputObject.SetActive(true);
        loginButton.SetActive(true);
        registerButton.SetActive(true);
        startedToggle = true;
    }

    public void Update()
    {
        if(startedToggle)
        {
            LoggedIn();
            startedToggle = false;
        }
    }

    public void LoggedIn()
    {
        if (GameManager.Instance.playerId != 0)
        {
            playButton.SetActive(true);
            saveButton.SetActive(true);
            welcomeMessage.text = $"Welcome, {GameManager.Instance.playerUsername}!";
            welcome.SetActive(true);
            usernameInputObject.SetActive(false);
            passwordInputObject.SetActive(false);
            loginButton.SetActive(false);
            registerButton.SetActive(false);
        }
        else
        {
            if (!startedToggle)
            {
                welcome.SetActive(true);
                welcomeMessage.text = apiManager.serverError;
            }
            
        }
    }
    public void Saved()
    {
        if (apiManager.serverSuccess == true)
        {
            welcomeMessage.text = $"{GameManager.Instance.playerUsername}, you have saved your game with {GameManager.Instance.playerXp} XP!";
        }
        else
        {
            welcomeMessage.text = apiManager.serverError;
        }
    }

    public void Registered()
    {
        if (apiManager.serverSuccess == true)
        {
            welcomeMessage.text = $"{GameManager.Instance.playerUsername}, you have registered. You may now log in.";
        }
        else
        {
            welcome.SetActive(true);
            welcomeMessage.text = apiManager.serverError;
        }
    }
   
}