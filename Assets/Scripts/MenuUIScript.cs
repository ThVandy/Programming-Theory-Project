using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuUIScript : MonoBehaviour
{
    //Methods for saving the XP with a button
    private void SaveButton()
    {
        MenuManager.Instance.SaveXP();
    }
    //Methods for loading the XP with a button
    private void LoadButton()
    {
        MenuManager.Instance.LoadXP();
    }
    //Methods for starting the game with a button
    private void StartButton()
    {
        SceneManager.LoadScene(1);
    }
}
