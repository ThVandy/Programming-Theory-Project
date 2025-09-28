using UnityEngine;
using TMPro;

public abstract class TreeScript : MonoBehaviour
{

    //Encapsulated variables
    public float treeDurability {  get; protected set; }
    public float treeRespawn { get; protected set; }
    public int treeXP { get; protected set; }
    public int treeLevel { get; protected set; }
    public GameObject status { get; protected set; }
    public TextMeshProUGUI statusMessage { get; protected set; }

    //Gets the Status UI object
    protected void StatusSet()
    {
        status = GameObject.Find("Status");
        statusMessage = status.GetComponent<TextMeshProUGUI>();
    }
    //Abstracted Method for setting the tree status
    public abstract void TreeStatus();
   
    
}
