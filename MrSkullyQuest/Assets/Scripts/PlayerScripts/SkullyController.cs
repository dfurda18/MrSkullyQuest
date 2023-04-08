using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkullyController : MonoBehaviour
{
    [Header("Skullys Data")]
    public bool isAlive = true;
    [Range(0, 150)]
    public int percentageLife = 100;
    [Range(0, 50)]
    public int dashCounter = 10;
    public bool canJump = true;
    public bool canDoubleJump = false;


    [Header("Movement")]
    public float speed = 12f;

    [Header("Jumping")]
    public float jumpForce = 7f;
    public LayerMask groundLayer;


    [Header("Ground Check")]
    public float raycastDistance = 0.6f;
    private bool isGrounded;
    public float distToGround;

    [Header("Keybinds")]
    public KeyCode jumpKey = KeyCode.Space;
    public KeyCode reviveKey = KeyCode.R;
    public KeyCode dashKey = KeyCode.F;
    public KeyCode pauseWorld = KeyCode.P;


    [Header("Dashing")]
    private bool canDash = true;
    private bool isDashing;
    public float dashPower = 24.0f;
    public float dashTime = 0.8f;
    public float dashCool = 1f;


    //
    private Rigidbody rigidBody;
    private TrailRenderer trailRenderer;
    public Vector3 direction;
    public Vector3 horizontal;

    public bool freeze;
    public bool activeGrapple;

    [Header("Cursor Stuff")]
    public Texture2D cursorTexture;
    public CursorMode cursorMode = CursorMode.Auto;
    public Vector2 hotSpot = Vector2.zero;
    public MovementState state;

    [Header("Screens")]
    public Pause_Screen pause_Screen;
    public Level_Completed level_Completed;
    private bool isPaused = false;

    [Header("Screen Elements")]
    public BoneCount boneCount;
    public enum MovementState
    {
        freeze,
        grappling,
        swinging,
        walking,
        sprinting,
        crouching,
        air
    }



    // Start is called before the first frame update
    void Start()
    {
       Cursor.SetCursor(cursorTexture,hotSpot,cursorMode);

       Cursor.visible =  true;
        
        rigidBody = gameObject.GetComponent<Rigidbody>();
        trailRenderer = gameObject.GetComponent<TrailRenderer>();
        direction = gameObject.transform.forward;
        horizontal = gameObject.transform.right;

        //screen = gameObject.GetComponent<Pause_Screen>();
        //readyToJump = true;
        isDashing = false;
        // This comes from the scene manager
        if (MainManager.Instance != null)
        {
            MainManager.HideLoadingScreen();
        }
    }

    void FixedUpdate()
    {
        if (isAlive) 
        { 
            rigidBody.AddForce(direction * speed);

            if (rigidBody.velocity.magnitude > speed && !isDashing)
            {
                rigidBody.velocity = rigidBody.velocity.normalized * speed;
            }
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isDashing)
            return;

        //Ground check        
        if (Physics.Raycast(transform.position, -Vector3.up, distToGround))
            isGrounded = true;
        else
            isGrounded = false;

        if(freeze) 
        {
            rigidBody.velocity = Vector3.zero;
        }

        inputHandler();
        if (!isAlive) isDead();

        if (boneCount.GetCount() >= 20)
        {
            level_Completed.LevelCompleted();
        }
    }

    public void isDead()
    {
        speed = 0;
        canDash = false;
        canJump = false;
    }

    public void resurrect() 
    {
        isAlive = true;
        speed = 12f;
        canDash = true;
        canJump = true;
    }


    private void inputHandler()
    {
        if (Input.GetAxis("Horizontal") > 0)
        {
            rigidBody.AddForce(horizontal * speed);
        }
        else if (Input.GetAxis("Horizontal") < 0)
        {
            rigidBody.AddForce(-horizontal * speed);
        }

        // when to jump
        if (Input.GetKey(jumpKey) && isGrounded && canJump)
        {
            rigidBody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
        // when to dash
        if (Input.GetKey(dashKey) && canDash && dashCounter>0)
        {
            StartCoroutine(Dash());
        }


        //ressurect
        if (Input.GetKey(reviveKey) && !isAlive)
        {
            //gameObject.transform.position = new Vector3(0, 0.5f, 0);
            resurrect();
        }

        //ressurect
        if (Input.GetKeyDown(pauseWorld))
        {
            if (isPaused) 
            {
                isPaused = false;
                pause_Screen.UnPause();
            }
                
            else
            { 
                pause_Screen.Pause();
                isPaused = true;
            }
               
        }



    }


    private IEnumerator Dash() 
    {
        dashCounter--;
        canDash = false;
        isDashing = true;
        //float originalGravity = rigidBody.gravityScale;
        rigidBody.useGravity = false;
        //rigidBody.gravityScale = 0f;
        rigidBody.velocity = direction * dashPower;
        trailRenderer.emitting = true;
        trailRenderer.SetPosition(1, gameObject.transform.position);
        yield return new WaitForSeconds(dashTime);
        trailRenderer.SetPosition(0
            , gameObject.transform.position);
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
        //Debug.Log(collision.gameObject.tag);
        if (collision.gameObject.CompareTag("TurnCollider"))
        {
           // Debug.Log(collision.gameObject.transform.forward);
            direction = collision.gameObject.transform.forward;
            horizontal = collision.gameObject.transform.right;
            
        }

        if (collision.gameObject.CompareTag("Item"))
        {
            // Debug.Log(collision.gameObject.transform.forward);
            boneCount.Collect();

        }

    }

    private bool enableMovementOnNextTouch;
    public void JumpToPosition(Vector3 targetPosition, float trajectoryHeight)
    {
        activeGrapple = true;

        velocityToSet = CalculateJumpVelocity(transform.position, targetPosition, trajectoryHeight);
        Invoke(nameof(SetVelocity), 0.1f);

        Invoke(nameof(ResetRestrictions), 3f);
    }

    private Vector3 velocityToSet;
    private void SetVelocity()
    {
        enableMovementOnNextTouch = true;
        rigidBody.velocity = velocityToSet;

        //cam.DoFov(grappleFov);
    }

    public Vector3 CalculateJumpVelocity(Vector3 startPoint, Vector3 endPoint, float trajectoryHeight)
    {
        float gravity = Physics.gravity.y;
        float displacementY = endPoint.y - startPoint.y;
        Vector3 displacementXZ = new Vector3(endPoint.x - startPoint.x, 0f, endPoint.z - startPoint.z);

        Vector3 velocityY = Vector3.up * Mathf.Sqrt(-2 * gravity * trajectoryHeight);
        Vector3 velocityXZ = displacementXZ / (Mathf.Sqrt(-2 * trajectoryHeight / gravity)
            + Mathf.Sqrt(2 * (displacementY - trajectoryHeight) / gravity));

        return velocityXZ + velocityY;
    }

    public void ResetRestrictions()
    {
        activeGrapple = false;
        //cam.DoFov(85f);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (enableMovementOnNextTouch)
        {
            enableMovementOnNextTouch = false;
            ResetRestrictions();

            GetComponent<Grappling>().StopGrapple();
        }
    }

    //void OnMouseEnter()
    //{
    //    Cursor.SetCursor(cursorTexture, hotSpot, cursorMode);
    //}

    //void OnMouseExit()
    //{
    //    Cursor.SetCursor(null, Vector2.zero, cursorMode);
    //}

}
