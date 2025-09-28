using UnityEngine;

public class LargePineScript : TreeScript

{   //Sets the variables for this specific tree
    void Start()
    {
        treeDurability = 10;
        treeRespawn = 10;
        treeXP = 200;
        treeLevel = 10;
        //Get's the Status UI object
        StatusSet();
    }
    //Method for setting the tree status
    public override void TreeStatus()
    {
        //Updates the status message for the tree
        statusMessage.text = "This is a Large Pine that takes " + treeDurability + " seconds to cut.";
    }
}
