using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    public Transform target;
    private Vector3 cameraOffset;

    // Start is called before the first frame update
    void Start()
    {
        cameraOffset = transform.position - target.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(target.position.x + cameraOffset.x, target.position.y + cameraOffset.y, target.position.z + cameraOffset.z);
    }
}
