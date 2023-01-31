using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkullyController : MonoBehaviour
{

    public float speed = 5f;
    public float jumpForce = 7f;
    public float speedCap = 5f;
    public LayerMask groundLayer;
    public float raycastDistance = 0.6f;

    private new Rigidbody rigidBody;
    private bool isGrounded;


    // Start is called before the first frame update
    void Start()
    {
        rigidBody = gameObject.GetComponent<Rigidbody>();    
    }

    // Update is called once per frame
    void Update()
    {

        //Ground check
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit, raycastDistance, groundLayer))
            isGrounded = true;
        else
            isGrounded = false;


        if (Input.GetAxis("Horizontal") > 0)
        {
            rigidBody.AddForce(Vector3.right * speed);
        }
        else if (Input.GetAxis("Horizontal") < 0)
        {
            rigidBody.AddForce(-Vector3.right * speed);
        }


        //if (Input.GetAxis("Vertical") > 0)
        //{
        //    rigidBody.AddForce(Vector3.forward * speed);
        //}
        //else if (Input.GetAxis("Vertical") < 0)
        //{
        //    rigidBody.AddForce(-Vector3.forward * speed);
        //}

        rigidBody.AddForce(0,0,speed);
       // rigidBody.force

        if(rigidBody.velocity.magnitude > speedCap)
        {

            rigidBody.velocity = rigidBody.velocity.normalized * speedCap;

        }

        //Jumping
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rigidBody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }

        //Reset Pos

        if (Input.GetKey(KeyCode.R))
        {
            gameObject.transform.position = new Vector3(0, 0.5f, 0);
        }

    }
}
