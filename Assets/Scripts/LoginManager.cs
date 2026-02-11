using Mono.Cecil;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using Unity.Android.Gradle;
using UnityEditor.PackageManager;
using UnityEngine;

[System.Serializable]
public class PlayerLoginData
{
    public string username;
    public string password;
}


public class LoginManager : MonoBehaviour
{
    public GameObject usernameInputObject;
    public TMP_InputField usernameInput;
    public GameObject passwordInputObject;
    public TMP_InputField passwordInput;
    public string username;
    public string password;
    public string payload;
    public string url;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        usernameInputObject = GameObject.Find("Username");
        usernameInput = usernameInputObject.GetComponent<TMP_InputField>();
        passwordInputObject = GameObject.Find ("Password");
        passwordInput = passwordInputObject.GetComponent<TMP_InputField>();
    }

    /* Update is called once per frame
    void Update()
    {

    }
    */

    public void Login()
    {
        url = "http://api.vandy.land/api/login";
        Debug.Log("Logging in with Username: " + username + " Password: " + password);
        LoginDataToPayload();
        Debug.Log("Json payload: " + payload);
        LoginConnection();
    }

    public void LoginDataToPayload()
    {
        username = usernameInput.text;
        password = passwordInput.text;

        PlayerLoginData data = new PlayerLoginData();
        data.username = username;
        data.password = password;

        string json = JsonUtility.ToJson(data);
        Debug.Log(json);

        payload = json;
    }

    async Task LoginConnection()
    {
        using var client = new HttpClient();
        var content = new StringContent(payload, Encoding.UTF8, "application/json");
        Debug.Log($"What is sent to server: {content}");

        try
        {
            var response = await client.PostAsync(url, content);
            string body = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
                Debug.Log(body);
            else
                Debug.Log($"Error 1: {response.StatusCode}\n{body}");
        }
        catch (Exception ex)
        {
            Debug.Log($"Error 2: {ex.Message}");
        }
    }
}
