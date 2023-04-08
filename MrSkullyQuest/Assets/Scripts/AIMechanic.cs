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
    public float directionChangeSpeed = 3f;

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

    void MaintainDistance()
    {
        Vector3 targetPosition = player.position + Vector3.forward * distanceFromPlayer;
        targetPosition.y = transform.position.y;
        transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * speed);
    }

    void MoveSideToSide()
    {
        float horizontalMovement = Mathf.Sin(Time.time * speed * currentHorizontalDirection) * sidewaysAmplitude;
        float verticalMovement = Mathf.Abs(Mathf.Sin(Time.time * speed)) * player.localScale.y * 0.5f;
        Vector3 newPosition = new Vector3(player.position.x + horizontalMovement, player.position.y + verticalMovement, transform.position.z);

        // Check for collision
        RaycastHit hit;
        Vector3 direction = newPosition - transform.position;
        if (Physics.Raycast(transform.position, direction, out hit, raycastDistance, obstacleLayers))
        {
            // If an obstacle is detected, change direction and wait for the next frame
            currentHorizontalDirection = -Mathf.Sign(currentHorizontalDirection);
            return;
        }

        // Update the position
        transform.position = newPosition;
        transform.LookAt(player);
    }

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