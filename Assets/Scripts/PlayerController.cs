using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This Will Auto Add Character Controller To Gameobject If It's Not Already Applied:
[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    // Camera:
    public Camera playerCam;

    // Camera Settings:
    public float lookSpeed = 2f;
    public float lookXLimit = 75f;

    private float rotationX = 0;
    private float rotationY = 0;

    private void Start()
    {
        // Lock And Hide Cursor:
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        // Camera Movement In Action:
        // Capture mouse movement for both axes
        float mouseX = Input.GetAxis("Mouse X") * lookSpeed;
        float mouseY = Input.GetAxis("Mouse Y") * lookSpeed;

        // Apply the rotation directly, accumulating the mouse input
        rotationY += mouseX;
        rotationX -= mouseY;

        // Clamp vertical rotation to prevent excessive up/down movement
        rotationX = Mathf.Clamp(rotationX, -lookXLimit, lookXLimit);

        // Apply direct horizontal rotation to the player body
        transform.rotation = Quaternion.Euler(0f, rotationY, 0f);

        // Apply direct vertical rotation to the camera
        playerCam.transform.localRotation = Quaternion.Euler(rotationX, 0f, 0f);
    }
}
