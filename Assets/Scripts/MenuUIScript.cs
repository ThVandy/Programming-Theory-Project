using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuUIScript : MonoBehaviour
{
    //Methods for saving the XP with a button
    public void SaveButton()
    {
        MenuManager.Instance.SaveXP();
    }
    //Methods for loading the XP with a button
    public void LoadButton()
    {
        MenuManager.Instance.LoadXP();
    }
    //Methods for starting the game with a button
    public void StartButton()
    {
        SceneManager.LoadScene(1);
    }
}
