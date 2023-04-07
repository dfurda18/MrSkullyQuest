//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class AIMechanic : MonoBehaviour
//{
//    [SerializeField] private float speed = 2.0f;
//    [SerializeField] private float hitPoints = 5.0f;
//    [SerializeField] private float rightPoints = 10.0f;
//    [SerializeField] private float leftPoints = -10.0f;
//    public float bounceForce = 10f;
//    public float hoverHeight = 2f;
//    public Transform spawnPoint;
//    [SerializeField] public List<GameObject> projectile;

//    public Transform player;
//    public float chaseDistance = 3.0f;
//    public float moveSpeed = 3.0f;

//    public float launchVelocity = 450f;
//    public float shootInterval = 1.0f; // shoot every 2 seconds
//    private float shootTimer = 0f;
//    [SerializeField] public Animator boogieAnimator;
//    Rigidbody rb;

//    private float distanceToPlayer;
//    private Vector3 directionToPlayer;
//    private Vector3 newPosition;
//    void Start()
//    {
//        rb = GetComponent<Rigidbody>();
//        shootTimer = shootInterval;
//        boogieAnimator = GetComponent<Animator>();
        
//    }

//    // Update is called once per frame
//    void Update()
//    {
//        distanceToPlayer = Vector3.Distance(transform.position, player.position);
//        Debug.Log("Distance to player: " + distanceToPlayer);
//        directionToPlayer = transform.position - player.position;
//        Debug.Log("Direction to player: " + directionToPlayer);
//        Debug.Log("Boogie position:"+transform.position+ " Player position: "+ player.position);
//        if (directionToPlayer.z < 0) directionToPlayer *= -1;
//        if (distanceToPlayer <= chaseDistance)
//        {

//            newPosition = player.position + directionToPlayer.normalized * chaseDistance;

//            transform.position = Vector3.MoveTowards(transform.position, newPosition, moveSpeed * Time.deltaTime);
//        }
//        else
//        {

//            newPosition = player.position + directionToPlayer.normalized * chaseDistance;
//            //transform.position = new Vector3(player.position.x, player.position.y, player.position.z-15);
//            transform.position = Vector3.MoveTowards(transform.position, newPosition, moveSpeed * 2 * Time.deltaTime);
//        }

//        //float distanceToPlayer = Vector3.Distance(transform.position, player.position);
//        //if (distanceToPlayer <= chaseDistance)
//        //{
//        //    // calculate direction vector from enemy to player
//        //    Vector3 directionToPlayer = player.position - transform.position;
//        //    // normalize the direction vector and multiply by desired distance
//        //    Vector3 targetPosition = player.position + directionToPlayer.normalized * chaseDistance;
//        //    // move enemy towards target position with move speed
//        //    transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
//        //}


//        //Vector3 directionToPlayer = transform.position - player.position;
//        //Vector3 newPosition = player.position + directionToPlayer.normalized * chaseDistance;
//        //transform.position = Vector3.MoveTowards(transform.position, newPosition, moveSpeed * Time.deltaTime);
//        //float distanceToPlayer = Vector3.Distance(transform.position, player.position);
//        //if (distanceToPlayer <= chaseDistance)
//        //{
//        //    if (player.GetComponent<Rigidbody>().velocity.y <= 0) // only move towards player if not jumping
//        //    {
//        //        Vector3 directionToPlayer = transform.position - player.position;
//        //        Vector3 newPosition = player.position + directionToPlayer.normalized * chaseDistance;
//        //        transform.position = Vector3.MoveTowards(transform.position, newPosition, moveSpeed * Time.deltaTime);
//        //    }
//        //}

//        if (hitPoints > 0)
//        {
//            float force = hoverHeight - transform.position.y;

//            // Apply the upward force to the Rigidbody
//            rb.AddForce(Vector3.up * force * bounceForce);

//            transform.position = new Vector3(Mathf.Sin(Time.time) * speed, transform.position.y, transform.position.z);

//            //transform.position += Time.deltaTime * speed * Vector3.right;
//            //Debug.Log("Position x:" + transform.position.x);

//            if (transform.position.x >= rightPoints)
//            {
//                transform.position = new Vector3(-(Mathf.Sin(Time.time) * speed), transform.position.y, transform.position.z);
//                //Debug.Log("Position x:" + transform.position.x);
//                return;
//            }
//        }

//        if (Input.GetKeyDown(KeyCode.Space))
//        {
//            transform.position = new Vector3(0, 0, 0);
//        }

//        // Update the shoot timer
//        shootTimer -= Time.deltaTime;
//        if (shootTimer <= 0f)
//        {
//            boogieAnimator.SetBool("IsIdlening", false);
//            boogieAnimator.SetBool("Attack", true);
//            // .SetTrigger("Throw");
//            //boogieAnimator.Play("Throw");
//            Shoot();
//            shootTimer = shootInterval;
            
//        }
//    }
//    void Shoot()
//    {
        
//        int myRandomIndex;
//        myRandomIndex = Random.Range(0, projectile.Count);
//        GameObject shooterPoint = Instantiate(projectile[myRandomIndex], spawnPoint.position, transform.rotation); 

