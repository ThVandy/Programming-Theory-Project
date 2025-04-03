using UnityEngine;

public class SmallPineScript : TreeScript
{
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        treeDurability = 5;
        treeRespawn = 5;
        treeXP = 50;
        treeLevel = 5;
        StatusSet();
    }

    // Update is called once per frame
    void Update()
    {

    }
    public override void TreeStatus()
    {
        statusMessage.text = "This is a Small Pine that takes " + treeDurability + " seconds to cut.";
    }
}
