using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyboardControls : MonoBehaviour
{
    // Start is called before the first frame update
    [Header("Speed Settings")]


    // TODO: Make private after final values have been agreed upon
   
    public float sideMovementSpeed = 15;                    // Side speed value
    public float forwardSpeed = 20;                         // Forward speed value



    // Update is called once per frame
    void Update()
    {
        PrototypeMovement();                                // Function call that enables the user
                                                            // to move the  player using the keyboard
    }

    public void PrototypeMovement()
    // This is a prototype player movement method that implements the old unity Input system.
    // It is used to move the player in the generated map.
    // NOTE: This method should not be considered a final version as it was made just for testing purposes
    {

        if (Input.GetKey(KeyCode.W))
        {
            
            transform.Translate(Vector3.forward * forwardSpeed * Time.deltaTime, Space.World);          // Moves the player forward if W is pressed
        }

        if (Input.GetKey(KeyCode.A))
        {
            if (this.gameObject.transform.position.x > Bounderies.limitLeft)                            // Limits how far to the left the player can move
                                                                                                        // to prevent the player from falling off the map
            {
                transform.Translate(Vector3.left * sideMovementSpeed * Time.deltaTime, Space.World);    // Strafe the plauer to the left if A is pressed  

            }
        }

        if (Input.GetKey(KeyCode.D))
        {

            if (this.gameObject.transform.position.x < Bounderies.limitRight)                            // Limits how far to the  right the player can move
                                                                                                         // to prevent the player from falling off the ma
            {
                transform.Translate(Vector3.right * sideMovementSpeed * Time.deltaTime, Space.World);    // Strafe the plauer to the right if D is pressed 

            }


        }

        // if (Input.GetKey(KeyCode.S))
        //  {

        //   transform.Translate(Vector3.forward * forwardSpeed * Time.deltaTime * -1, Space.World);
        //}
    }
}
