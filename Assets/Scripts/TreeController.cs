using System;
using UnityEngine;

public class TreeController : MonoBehaviour
{
    public CameraController CameraController;
    public bool mouseClick;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        mouseClick = Input.GetMouseButton(0);
        

        if (mouseClick == true)
        {
            //Debug.Log("Shot Fired");
            //Debug.Log(CameraController.lastHit.ToString());

            if (CameraController.lastHit.CompareTag("Tree"))
            {
                Debug.Log("This is a tree");
            }

        
        }
    }
}
