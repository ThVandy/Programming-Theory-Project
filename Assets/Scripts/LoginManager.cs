using Mono.Cecil;
using NUnit.Framework;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using Unity.Android.Gradle;
using UnityEditor.PackageManager;
using UnityEngine;

[System.Serializable]
public class InputData
{
    public string username;
    public string password;
}

[System.Serializable]
public class ServerData
{
    public bool success;
    public string message;
    public string token;
    public PlayerData data;
}
//JSON is CASE SENSITIVE!
[System.Serializable]
public class PlayerData
{
    public int id;
    public string username;
    public int xp;
}

[System.Serializable]
public class SaveData
{
    public int xp;
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
    public string serverResponse;
    public bool serverSuccess;
    public string serverMessage;
    public string playerToken;
    public int playerId;
    public string playerUsername;
    public int playerXp;

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

    public async void Login()
    {
        url = "http://api.vandy.land/api/login";
        Debug.Log($"Logging in with Username: {username} Password: {password}");
        InputDataToPayload();
        Debug.Log($"Json payload:{payload}");
        await APIPostConnection();
        Debug.Log($"The server said: {serverResponse}");
        ServerResponseToPlayerData();  
    }
    public async void Register()
    {
        url = "http://api.vandy.land/api/register";
        Debug.Log($"Registering in with Username: {username}, Password: {password}");
        InputDataToPayload();
        Debug.Log($"Json payload:{payload}");
        await APIPostConnection();
    }

    public async void Save()
    {
        if (playerId != 0)
        {
            url = $"http://api.vandy.land/api/users/{playerId}";
            Debug.Log(url);
            Debug.Log($"Saving {playerXp} Xp of player {playerId}");
            SaveDataToPayload();
            Debug.Log($"Json payload:{payload}");
            await APIPutConnection();
        }
    }

    public void InputDataToPayload()
    {
        username = usernameInput.text;
        password = passwordInput.text;

        InputData data = new InputData();
        data.username = username;
        data.password = password;

        string json = JsonUtility.ToJson(data);
        Debug.Log(json);

        payload = json;
    }

    public void SaveDataToPayload()
    {
        SaveData data = new SaveData();
        data.xp = playerXp;

        string json = JsonUtility.ToJson(data);
        Debug.Log(json);

        payload = json;
    }

    public void ServerResponseToPlayerData()
    {
        ServerData myData = JsonUtility.FromJson<ServerData>(serverResponse);
        Debug.Log($"Server success: {myData.success}");
        serverSuccess = myData.success;
        Debug.Log($"Server message data: {myData.message}");
        serverMessage = myData.message;
        Debug.Log($"Player token: {myData.token}");
        playerToken = myData.token;
        Debug.Log($"Player Id: {myData.data.id}");
        playerId = myData.data.id;
        Debug.Log($"Player Username: {myData.data.username}");
        playerUsername = myData.data.username;
        Debug.Log($"Player Xp: {myData.data.xp}");
        playerXp = myData.data.xp;
    }

    async Task APIPostConnection()
    {
        using var client = new HttpClient();
        Debug.Log($"{payload} being sent");
        var content = new StringContent(payload, Encoding.UTF8, "application/json");
        Debug.Log($"What is sent to server: {content} and waiting response");

        try
        {
            var response = await client.PostAsync(url, content);
            string body = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
            {
                Debug.Log(body);
                serverResponse = body;
            }
            else
                Debug.Log($"Error 1: {response.StatusCode}\n{body}");
        }
        catch (Exception ex)
        {
            Debug.Log($"Error 2: {ex.Message}");
        }
    }

    async Task APIPutConnection()
    {

        if (playerToken != null)
        {
            using var client = new HttpClient();
            Debug.Log($"{payload} being sent");
            var content = new StringContent(payload, Encoding.UTF8, "application/json");
            Debug.Log($"What is sent to server: {content} and waiting response");
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", $"{playerToken}");

            try
            {
                var response = await client.PutAsync(url, content);
                string body = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    Debug.Log(body);
                    serverResponse = body;
                }
                else
                    Debug.Log($"Error 1: {response.StatusCode}\n{body}");
            }
            catch (Exception ex)
            {
                Debug.Log($"Error 2: {ex.Message}");
            }
        }
    }
}