using UnityEngine;
using TMPro;
using System;

public class LoginManager : MonoBehaviour
{
    public GameObject usernameInputObject;
    public TextMeshProUGUI usernameInput;
    public GameObject passwordInputObject;
    public string username;
    public string password;
   
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        usernameInputObject = GameObject.Find("Username");
        usernameInput = usernameInputObject.GetComponent<TextMeshProUGUI>();

    }

    // Update is called once per frame
    void Update()
    {
        username = usernameInput.text;
      
            
    }
}
