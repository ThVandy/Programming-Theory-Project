using UnityEngine;

public class GameManager : MonoBehaviour
{
    //JSON is CASE SENSITIVE!
    public int playerId;
    public string playerUsername;
    public int playerXp;
    public string playerToken;


    public static GameManager Instance { get; private set; }

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


}
