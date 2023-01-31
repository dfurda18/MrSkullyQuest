using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementPrototype : MonoBehaviour
{

    [Header("Movment values")]
   
    public float walkingSpeed;
    public float runningSpeed = 30;
    public float movementSpeed = 5;
    public float turnSmoothTime = 5;
    public float mouseSensitivity = 5;

    
    [Header("Jump settings")]

    Vector3 velocity;
    public LayerMask groundMask;
    public float gravity = -9.8f;
    public float groundDist;
    public bool isSprinting;
    bool isGrounded;
    public float jumpHeight = 7;
    public Transform playerCamera;
    public Transform groundCheck;
    KeyboardControls keyBoardControls;


    // Start is called before the first frame update
    void Start()
    {
        keyBoardControls= GetComponent<KeyboardControls>();
        walkingSpeed = keyBoardControls.forwardSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
    }

    void Movement()
    {
        isSprinting = Input.GetKey(KeyCode.LeftShift);
        Vector3 moveAmount = Vector3.zero;
        movementSpeed = isSprinting ? runningSpeed : walkingSpeed;
       
        Jumping(moveAmount);
       
        velocity.y += gravity * Time.deltaTime;
        moveAmount.y = velocity.y;
        moveAmount.z = Input.GetAxis("Vertical") * movementSpeed;
        moveAmount.x = Input.GetAxis("Horizontal") * movementSpeed;
        Vector3 direction = transform.TransformDirection(moveAmount);
        transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * mouseSensitivity, 0);
      //  charController.Move(direction * Time.deltaTime);
    }

    void Jumping(Vector3 moveDirection)
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDist, groundMask);
       
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -1f;
        }

        if (Input.GetKeyDown(KeyCode.Space) && moveDirection.magnitude <= 0.05f)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);

        }
    }
}
