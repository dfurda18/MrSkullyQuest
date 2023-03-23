using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirectionEffect : MonoBehaviour
{
    public DirectionTracker directionTracker;                               // Change to private later?

    private void Start()
    {
        GameObject go = GameObject.FindGameObjectWithTag("Directions");

        directionTracker = go.GetComponent<DirectionTracker>();

        if(directionTracker != null)
        {
            Debug.Log("We have found a direction Tracker");
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        

       if  (other.CompareTag("Player"))
        {
            directionTracker.goingLeft = true;
            directionTracker.goingRight = false;
            directionTracker.goingForward = false;
        }
        
        
    }
}
