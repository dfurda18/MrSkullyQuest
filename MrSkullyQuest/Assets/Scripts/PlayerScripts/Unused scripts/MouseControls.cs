using System.Collections;
using System.Collections.Generic;
//using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class MouseControls : MonoBehaviour
{
    [Header("Mouse movement settings")]

    public Transform playerOrientation;

    Vector3 movementDirection;
   // Rigidbody rigidbody;
    [SerializeField] KeyboardControls keyboardControls;

    [Header("Mouse movement values")]

    public float horizontalInput;
    public float verticalInput;
    public float forceMultiplier = 15;


    private void Start()
    {
        keyboardControls = GetComponent<KeyboardControls>();
        Rigidbody rigidbody = GetComponent<Rigidbody>();
      //  rigidbody.freezeRotation = true; 
        Cursor.lockState = CursorLockMode.Locked;
    }
    // Update is called once per frame
    void Update()
    {
        PlayerMouseInputt();
        

    }

    private void FixedUpdate()
    {
        MouseMovePlayer();
    }

    private void PlayerMouseInputt()
    {
      //  horizontalInput = Input.GetAxis("Mouse X");
        verticalInput = Input.GetAxis("Mouse Y");
    }

    private void MouseMovePlayer()
    {
     //   movementDirection = playerOrientation.rotation.y * verticalInput;           // Makes it so the player alaywas move in 
                                                                                 // the direction we are facing

        GetComponent<Rigidbody>().AddForce(movementDirection.normalized * forceMultiplier
            * keyboardControls.forwardSpeed, ForceMode.Force);                   // Adds movment force to the
                                                                                 // players rigigbody    
    }
}
