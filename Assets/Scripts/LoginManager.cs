///
///
///
///You need to have Mono.Data.Sqlite and sqlite3.dll in your unity project folder for this to work correctly.
///
///
///
using TMPro;
using UnityEngine;
using UnityEngine.UI;
//using System.Data.SQLite; no longer using
using Mono.Data.Sqlite;

using System.Data;


public class LoginManager : MonoBehaviour
{
    private TMP_InputField usernameField;
    private TMP_InputField passwordField;
    private Button loginButton;
    private Button createAccount;
    private string dbPath = "test/testusers.db";
    //private string dbName = "URI=file:Assets/TestUsers.db";
    string usernameDBString;
    string passwordDBString;
    bool accountFound;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        usernameField = GameObject.Find("UsernameInput").GetComponent<TMP_InputField>();
        passwordField = GameObject.Find("PasswordInput").GetComponent<TMP_InputField>();
        loginButton = GameObject.Find("LoginButton").GetComponent<Button>();
        createAccount = GameObject.Find("CreateAccount").GetComponent<Button>();
        loginButton.onClick.AddListener(OnLogin);
        createAccount.onClick.AddListener(CreateAccount);
        CreateDB();

    }

    void CreateDB()
    {

        using (var connection = new SqliteConnection(dbPath))
        {
            connection.Open();
            using (var command = connection.CreateCommand())
            {
                command.CommandText = "CREATE TABLE IF NOT EXISTS testusers (id INTEGER, username TEXT UNIQUE, password_hash TEXT, PRIMARY KEY(\"id\" AUTOINCREMENT));";
                command.ExecuteNonQuery();
            }
            connection.Close();
        }
    }
    void OnLogin()
    {
        Debug.Log("Attempting to login");
        AccountLookup();
        if (usernameField.text == usernameDBString && passwordField.text == passwordDBString)
        {
            Debug.Log("Username: " + usernameField.text + " with Password: " + passwordField.text + " Is Logged in!");
        }
        else
        {
            Debug.Log("Incorrect Credentials.");
        }
    }


    void CreateAccount()
    {

        AccountLookup();
        if (!accountFound)
        {

            using (var connection = new SqliteConnection(dbPath))
            {
                connection.Open();
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = $"INSERT INTO testusers (username, password_hash) VALUES ('{usernameField.text}', '{passwordField.text}');";
                    command.ExecuteNonQuery();
                    Debug.Log("Account Created");
                }
                connection.Close();
            }

        }
    }

    void AccountLookup()
    {

        //Pull data via user name.
        using (var connection = new SqliteConnection(dbPath))
        {
            connection.Open();
            Debug.Log("DB Commected");
            using (var command = connection.CreateCommand())
            {
                Debug.Log("Attempting to find: " + usernameField.text);
                command.CommandText = "SELECT * FROM testusers WHERE username = '" + usernameField.text + "';";
                IDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    Debug.Log("Username Found");
                    accountFound = true;
                    var value0 = reader.GetValue(0);
                    var string1 = reader.GetString(1);
                    var string2 = reader.GetString(2);
                    usernameDBString = string1;
                    passwordDBString = string2;

                }
                else
                {
                    Debug.Log("Username Not Found(Create Account)");
                    accountFound = false; 
                }
            }
            connection.Close();
        }

    }
}