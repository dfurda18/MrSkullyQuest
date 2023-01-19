using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Speed Settings")]

    public float sideMovementSpeed = 4;

    public float forwardSpeed = 3;

    

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        PrototypeMovement();
    }

    public void PrototypeMovement()
    {
        


        if (Input.GetKey(KeyCode.W))
        {

            transform.Translate(Vector3.forward * forwardSpeed * Time.deltaTime, Space.World);
        }

        if (Input.GetKey(KeyCode.A))
        {
            if (this.gameObject.transform.position.x > Bounderies.limitLeft)
            {
                transform.Translate(Vector3.left * sideMovementSpeed * Time.deltaTime, Space.World);

            }
        }

        if (Input.GetKey(KeyCode.D))
        {

            if (this.gameObject.transform.position.x < Bounderies.limitRight)
            {
                transform.Translate(Vector3.right * sideMovementSpeed * Time.deltaTime, Space.World); ;

            }


        }

       // if (Input.GetKey(KeyCode.S))
      //  {

         //   transform.Translate(Vector3.forward * forwardSpeed * Time.deltaTime * -1, Space.World);
        //}
    }
}
