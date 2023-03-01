using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIMechanic : MonoBehaviour
{
    [SerializeField] private float speed = 2.0f;
    [SerializeField] private float hitPoints = 5.0f;
    [SerializeField] private float rightPoints = 10.0f;
    [SerializeField] private float leftPoints = -10.0f;
    public float bounceForce = 10f;
    public float hoverHeight = 2f;
    public Transform spawnPoint;
    [SerializeField] public List<GameObject> projectile;

    public Transform player;
    public float chaseDistance = 3.0f;
    public float moveSpeed = 3.0f;

    public float launchVelocity = 750f;
    public float shootInterval = 1.0f; // shoot every 2 seconds
    private float shootTimer = 0f;

    Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        shootTimer = shootInterval;
    }

    // Update is called once per frame
    void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);
        if (distanceToPlayer <= chaseDistance)
        {
            Vector3 directionToPlayer = transform.position - player.position;
            Vector3 newPosition = player.position + directionToPlayer.normalized * chaseDistance;
            transform.position = Vector3.MoveTowards(transform.position, newPosition, moveSpeed * Time.deltaTime);
        }

        if (hitPoints > 0)
        {
            float force = hoverHeight - transform.position.y;

            // Apply the upward force to the Rigidbody
            rb.AddForce(Vector3.up * force * bounceForce);

            transform.position = new Vector3(Mathf.Sin(Time.time) * speed, transform.position.y, transform.position.z);

            //transform.position += Time.deltaTime * speed * Vector3.right;
            Debug.Log("Position x:" + transform.position.x);

            if (transform.position.x >= rightPoints)
            {
                transform.position = new Vector3(-(Mathf.Sin(Time.time) * speed), transform.position.y, transform.position.z);
                Debug.Log("Position x:" + transform.position.x);
                return;
            }
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            transform.position = new Vector3(0, 0, 0);
        }

        // Update the shoot timer
        shootTimer -= Time.deltaTime;
        if (shootTimer <= 0f)
        {
            Shoot();
            shootTimer = shootInterval;
        }
    }
    void Shoot()
    {
        int myRandomIndex;
        myRandomIndex = Random.Range(0, projectile.Count);
        GameObject shooterPoint = Instantiate(projectile[myRandomIndex], spawnPoint.position, transform.rotation);

        Vector3 direction = (player.position - transform.position).normalized;
        shooterPoint.GetComponent<Rigidbody>().AddForce(direction * launchVelocity);
    }
}