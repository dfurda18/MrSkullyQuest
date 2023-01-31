using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectRotation : MonoBehaviour
{
    [Header("Object Rotation Settings")]
    [Tooltip("Object rotaion speed in m/s")]

    public float rotationSpeed = 1.0f;
 
    void Update()
    {
        transform.Rotate(0, rotationSpeed, 0, Space.World);
    }
}
