using UnityEngine;
using TMPro;

public abstract class TreeScript : MonoBehaviour
{


    public float treeDurability;
    public float treeRespawn;
    public int treeXP;
    public int treeLevel;
    public GameObject status;
    public TextMeshProUGUI statusMessage;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
     
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public abstract void TreeStatus();
   

    public void StatusSet()
    {
        status = GameObject.Find("Status");
        statusMessage = status.GetComponent<TextMeshProUGUI>();
    }
}
