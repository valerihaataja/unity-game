using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public CharacterController controller;
    

     
    float speed;
    [SerializeField] public float runSpeed = 6f;
    [SerializeField] public float crouchSpeed = 3f;
    [SerializeField] public float gravity = -9.81f * 3;
    [SerializeField] public float jumpHeight = 1f;


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
        playerMovement();
    }

    
    
    private void playerMovement()
    {

        //MOVEMENT

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        
        Vector3 move = transform.right * x + transform.forward * z;

        controller.Move(move * speed * Time.deltaTime);


        //CROUCHING
        /*if(isGrounded && Input.GetKey(KeyCode.LeftControl))
        {
            speed = crouchSpeed;
            controller.transform.localScale = new Vector3(1.0f, 0.6f, 1.0f);
           
        }else
        {
            controller.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
            speed = runSpeed;
        }

        if(isGrounded == false && Input.GetKey(KeyCode.LeftControl))
        {
            speed = runSpeed;
            controller.transform.localScale = new Vector3(1.0f, 0.6f, 1.0f);
        }
        if(isGrounded == false && Input.GetKey(KeyCode.LeftControl ))
        {
            speed = crouchSpeed;
            controller.transform.localScale = new Vector3(1.0f, 0.6f, 1.0f);
        }*/

        //WALKING
        if(isGrounded && Input.GetKey(KeyCode.LeftShift)) {
            speed = crouchSpeed;
        }else{
            speed = runSpeed;
        }

        //JUMPING 


        if(isGrounded && velocity.y < 0){
            controller.slopeLimit = 45f;
            velocity.y = -2f;
        }
        

        if(Input.GetButtonDown("Jump") && isGrounded)
        {
            do
            {
                controller.slopeLimit = 100f;
                velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            }while(!isGrounded && controller.collisionFlags != CollisionFlags.Above);
        }
        /*if(Input.GetButtonDown("Jump") && isGrounded)
        {
            
                controller.slopeLimit = 100f;
                velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }*/

        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);
        Debug.Log(velocity.magnitude);

    }
}
