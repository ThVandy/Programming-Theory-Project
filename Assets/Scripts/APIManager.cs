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
    public string error;
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

public class APIManager : MonoBehaviour
{
    public MenuUIScript menuUIScript;
    public TMP_InputField usernameInput;
    public TMP_InputField passwordInput;
    public string payload;
    public string url;
    public string serverResponse;
    public bool serverSuccess;
    public string serverMessage;
    public string serverError;

    public async Task Login()
    {
        url = "http://api.vandy.land/api/login";
        Debug.Log($"Logging in with Username: {menuUIScript.usernameInput.text} Password: {menuUIScript.passwordInput.text}");
        InputDataToPayload();
        Debug.Log($"Json payload:{payload}");
        await APIPostConnection();
        Debug.Log($"The server said: {serverResponse}");
        ServerResponseToPlayerData();  
    }
    public async Task Register()
    {
        url = "http://api.vandy.land/api/register";
        Debug.Log($"Registering in with Username: {menuUIScript.usernameInput.text}, Password: {menuUIScript.passwordInput.text}");
        InputDataToPayload();
        Debug.Log($"Json payload:{payload}");
        await APIPostConnection();
        ServerResponseToPlayerData();
    }

    public async Task Save()
    {
        if (GameManager.Instance.playerId != 0)
        {
            url = $"http://api.vandy.land/api/users/{GameManager.Instance.playerId}";
            Debug.Log(url);
            Debug.Log($"Saving {GameManager.Instance.playerXp} Xp of player {GameManager.Instance.playerId}");
            SaveDataToPayload();
            Debug.Log($"Json payload:{payload}");
            await APIPutConnection();
            ServerResponseSuccessCheck();
        }
    }

    public void InputDataToPayload()
    {
        InputData data = new InputData();
        data.username = menuUIScript.usernameInput.text;
        data.password = menuUIScript.passwordInput.text;

        string json = JsonUtility.ToJson(data);
        Debug.Log(json);

        payload = json;
    }

    public void SaveDataToPayload()
    {
        SaveData data = new SaveData();
        data.xp = GameManager.Instance.playerXp;

        string json = JsonUtility.ToJson(data);
        Debug.Log(json);

        payload = json;
    }

    public void ServerResponseToPlayerData()
    {
        ServerData data = JsonUtility.FromJson<ServerData>(serverResponse);
        Debug.Log($"Server success: {data.success}");
        serverSuccess = data.success;
        if (serverSuccess == true)
        {
            Debug.Log($"Server message data: {data.message}");
            serverMessage = data.message;
            Debug.Log($"Player token: {data.token}");
            GameManager.Instance.playerToken = data.token;
            Debug.Log($"Player Id: {data.data.id}");
            GameManager.Instance.playerId = data.data.id;
            Debug.Log($"Player Username: {data.data.username}");
            GameManager.Instance.playerUsername = data.data.username;
            Debug.Log($"Player Xp: {data.data.xp}");
            GameManager.Instance.playerXp = data.data.xp;
        }
        else
        {
            Debug.Log($"Server error: {data.error}");
            serverError = data.error;
        }
        
    }

    public void ServerResponseSuccessCheck()
    {
        ServerData data = JsonUtility.FromJson<ServerData>(serverResponse);
        Debug.Log($"Server success: {data.success}");
        serverSuccess = data.success;
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
            {
                Debug.Log($"Error 1: {response.StatusCode}\n{body}");
                serverResponse = body;
            }
        }
        catch (Exception ex)
        {
            Debug.Log($"Error 2: {ex.Message}");
        }
    }

    async Task APIPutConnection()
    {

        if (GameManager.Instance.playerToken != null)
        {
            using var client = new HttpClient();
            Debug.Log($"{payload} being sent");
            var content = new StringContent(payload, Encoding.UTF8, "application/json");
            Debug.Log($"What is sent to server: {content} and waiting response");
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", $"{GameManager.Instance.playerToken}");

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