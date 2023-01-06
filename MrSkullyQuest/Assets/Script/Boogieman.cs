using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boogieman : MonoBehaviour
{
    public float bounceForce = 10f;
    public float hoverHeight = 2f;

    Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // Calculate the amount of upward force needed to reach the desired hover height
        float force = hoverHeight - transform.position.y;

        // Apply the upward force to the Rigidbody
        rb.AddForce(Vector3.up * force * bounceForce);
    }
}
