using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirectionEffect : MonoBehaviour
{
    public DirectionTracker directionTracker;                               // Change to private later?
    GameObject go;

    private void Start()
    {
        go = GameObject.FindGameObjectWithTag("Directions");

        directionTracker = go.GetComponent<DirectionTracker>();

        if(directionTracker != null)
        {
            directionTracker.onStraightChunk = true;
         
        }
    }
    private void OnTriggerEnter(Collider other)
    {


        if (other.CompareTag("Player"))
        {
            if (transform.tag == "LeftDirCollider")
            {
                Debug.Log("Direction going left set");
                directionTracker.onLeftChunk = true;
                directionTracker.onRightChunk = false;
                directionTracker.onStraightChunk= false;
            }

            else if (transform.tag == "CenterDirCollider")
            {
                Debug.Log("Direction going foward set");
                directionTracker.onLeftChunk = false;
                directionTracker.onRightChunk = false;
                directionTracker.onStraightChunk = true;
            }

            else if (transform.tag == "RightDirCollider")
            {
                Debug.Log("Direction going right set");
                directionTracker.onStraightChunk= false;
                directionTracker.onRightChunk = true;
                directionTracker.onLeftChunk = false;
            }
        }


        



    }
}
