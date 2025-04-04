using System;
using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class TreeController : MonoBehaviour
{
    //Private variables for the Tree Contoller logic
    private GameObject MainCamera;
    private CameraController CameraController;
    private bool mouseClick;
    private bool treeClicked;
    private bool delayReset;
    private float treeDurability;
    private float treeDelay;
    private GameObject activeTree;
    private float treeRespawn;
    private int treeXP;
    private int treeLevel;
    private XPTracker XPTracker;
    private GameObject status;
    private TextMeshProUGUI statusMessage;

    //Finds and sets the Camera Object script, the XP tracker script, and the Status UI message
    void Start()
    {
       
        MainCamera = GameObject.Find("Main Camera");
        CameraController = MainCamera.GetComponent<CameraController>();
        XPTracker = GetComponent<XPTracker>();
        status = GameObject.Find("Status");
        statusMessage = status.GetComponent<TextMeshProUGUI>();
        statusMessage.text = "Hold Click on a Tree!";
    }

    //Checks for input and runs tree cutting logic
    void Update()
    {
        
        mouseClick = Input.GetMouseButton(0);
        TreeCheck();
        StartCoroutine(CutTree());
        
    }
    //Checks if a tree is clicked and if so updates the variables
    private void TreeCheck()
    {
        //Checking if a tree is clicked
        if (mouseClick && CameraController.lastHit != null && CameraController.lastHit.CompareTag("Tree"))
        {
            treeClicked = true;
            //setting the active tree from the camera
            activeTree = CameraController.lastHit;
            //Updating the tree's variables
            activeTree.GetComponent<TreeScript>().TreeStatus();
            treeRespawn = activeTree.GetComponent<TreeScript>().treeRespawn;
            treeDurability = activeTree.GetComponent<TreeScript>().treeDurability;
            treeXP = activeTree.GetComponent<TreeScript>().treeXP;
            treeLevel = activeTree.GetComponent<TreeScript>().treeLevel;
        }
        else
        {
            //Marks if tree is not clicked
            treeClicked = false;

        }
    }
    //Method for cutting down a tree
    private IEnumerator CutTree()
    {
        //Checks to see if the tree delay has been reset
        if (!delayReset)
        {
            //sets the delay to the durability
            treeDelay = treeDurability;
            delayReset = true;
        }
        //While the mouse is held down continues to cop the tree
        while (treeClicked)
        {
            //Checks the delay matches the durability before starting the chop
            if (delayReset)
            {
                //Checks the player is the Level needed to cut the tree
                if (treeLevel <= XPTracker.cuttingLevel)
                {
                    //If the delay hasn't been reached continues to remove time from the delay
                    if (treeDelay > 0)
                    {
                        treeDelay = treeDelay - Time.deltaTime;
                    }
                    //Once tree delay is gone it continues cutting the tree
                    else if (treeDelay <= 0)
                    {
                        //despawn and respawn the cut tree
                        StartCoroutine(TreeSpawning(activeTree));
                        //Sets the XP for the XP tracker
                        XPTracker.cuttingXP = treeXP;
                        delayReset = false;
                        mouseClick = false;
                    }
                }
                else
                {
                    //Message to inform the player can't cut a tree
                    statusMessage.text = "Not high enough level to cut this tree";
                }
            }
            yield break;
        }
        //Resets delay is mouse isn't held down
        delayReset = false;
    }
    //Method for removing the tree game object and bring it back after the respawn timer
    private IEnumerator TreeSpawning(GameObject removedTree)
    {
        removedTree.SetActive(false);
        yield return new WaitForSeconds(treeRespawn);
        removedTree.SetActive(true);
    }
}
