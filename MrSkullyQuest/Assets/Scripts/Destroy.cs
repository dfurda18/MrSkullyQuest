using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroy : MonoBehaviour
{


    // Update is called once per frame
    void Update()
    {
        if (transform.position.y < 0.33f)
        {
            Destroy(gameObject);
        }
        
    }
}
