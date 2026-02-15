using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class CameraController : MonoBehaviour
{
    private float cameraYInput;
    private float currentRotationX = 0f;
    public float minRotationX = -60f;
    public float maxRotationX = 60f;
    private float rotateSpeed = 90;
    private float rayDistance = 2.0f;
    private float rotationAmount;
    public UIScript uiScript;
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
        if (!uiScript.gamePause)
        {
            cameraYInput = Input.GetAxis("Mouse Y");
            rotationAmount = Input.GetAxis("Mouse Y") * rotateSpeed * Time.deltaTime;
            currentRotationX -= rotationAmount;
            currentRotationX = Mathf.Clamp(currentRotationX, minRotationX, maxRotationX);
            transform.localEulerAngles = new Vector3(currentRotationX, 270, 0);
            //transform.Rotate(Vector3.left * cameraYInput * Time.deltaTime * rotateSpeed);
        }
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
