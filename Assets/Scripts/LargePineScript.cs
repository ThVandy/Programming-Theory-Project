using UnityEngine;

public class LargePineScript : TreeScript

{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        treeDurability = 10;
        treeRespawn = 10;
        treeXP = 200;
        treeLevel = 10;
        StatusSet();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public override void TreeStatus()
    {
        statusMessage.text = "This is a Large Pine that takes " + treeDurability + " seconds to cut.";
    }
}
