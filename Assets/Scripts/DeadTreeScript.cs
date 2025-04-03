using UnityEngine;

public class DeadTreeScript : TreeScript
{
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        treeDurability = 3;
        treeRespawn = 3;
        treeXP = 10;
        treeLevel = 1;
        StatusSet();
    }

    // Update is called once per frame
    void Update()
    {

    }
    public override void TreeStatus()
    {
        statusMessage.text = "This is a Dead Tree that takes " + treeDurability + " seconds to cut.";
    }
}
