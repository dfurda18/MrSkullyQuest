using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIMechanic : MonoBehaviour
{
    public Transform player;
    public GameObject[] throwableObjects;
    public float speed = 5f;
    public float distanceFromPlayer = 10f;
    public float throwInterval = 3f;
    public float sidewaysAmplitude = 2f;
    public LayerMask obstacleLayers;
    public float raycastDistance = 1f;
    public float hoverHeight = 2f;

    private float currentHorizontalDirection = 1f;
    private float nextThrowTime;

    void Start()
    {
        nextThrowTime = Time.time + throwInterval;
    }

    void Update()
    {
        MaintainDistance();
        MoveSideToSide();
        ThrowObjects();
    }

    // Maintain a constant distance from the player
    void MaintainDistance()
    {
        Vector3 targetPosition = player.position + Vector3.forward * distanceFromPlayer;
        targetPosition.y = transform.position.y;
        transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * speed);
    }

    // Move side to side and hover at the specified height
    void MoveSideToSide()
    {
        float horizontalMovement = Mathf.Sin(Time.time * speed * currentHorizontalDirection) * sidewaysAmplitude;
        float verticalMovement = Mathf.Abs(Mathf.Sin(Time.time * speed)) * 0.5f * hoverHeight;

        // Calculate the enemy's position relative to the player's local space
        Vector3 localOffset = new Vector3(horizontalMovement, verticalMovement, -distanceFromPlayer);
        Vector3 worldOffset = player.position + (player.forward * -distanceFromPlayer) + (player.right * horizontalMovement) + (player.up * verticalMovement);

        // Check for collision
        RaycastHit hit;
        Vector3 direction = worldOffset - transform.position;
        if (Physics.Raycast(transform.position, direction, out hit, raycastDistance, obstacleLayers))
        {
            // If an obstacle is detected, change direction and wait for the next frame
            currentHorizontalDirection = -Mathf.Sign(currentHorizontalDirection);
            return;
        }

        // Smoothly update the position
        transform.position = Vector3.Lerp(transform.position, worldOffset, Time.deltaTime * speed);

        // Look at the player while maintaining the enemy's own rotation in the Y-axis
        Vector3 lookAtPlayer = new Vector3(player.position.x, transform.position.y, player.position.z);
        transform.LookAt(lookAtPlayer);
    }

    // Throw random objects at the player's position
    void ThrowObjects()
    {
        if (Time.time >= nextThrowTime)
        {
            GameObject throwable = throwableObjects[Random.Range(0, throwableObjects.Length)];
            GameObject thrownObject = Instantiate(throwable, transform.position, Quaternion.identity);
            Rigidbody rb = thrownObject.AddComponent<Rigidbody>();
            rb.velocity = (player.position - transform.position).normalized * speed;

            nextThrowTime = Time.time + throwInterval;
        }
    }
}