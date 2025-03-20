using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float verticalInput;
    public float horizontalInput;
    public float mouseXInput;
    public float movementSpeed = 5;
    public float rotateSpeed = 90;

    



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
     
    }

    // Update is called once per frame
    void Update()
    {
        verticalInput = Input.GetAxis("Vertical");
        transform.Translate(Vector3.left * verticalInput * Time.deltaTime * movementSpeed);

        horizontalInput = Input.GetAxis("Horizontal");
        transform.Translate(Vector3.forward * horizontalInput * Time.deltaTime * movementSpeed);

        mouseXInput = Input.GetAxis("Mouse X");
        transform.Rotate(Vector3.up * mouseXInput * Time.deltaTime * rotateSpeed);

    }

}
