using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public CharacterController controller;

    [SerializeField] public float speed = 6f;
    [SerializeField] public float crouchSpeed = 3f;
    [SerializeField] public float gravity = -9.81f * 3;
    [SerializeField] public float jumpHeight = 1f;
    [SerializeField] private AnimationCurve jumpFallOff;
    [SerializeField] private float jumpMultiplier;


    public Transform groundCheck;
    public float groundDistance;
    public LayerMask groundMask;
    

    Vector3 velocity;
    bool isGrounded;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;

    }
    void OnGUI()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }
    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        playerMovement();
    }

     private void JumpInput()
    {
        if(Input.GetButtonDown("Jump") && isGrounded){
            StartCoroutine(JumpEvent());
        }
    }
    private IEnumerator JumpEvent()
    {
        float timeInAir = 0.0f;

        do
        {
            float jumpForce = jumpFallOff.Evaluate(timeInAir);
            controller.Move(Vector3.up * jumpForce * jumpMultiplier * Time.deltaTime);
            timeInAir += Time.deltaTime;
            yield return null;
        }while(!isGrounded && controller.collisionFlags != CollisionFlags.Above);
    }
    private void playerMovement()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        

        Vector3 move = transform.right * x + transform.forward * z;
        controller.Move(move * speed * Time.deltaTime);

        //CROUCHING
        if(isGrounded && Input.GetKey(KeyCode.LeftControl))
        {
            speed = crouchSpeed;
            controller.transform.localScale = new Vector3(1.0f, 0.6f, 1.0f);

        }else
        {
            controller.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
            speed = 6f;
        }

        if(isGrounded == false && Input.GetKey(KeyCode.LeftControl))
        {
            speed = 6f;
            controller.transform.localScale = new Vector3(1.0f, 0.6f, 1.0f);
        }
        if(isGrounded == false && Input.GetKey(KeyCode.LeftControl ))
        {
            speed = crouchSpeed;
            controller.transform.localScale = new Vector3(1.0f, 0.6f, 1.0f);
        }

        //WALKING
        if(isGrounded && Input.GetKey(KeyCode.LeftShift)) {
            speed = crouchSpeed;
        }

        JumpInput();
    }
}
