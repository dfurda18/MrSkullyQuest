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