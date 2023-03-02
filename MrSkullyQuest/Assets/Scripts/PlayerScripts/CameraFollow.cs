using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    public GameObject target;
    private Vector3 cameraOffset;
    private Vector3 currentOffset;
    

    // Start is called before the first frame update
    void Start()
    {
        cameraOffset = target.transform.position - transform.position;
        currentOffset = cameraOffset;
    }

    // Update is called once per frame
    void Update()
    {
        //Vector3 direction = target.position - target.forward;
        //direction.y = target.position.y - transform.position.y;
        
        if(target.GetComponent<SkullyController>().direction.x < 0)
        {
            currentOffset.x = -cameraOffset.z;
            currentOffset.z = cameraOffset.x;
        } else if(target.GetComponent<SkullyController>().direction.x > 0)
        {
            currentOffset.x = cameraOffset.z;
            currentOffset.z = cameraOffset.x;
        }
        else if (target.GetComponent<SkullyController>().direction.z > 0)
        {
            currentOffset.z = cameraOffset.z;
            currentOffset.x = cameraOffset.x;
        } else
        {
            currentOffset.z = -cameraOffset.z;
            currentOffset.x = cameraOffset.x;
        }
        Debug.Log(currentOffset);
        Debug.Log(cameraOffset);
        transform.position = target.transform.position - currentOffset;
        transform.LookAt(target.transform.position);
    }
}
