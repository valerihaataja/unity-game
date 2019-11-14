using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public CharacterController controller;

    [SerializeField] public float speed = 6f;
    [SerializeField] public float gravity = -9.81f * 3;
    [SerializeField] public float jumpHeight = 2f;
    [SerializeField] public float crouchSpeed = 3f;
    

    public Transform groundCheck;
    public float groundDistance;
    public LayerMask groundMask;

    

    Vector3 velocity;
    bool isGrounded;
    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        

        if(isGrounded && velocity.y < 0){
            controller.slopeLimit = 45f;
            velocity.y = -2f;
        }

        

        if(isGrounded && Input.GetKey(KeyCode.LeftControl))
        {
            speed = crouchSpeed;
            controller.transform.localScale = new Vector3(1.0f, 0.8f, 1.0f);
            
        }else
        {
            controller.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
            speed = 6f;
        } 

        if(isGrounded == false && Input.GetKey(KeyCode.LeftControl))
        {
            speed = crouchSpeed;
            controller.transform.localScale = new Vector3(1.0f, 0.8f, 1.0f);
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;
        controller.Move(move * speed * Time.deltaTime);

        if(Input.GetButtonDown("Jump") && isGrounded)
        {
            controller.slopeLimit = 100f;
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime); 
    }   
}
