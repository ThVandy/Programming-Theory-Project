using System;
using System.Collections;
using UnityEngine;

public class TreeController : MonoBehaviour
{

    public GameObject MainCamera;
    public CameraController CameraController;
    public bool mouseClick;
    public bool treeClicked;
    public float treeDurability;
    public float treeDelay;
    public GameObject activeTree;
    public float treeRespawn;
    public int treeXP;
    public int treeLevel;
    public XPTracker XPTracker;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
       
        MainCamera = GameObject.Find("Main Camera");
        CameraController = MainCamera.GetComponent<CameraController>();
        XPTracker = GetComponent<XPTracker>();
       
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
        while (treeClicked)
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
                    treeDelay = treeDurability;
                    mouseClick = false;

                }
                yield break;
            }
            else
            {
                Debug.Log("Not high enough level");
            }
            yield break;
            
        }
        treeDelay = treeDurability;
        yield break;
    }

    public IEnumerator TreeSpawning(GameObject removedTree)
    {
        removedTree.SetActive(false);
        yield return new WaitForSeconds(treeRespawn);
        removedTree.SetActive(true);
    }
}
