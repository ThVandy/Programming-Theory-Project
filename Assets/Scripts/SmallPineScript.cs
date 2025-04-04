using UnityEngine;

public class SmallPineScript : TreeScript
{

    //Sets the variables for this specific tree
    void Start()
    {
        treeDurability = 5;
        treeRespawn = 5;
        treeXP = 50;
        treeLevel = 5;
        //Get's the Status UI object
        StatusSet();
    }
    //Method for setting the tree status
    public override void TreeStatus()
    {
        //Updates the status message for the tree
        statusMessage.text = "This is a Small Pine that takes " + treeDurability + " seconds to cut.";
    }
}
