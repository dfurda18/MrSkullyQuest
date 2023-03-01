using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeController : MonoBehaviour
{

    public GameObject targetObject;
    public int speed = 2;

    private Vector3 startPosition;

    private void Start()
    {
        startPosition = gameObject.transform.position;
    }
    private void Update()
    {
        if (Input.GetKey(KeyCode.Space)) {
            gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, targetObject.transform.position, Time.deltaTime * speed);
        }

        if (Input.GetKey(KeyCode.R)) {
            gameObject.transform.position = startPosition;
        }
    }

}
