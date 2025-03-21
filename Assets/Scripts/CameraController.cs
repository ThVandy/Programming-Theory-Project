using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class CameraController : MonoBehaviour
{
    public float cameraYInput;
    public float rotateSpeed = 90;
    public GameObject lastHit;
    public bool mouseClick;
    public PlayerController playerController;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        var ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;
        if(Physics.Raycast(ray, out hit))
        {
            lastHit = hit.transform.gameObject;
        }

        CameraRotate();

        mouseClick = Input.GetMouseButtonDown(0);

        if (mouseClick)
        {
            if (GameObject.Find("Sphere"))
            {
                GameObject.Find("Sphere").gameObject.transform.Translate(playerController.transform.position + Vector3.forward);
            }
            
        ;           
        }

    }

    void CameraRotate()
    {
        cameraYInput = Input.GetAxis("Mouse Y");
        transform.Rotate(Vector3.left * cameraYInput * Time.deltaTime * rotateSpeed);
    }

}
