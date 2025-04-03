using System;
using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class TreeController : MonoBehaviour
{

    public GameObject MainCamera;
    public CameraController CameraController;
    public bool mouseClick;
    public bool treeClicked;
    public bool delayReset;
    public float treeDurability;
    public float treeDelay;
    public GameObject activeTree;
    public float treeRespawn;
    public int treeXP;
    public int treeLevel;
    public XPTracker XPTracker;
    public GameObject status;
    public TextMeshProUGUI statusMessage;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
       
        MainCamera = GameObject.Find("Main Camera");
        CameraController = MainCamera.GetComponent<CameraController>();
        XPTracker = GetComponent<XPTracker>();
        status = GameObject.Find("Status");
        statusMessage = status.GetComponent<TextMeshProUGUI>();
        statusMessage.text = "Click on a Tree!";
    }

    // Update is called once per frame
    void Update()
    {
        
        mouseClick = Input.GetMouseButton(0);
        TreeCheck();
        StartCoroutine(CutTree());
        
    }

    public void TreeCheck()
    {
        if (mouseClick && CameraController.lastHit != null && CameraController.lastHit.CompareTag("Tree"))
        {
            treeClicked = true;
            
            activeTree = CameraController.lastHit;
            activeTree.GetComponent<TreeScript>().TreeStatus();
            treeRespawn = activeTree.GetComponent<TreeScript>().treeRespawn;
            treeDurability = activeTree.GetComponent<TreeScript>().treeDurability;
            treeXP = activeTree.GetComponent<TreeScript>().treeXP;
            treeLevel = activeTree.GetComponent<TreeScript>().treeLevel;
        }
        else
        {
            treeClicked = false;

        }
    }

    public IEnumerator CutTree()
    {
        if (!delayReset)
        {
            treeDelay = treeDurability;
            delayReset = true;
        }
        while (treeClicked)
        {
            if (delayReset)
            {
                if (treeLevel <= XPTracker.cuttingLevel)
                {
                    if (treeDelay > 0)
                    {
                        treeDelay = treeDelay - Time.deltaTime;
                    }
                    else if (treeDelay <= 0)
                    {
                        Debug.Log("You cut down a tree");
                        //despawn and respawn cut tree
                        StartCoroutine(TreeSpawning(activeTree));
                        XPTracker.cuttingXP = treeXP;
                        delayReset = false;
                        mouseClick = false;
                    }
                }
                else
                {
                    statusMessage.text = "Not high enough level to cut this tree";
                }
            }
            yield break;
        }
        delayReset = false;
    }

    public IEnumerator TreeSpawning(GameObject removedTree)
    {
        removedTree.SetActive(false);
        yield return new WaitForSeconds(treeRespawn);
        removedTree.SetActive(true);
    }
}
