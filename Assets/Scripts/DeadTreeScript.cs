using UnityEngine;

public class DeadTreeScript : TreeScript
{
    
    //Sets the variables for this specific tree
    void Start()
    {
        treeDurability = 3;
        treeRespawn = 3;
        treeXP = 10;
        treeLevel = 1;
        //Get's the Status UI object
        StatusSet();
    }
    //Method for setting the tree status
    public override void TreeStatus()
    {
        //Updates the status message for the tree
        statusMessage.text = "This is a Dead Tree that takes " + treeDurability + " seconds to cut.";
    }
}
