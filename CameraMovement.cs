using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Jobs;

public class CameraMovement : MonoBehaviour
{
    public Transform orienatation;
    public Transform player;

    public Vector3 offset;

    public float sensitivity;

    float xRotation, yRotation;

    public void Start()
    {
        //locks cursor and makes it invisiible
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void Update()
    {
        //gets mouse inputs
        float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * sensitivity;
        float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * sensitivity;

        //puts the correct rotation to the player
        yRotation += mouseX;
        xRotation -= mouseY;

        //clamps up the rotation
        xRotation = Mathf.Clamp(xRotation, -90f, 90);

        //rotate camera
        transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);
        orienatation.rotation = Quaternion.Euler(0, yRotation, 0);
        transform.position = player.position + offset;
    }
}
