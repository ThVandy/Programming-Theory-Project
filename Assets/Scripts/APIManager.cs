//using Mono.Cecil;
//using NUnit.Framework;
using System;
using System.Collections;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using TMPro;
//using Unity.Android.Gradle;
//using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.Networking;

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
        url = "https://api.vandy.land/api/login";
        Debug.Log($"Logging in with Username: {menuUIScript.usernameInput.text} Password: {menuUIScript.passwordInput.text}");
        InputDataToPayload();
        Debug.Log($"Json payload:{payload}");
        await APIPostConnection(url, payload);
        Debug.Log($"The server said: {serverResponse}");
        ServerResponseToPlayerData();
    }
    public async Task Register()
    {
        url = "https://api.vandy.land/api/register";
        Debug.Log($"Registering in with Username: {menuUIScript.usernameInput.text}, Password: {menuUIScript.passwordInput.text}");
        InputDataToPayload();
        Debug.Log($"Json payload:{payload}");
        await APIPostConnection(url, payload);
        ServerResponseToPlayerData();
    }
    public async Task Save()
    {
        if (GameManager.Instance.playerId != 0)
        {
            url = $"https://api.vandy.land/api/users/{GameManager.Instance.playerId}";
            Debug.Log(url);
            Debug.Log($"Saving {GameManager.Instance.playerXp} Xp of player {GameManager.Instance.playerId}");
            SaveDataToPayload();
            Debug.Log($"Json payload:{payload}");
            await APIPutConnection(url, payload);
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
    public async Task APIPostConnection(string url, string payload)
    {
        Debug.Log($"{payload} being sent");

        // Create a POST request manually to ensure proper JSON handling
        using (UnityWebRequest request = new UnityWebRequest(url, "POST"))
        {
            byte[] bodyRaw = Encoding.UTF8.GetBytes(payload);
            request.uploadHandler = new UploadHandlerRaw(bodyRaw);
            request.downloadHandler = new DownloadHandlerBuffer();

            // HttpClient's StringContent equivalent
            request.SetRequestHeader("Content-Type", "application/json");

            Debug.Log("Waiting for response...");

            // Send and wait for the browser to complete the request
            await request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.Success)
            {
                string body = request.downloadHandler.text;
                Debug.Log(body);
                serverResponse = body;
            }
            else
            {
                // Captures 4xx/5xx errors and network failures
                Debug.Log($"Error: {request.responseCode}\n{request.error}");
                serverResponse = request.downloadHandler.text;
            }
        }
    }
    /*
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
        */
    public async Task APIPutConnection(string url, string payload)
    {
        if (GameManager.Instance.playerToken != null)
        {
            Debug.Log($"{payload} being sent");

            // Setup the request as a PUT
            using (UnityWebRequest request = new UnityWebRequest(url, "PUT"))
            {
                byte[] bodyRaw = Encoding.UTF8.GetBytes(payload);
                request.uploadHandler = new UploadHandlerRaw(bodyRaw);
                request.downloadHandler = new DownloadHandlerBuffer();

                // Set Headers
                request.SetRequestHeader("Content-Type", "application/json");
                request.SetRequestHeader("Authorization", "Bearer " + GameManager.Instance.playerToken);

                Debug.Log("Waiting for response...");

                await request.SendWebRequest();

                if (request.result == UnityWebRequest.Result.Success)
                {
                    string body = request.downloadHandler.text;
                    Debug.Log(body);
                    serverResponse = body;
                }
                else
                {
                    // Handles 4xx/5xx and Network errors
                    Debug.Log($"Error: {request.responseCode}\n{request.error}\n{request.downloadHandler.text}");
                    serverResponse = request.downloadHandler.text;
                }
            }
        }
    }
    /*

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
        */
}


