using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPersonMovement : MonoBehaviour
{
    
    public CharacterController controller;

    public float speed = 20f;
    public float gravity = -20f;
    public float jumpHeight = 3f;
    private float friction = 3f;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    Vector3 velocity;
    Vector3 movement;
    bool isGrounded;



    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = Mathf.Lerp(velocity.y, 0f, 3f * Time.deltaTime);
        }

        // Input System

        float x = Input.GetAxisRaw("Horizontal");
        float z = Input.GetAxisRaw("Vertical");

        Vector3 move = (transform.right * x + transform.forward * z).normalized; // Direction of movement relative to player face. 
        
        Vector3 slide = move * speed;

        movement = Vector3.Lerp(movement, slide, friction * Time.deltaTime);

        controller.Move(movement * Time.deltaTime);

        // Jump

        if(Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        } 

        // Gravity

        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime); // y = 1/2 * g * t^2



    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Enemy")
        {
            Destroy(gameObject);
        }

    }
}
