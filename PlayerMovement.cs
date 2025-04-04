using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerMovement : MonoBehaviour
{
    public float vSpeed = 0f;
    public float moveSpeed = 100;
    public float jumpSpeed = 20f;
    public float gravitySpeed;
    public float checkSphereSize;
    float gravity;

    bool isGrounded;

    float horizontalInput, verticalInput;

    Vector3 moveDirection;

    public Transform player;
    public Transform orientation;
    public CharacterController controller;

    public LayerMask ground;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics.Raycast(player.transform.position, Vector3.down, checkSphereSize, ground);

        if(isGrounded)
        {
            vSpeed = 0f; // grounded player has no v speed
            gravity = 0f;

            if (Input.GetKeyDown("space")) // unless they are jumping
            {
                vSpeed = jumpSpeed;
            }
        } else
        {
            gravity = gravitySpeed;
        }

            //gets input and moves player
            MyInput();
            MovePlayer();
    }

    private void MyInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");
    }

    private void MovePlayer()
    {
        //calculates movement direction
        vSpeed -= gravity * Time.deltaTime;
        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;

        //moves player
        controller.Move(new Vector3(0, vSpeed * Time.deltaTime, 0));
        controller.Move(moveDirection.normalized * moveSpeed * Time.deltaTime);
    }
}
