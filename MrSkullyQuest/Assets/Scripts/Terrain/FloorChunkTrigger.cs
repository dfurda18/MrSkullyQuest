using System;
using Unity.Burst.Intrinsics;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using static SECTR_Member;
using UnityEngine.Playables;


public class FloorChunkTrigger : MonoBehaviour
{
    public GameObject[] spawnPoints;


    /*private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            spawnPoints = new Transform[0];
            foreach (Transform child in transform)
            {
                if (child.CompareTag("SpawnPoint"))
                {
                    ArrayUtility.Add(ref spawnPoints, child);
                }
            }
        }
    }*/

    /* private void OnTriggerEnter(Collider other)
     {
         if (other.CompareTag("Player"))
         {
             spawnPoints = transform.GetComponentsInChildren<Transform>();
         }
     }*/

    private void OnTriggerEnter(Collider other)

    // we first create an empty array of GameObject variables with spawnPoints = new GameObject[0].
    // Then, we iterate over all the child objects of the trigger's parent object using a foreach loop.
    // For each child object, we check if it has both the "SpawnPoint" tag and
    // is a child of the trigger's parent object using child.CompareTag("SpawnPoint") && child.parent == transform.parent.
    // If it does, we add its reference to the spawnPoints array using the ArrayUtility.Add function.

    //Note that we're using child.gameObject to get the GameObject component of the Transform object in the loop,
    //since we're storing the result in an array of GameObject variables.
    //The spawnPoints array will only contain child objects with the "SpawnPoint" tag and are children of
    //the trigger's parent object, and other child objects will be excluded.
    {
        if (other.CompareTag("Player"))
        {
        spawnPoints = new GameObject[0];
        foreach (Transform child in transform.parent)
        {
            if (child.CompareTag("SpawnPoint"))
            {
                ArrayUtility.Add(ref spawnPoints, child.gameObject);
            }
        }
        }
    }
}






