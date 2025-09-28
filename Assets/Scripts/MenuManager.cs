using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;
using Unity.VisualScripting;
#if UNITY_EDITOR
using UnityEditor;
#endif


public class MenuManager : MonoBehaviour
{
    //Variables for the MenuManager
    public static MenuManager Instance { get; private set; }
    //Total XP stored here
    public int totalCuttingXP;


    private void Awake()
    {
        //Checks to see if an Instance already exists
        if (Instance != null)
        {
            //Destroys the game object if already an Instance
            Destroy(gameObject);
            return;
        }

        //Sets the Game Object Instance and adds it to Don't Destroy on Load
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    //New class for save data
    [System.Serializable]
    class SaveData
    {
        public int totalCuttingXP;
    }
    //Method for saving the score to JSON
    public void SaveXP()
    {
        //Sets the XP from MenuManager to SaveData class variable
        SaveData data = new SaveData();
        data.totalCuttingXP = totalCuttingXP;

        //Stringify the data to JSON
        string json = JsonUtility.ToJson(data);

        //Writes the JSON to a file in the path
        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }
    //Method for loading the XP from JSON
    public void LoadXP()
    {
        //Finds the path to the save file
        string path = Application.persistentDataPath + "/savefile.json";
        //Checks if a save file exists
        if (File.Exists(path))
        {
            //Gets the JSON and sets it to the SaveData variable
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            //Sets the SaveData to the MenuManager variable 
            totalCuttingXP = data.totalCuttingXP;

        }
    }
}
