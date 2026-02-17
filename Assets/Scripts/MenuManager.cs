//using TMPro;
using System.Collections;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public APIManager apiManager;
    public MenuUIScript menuUIScript;

    
    //Method for starting the game with a button
    public void PlayButton()
    {
        SceneManager.LoadScene(1);
    }

    //Methods for loading the XP with a button
    public async void LoginButton()
    {
        await apiManager.Login();
        menuUIScript.LoggedIn();
        
    }

    public async void RegisterButton()
    {
        await apiManager.Register();
        menuUIScript.Registered();
    }

    //Methods for saving the XP with a button
    public async void SaveButton()
    {
        await apiManager.Save();
        menuUIScript.Saved();
    }
}