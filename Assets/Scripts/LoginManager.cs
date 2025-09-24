///
///
///
///You need to have Mono.Data.Sqlite and sqlite3.dll in your unity project folder for this to work correctly.
///
///
///
using System;
using System.Net.Sockets;
using TMPro;
using Unity.VisualScripting.Dependencies.Sqlite;
using UnityEngine;
using UnityEngine.UI;
using static Unity.VisualScripting.Member;
using static UnityEditor.ShaderData;
using static UnityEngine.InputManagerEntry;
using static UnityEngine.UIElements.UxmlAttributeDescription;
//using System.Data.SQLite; no longer using
using Mono.Data.Sqlite;
using UnityEditor.MemoryProfiler;
using static System.Net.Mime.MediaTypeNames;
using System.Xml;
using Unity.VisualScripting;
using JetBrains.Annotations;
using System.ComponentModel;
using System.Data;


public class LoginManager : MonoBehaviour
{
    private TMP_InputField usernameField;
    private TMP_InputField passwordField;
    private Button loginButton;
    private string dbPath = "Assets/users.db";
    //private string dbPath = "Assets/users.db";
    private string dbName = "URI=file:Assets/TestUsers.db";
    string usernameDBString;
    string passwordDBString;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        usernameField = GameObject.Find("UsernameInput").GetComponent<TMP_InputField>();
        passwordField = GameObject.Find("PasswordInput").GetComponent<TMP_InputField>();
        loginButton = GameObject.Find("LoginButton").GetComponent<Button>();
        loginButton.onClick.AddListener(OnLogin);
        CreateDB();

    }

    void CreateDB()
    {

        using (var connection = new SqliteConnection(dbName))
        {
            connection.Open();
            using (var command = connection.CreateCommand())
            {
                command.CommandText = "CREATE TABLE IF NOT EXISTS testusers (id INTEGER, username TEXT UNIQUE, password_hash TEXT);";
                command.ExecuteNonQuery();
            }
            connection.Close();
        }
    }
    void OnLogin()
    {

        Debug.Log("Attempting to login");

        //Pull data via user name.
        using (var connection = new SqliteConnection(dbName))
        {
            connection.Open();
            Debug.Log("DB Commected");
            using (var command = connection.CreateCommand())
            {
                Debug.Log("Attempting to find: " + usernameField.text);
                command.CommandText = "SELECT * FROM testusers WHERE username = '" + usernameField.text + "';";
                IDataReader reader = command.ExecuteReader();
                if(reader.Read()) 
                {
                    Debug.Log("Username Found");
                    var value0 = reader.GetValue(0);
                    var string1 = reader.GetString(1);
                    var string2 = reader.GetString(2);
                    Debug.Log(value0);
                    Debug.Log(string1);
                    usernameDBString = string1;
                    Debug.Log(string2);
                    passwordDBString = string2;  

                }
                else
                {
                    Debug.Log("Username Not Found");
                }
            }
            connection.Close();
        }

        if (usernameField.text == usernameDBString && passwordField.text == passwordDBString)
        {
            Debug.Log("Username: " + usernameField.text + " with Password: " + passwordField.text + " Is Logged in!");
        }
        else
        {
            Debug.Log("Wrong log-in!");
        }

        


    }
}