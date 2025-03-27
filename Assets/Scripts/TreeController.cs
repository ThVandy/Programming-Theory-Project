using System;
using System.Collections;
using UnityEngine;

public class TreeController : MonoBehaviour
{
    public CameraController CameraController;
    public bool mouseClick;
    public bool treeClicked;
    public float treeDurability;
    public float treeDelay;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        treeDelay = treeDurability;
    }

    // Update is called once per frame
    void Update()
    {
        mouseClick = Input.GetMouseButton(0);
        TreeCheck();
        StartCoroutine(CutTree());
    }

    private void TreeCheck()
    {
        if (mouseClick && CameraController.lastHit != null && CameraController.lastHit.CompareTag("Tree"))
        {
            treeClicked = true;
        }
        else
        {
            treeClicked = false;
        }
    }

    private IEnumerator CutTree()
    {
        while (treeClicked)
        {
            if (treeDelay > 0)
            {
                treeDelay = treeDelay - Time.deltaTime;
            }
            else if (treeDelay <= 0)
            {
                Debug.Log("Tree Is Destroyed");
            }
            yield break;
        }
        treeDelay = treeDurability;
        yield break;
    }
}