//        Vector3 direction = (player.position - transform.position).normalized;  //  transform.position - player.position;
//        shooterPoint.GetComponent<Rigidbody>().AddForce(-direction * launchVelocity);
//        boogieAnimator.SetBool("IsIdlening", true);
//        boogieAnimator.SetBool("Attack", false);
//    }
//}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIMechanic : MonoBehaviour
{
    [SerializeField] private float hoverHeight = 2f;
    [SerializeField] private float moveSpeed = 3.0f;
    [SerializeField] private float followDistance = 5.0f;
    [SerializeField] private float projectileSpeed = 10.0f;
    [SerializeField] private float shootInterval = 1.0f; // shoot every 1 second
    [SerializeField] private List<GameObject> projectiles;
    [SerializeField] private float projectileTime = 3.0f;
    [SerializeField] private float pushForce = 0.001f;
    private Rigidbody rb;
    private Transform player;
    private float shootTimer = 0f;
    private Vector3 targetPosition;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        targetPosition = transform.position;
    }


    void FixedUpdate()
    {
        // Calculate the target position based on the player's current velocity
        Vector3 playerVelocity = player.GetComponent<Rigidbody>().velocity;
        Vector3 targetDirection = playerVelocity.normalized;
        targetPosition = player.position + targetDirection * followDistance;

        // Move towards the target position while maintaining a constant distance from the player
        Vector3 directionToTarget = targetPosition - transform.position;
        Vector3 directionToPlayer = player.position - transform.position;
        float distanceToPlayer = directionToPlayer.magnitude;
        directionToPlayer.y = 0f; // Don't move in the y-axis
        directionToTarget.y = 0f; // Don't move in the y-axis
        float distanceToTarget = directionToTarget.magnitude;
        if (distanceToPlayer > followDistance && distanceToTarget > distanceToPlayer)
        {
            // Move towards the player if they are too far away
            transform.position += directionToPlayer.normalized * moveSpeed * Time.fixedDeltaTime;
        }
        else
        {
            // Move towards the target position
            transform.position += directionToTarget.normalized * moveSpeed * Time.fixedDeltaTime;
        }

        // Add a force that pushes the enemy away from the player
        Vector3 directionToPlayer2D = player.position - transform.position;
        directionToPlayer2D.y = 0f;
        if (distanceToPlayer <= followDistance)
        {
            rb.AddForce((-directionToPlayer2D.normalized / pushForce), ForceMode.Impulse);
        }
        // Update the shoot timer
        shootTimer -= Time.fixedDeltaTime;
        if (shootTimer <= 0f)
        {
            Shoot();
            shootTimer = shootInterval;
        }

        // Hover in place
        float force = hoverHeight - transform.position.y;
        rb.AddForce(Vector3.up * force, ForceMode.Impulse);

        // Move sideways randomly
        Vector3 randomDirection = new Vector3(Random.Range(-1f, 1f), 0f, 0f);
        rb.AddForce(randomDirection * moveSpeed * Time.fixedDeltaTime, ForceMode.Impulse);

        // Aim at the player
        transform.LookAt(player);
    }

    //void Update()
    //{
    //    // Move towards the target position
    //    Vector3 directionToTarget = targetPosition - transform.position;
    //    directionToTarget.y = 0f; // Don't move in the y-axis
    //    transform.position += directionToTarget.normalized * moveSpeed * Time.deltaTime;

    //    // Update the shoot timer
    //    shootTimer -= Time.deltaTime;
    //    if (shootTimer <= 0f)
    //    {
    //        Shoot();
    //        shootTimer = shootInterval;

    //    }
    //}

    //void FixedUpdate()
    //{
    //    // Keep a constant distance from the player
    //    float distanceToPlayer = Vector3.Distance(transform.position, player.position);
    //    if (distanceToPlayer > followDistance)
    //    {
    //        Vector3 directionToPlayer = player.forward;
    //        targetPosition = player.position + directionToPlayer * followDistance;
    //    }

    //    // Hover in place
    //    float force = hoverHeight - transform.position.y;
    //    rb.AddForce(Vector3.up * force, ForceMode.Impulse);

    //    // Move sideways randomly
    //    Vector3 randomDirection = new Vector3(Random.Range(-0.1f, 0.1f), 0f, 0f);
    //    rb.AddForce(randomDirection * moveSpeed * Time.fixedDeltaTime, ForceMode.Impulse);

    //    // Aim at the player
    //    Vector3 directionToPlayer2D = player.position - transform.position;
    //    directionToPlayer2D.y = 0f;
    //    transform.rotation = Quaternion.LookRotation(directionToPlayer2D);
    //}

    void Shoot()
    {
        if (projectiles.Count > 0)
        {
            int randomIndex = Random.Range(0, projectiles.Count);
            GameObject projectile = Instantiate(projectiles[randomIndex], transform.position, Quaternion.identity);
            Vector3 direction = (player.position - transform.position).normalized;
            projectile.GetComponent<Rigidbody>().velocity = direction * projectileSpeed;
            Destroy(projectile, projectileTime);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("TurnCollider"))
        {
            // Change direction with the player
            Vector3 directionToTurn = other.transform.position - transform.position;
            directionToTurn.y = 0f;
            targetPosition = player.position + directionToTurn.normalized * followDistance;
        }
    }
}