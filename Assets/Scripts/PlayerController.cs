using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //private player movement variables
    private float verticalInput;
    private float horizontalInput;
    private float mouseXInput;
    private float movementSpeed = 5;
    private float rotateSpeed = 90;

    //Updates the player's position and rotation
    void Update()
    {
        //Moves player forward/back
        verticalInput = Input.GetAxis("Vertical");
        transform.Translate(Vector3.left * verticalInput * Time.deltaTime * movementSpeed);

        //moves player side to side
        horizontalInput = Input.GetAxis("Horizontal");
        transform.Translate(Vector3.forward * horizontalInput * Time.deltaTime * movementSpeed);

        //Rotates the player around
        mouseXInput = Input.GetAxis("Mouse X");
        transform.Rotate(Vector3.up * mouseXInput * Time.deltaTime * rotateSpeed);

    }

}
