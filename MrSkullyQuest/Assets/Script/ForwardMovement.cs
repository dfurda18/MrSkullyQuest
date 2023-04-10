using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForwardMovement : MonoBehaviour
{
    public float speed = 200;

    // Update is called once per frame
    void Update()
    {
        transform.Translate(0, 0, 0);
    }
}
