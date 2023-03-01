using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkullyController : MonoBehaviour
{

    public float speed = 5f;
    public float jumpForce = 7f;
    public LayerMask groundLayer;
    public float raycastDistance = 0.6f;
    
    private bool isGrounded;
    private float distToGround;

    private bool canDash = true;
    private bool isDashing;
    private float dashPower = 24.0f;
    private float dashTime = 0.8f;
    private float dashCool = 1f;

    private new Rigidbody rigidBody;
    private new TrailRenderer trailRenderer;

   
    

    // Start is called before the first frame update
    void Start()
    {
        rigidBody = gameObject.GetComponent<Rigidbody>();
        trailRenderer = gameObject.GetComponent<TrailRenderer>();
        //distToGround = collider.bounds.extents.y;
        distToGround = gameObject.GetComponent<Collider>().bounds.extents.y;
    }

    // Update is called once per frame
    void Update()
    {
        if (isDashing)
            return;

        //Ground check
        RaycastHit hit;
        //if (Physics.Raycast(transform.position, Vector3.down, out hit, raycastDistance, groundLayer))
        if (Physics.Raycast(transform.position, -Vector3.up, distToGround + 0.1f))
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


        if (Input.GetAxis("Vertical") > 0)
        {
            rigidBody.AddForce(Vector3.forward * speed);
        }
        else if (Input.GetAxis("Vertical") < 0)
        {
            rigidBody.AddForce(-Vector3.forward * speed);
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

        if (Input.GetKeyDown(KeyCode.LeftShift) && canDash)
        {
            StartCoroutine(Dash());
        }



    }

    private IEnumerator Dash() 
    {
        canDash = false;
        isDashing = true;
        //float originalGravity = rigidBody.gravityScale;
        rigidBody.useGravity = false;
        //rigidBody.gravityScale = 0f;
        rigidBody.velocity = new Vector3(0f, 0f, transform.localScale.z * dashPower);
        trailRenderer.emitting = true;
        yield return new WaitForSeconds(dashTime);
        trailRenderer.emitting = false;
        //rigidBody.gravityScale = originalGravity;
        rigidBody.useGravity = true;
        isDashing = false;
        yield return new WaitForSeconds(dashCool);
        canDash = true;

    }
}
