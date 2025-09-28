using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class CameraController : MonoBehaviour
{
    private float cameraYInput;
    private float rotateSpeed = 90;
    private float rayDistance = 2.0f;
    public GameObject lastHit {  get; private set; }

    //Updates the rotation and what the Camera is pointing at
    void Update()
    {
        CameraRotate();
        RaycastLastHit();
    }
    //Roates the Camera with the mouse
    private void CameraRotate()
    {
        cameraYInput = Input.GetAxis("Mouse Y");
        transform.Rotate(Vector3.left * cameraYInput * Time.deltaTime * rotateSpeed);
    }
    //Method for setting the last thing the ray cast hit within the allowed distance
    private void RaycastLastHit()
    {
        //Creates a new ray forward
        var ray = new Ray(transform.position, transform.forward);
        //Gets the data from what object was hit
        RaycastHit hit;
        //Checks if ray has an Object within distance
        if (Physics.Raycast(ray, out hit, rayDistance))
        {
           //Sets the last hit Object from Hit 
           lastHit = hit.transform.gameObject;
        }
        else
        {
            //Sets last hit to null if nothing is close enough or hit
            lastHit = null;
        }
        
    }
}
