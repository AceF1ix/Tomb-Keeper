using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{

    public float mouseSens = 150f;

    public Transform playerBody;

    private float xRotation = 0f;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSens * Time.deltaTime; // Make the rotation framrate independent
        float mouseY = Input.GetAxis("Mouse Y") * mouseSens * Time.deltaTime; // Make the rotation framrate independent

        xRotation -= mouseY; // We decrease the rotation of the CAMERA (not players body) based on Y-input
        xRotation = Mathf.Clamp(xRotation, -90f, 90f); // This is so we don't overrotate


        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f); // Quaternions are responsible for rotation in Unity. 
        playerBody.Rotate(Vector3.up * mouseX);
    }
}
