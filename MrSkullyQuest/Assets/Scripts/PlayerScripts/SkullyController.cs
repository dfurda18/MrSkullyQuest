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
    public float distToGround;

    private bool canDash = true;
    private bool isDashing;
    public float dashPower = 24.0f;
    public float dashTime = 0.8f;
    public float dashCool = 1f;

    private new Rigidbody rigidBody;
    private new TrailRenderer trailRenderer;
    public Vector3 direction;
    public Vector3 horizontal;

   
    

    // Start is called before the first frame update
    void Start()
    {
        rigidBody = gameObject.GetComponent<Rigidbody>();
        trailRenderer = gameObject.GetComponent<TrailRenderer>();
        direction = gameObject.transform.forward;
        horizontal = gameObject.transform.right;
        //distToGround = collider.bounds.extents.y;
        //distToGround = gameObject.GetComponent<Collider>().bounds.extents.y;
        if(MainManager.Instance != null)
        {
            MainManager.HideLoadingScreen();
        }
    }

    void FixedUpdate()
    {
        rigidBody.AddForce(direction * speed);

        if (rigidBody.velocity.magnitude > speed && !isDashing)
        {
            rigidBody.velocity = rigidBody.velocity.normalized * speed;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isDashing)
            return;

        //Ground check
        RaycastHit hit;
        //if (Physics.Raycast(transform.position, Vector3.down, out hit, raycastDistance, groundLayer))
        
        if (Physics.Raycast(transform.position, -Vector3.up, distToGround))
            isGrounded = true;
        else
            isGrounded = false;


        if (Input.GetAxis("Horizontal") > 0)
        {
            //rigidBody.AddForce(Vector3.right * speed); 
            rigidBody.AddForce(horizontal*speed);
        }
        else if (Input.GetAxis("Horizontal") < 0)
        {
            //rigidBody.AddForce(-Vector3.right * speed);
            rigidBody.AddForce(-horizontal * speed);
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
        rigidBody.velocity = direction * dashPower;
        trailRenderer.emitting = true;
        yield return new WaitForSeconds(dashTime);
        trailRenderer.emitting = false;
        //rigidBody.gravityScale = originalGravity;
        rigidBody.useGravity = true;
        rigidBody.velocity = direction * speed;
        isDashing = false;
        yield return new WaitForSeconds(dashCool);
        canDash = true;

    }

    private void OnTriggerEnter(Collider collision) //void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision.gameObject.tag);
        if (collision.gameObject.CompareTag("TurnCollider"))
        {
            Debug.Log(collision.gameObject.transform.forward);
            direction = collision.gameObject.transform.forward;
            horizontal = collision.gameObject.transform.right;
            
        }
    }
}
