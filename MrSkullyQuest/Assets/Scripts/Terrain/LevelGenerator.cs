using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    [Header("Floor chunk generation Settings")]
    public int positionZ = 40;                                          // Start position of first randomly generated floor chunk
    public int floorChunkNumber;                                        // Floor chunk index in the floorChunk array
    public bool creatingFloorChunk = false;
    [Tooltip("Add different floor chunks in the array below")]
    public GameObject[] floorChunksArray;                               // Array that holds all posible floor chunks

 
    // Update is called once per frame
    void Update()
    {
        if(creatingFloorChunk == false)                                 // We start generating chunks by switching creatingFloorChunks
        {                                                               // to true

            creatingFloorChunk = true;
            StartCoroutine(GenerateFloorChunk());                       // Call to coroutine that generates floro chunks after a given 
                                                                        // number of seconds

        }

    }

    IEnumerator GenerateFloorChunk()
    // This method is used to endlessly generate randomly selected floor chunks from the floorChunksArray
    {
        floorChunkNumber = Random.Range(0, floorChunksArray.Length);    // Sets the floor chunk to bve generated to a random index
       
        Instantiate(floorChunksArray[floorChunkNumber],                 // Instantiates a random floor chunk
            new Vector3(0,0,positionZ), Quaternion.identity);
       
        
       
        positionZ += 40;                                                // Moves the span poistion of the next floor chunk in the Z axis
       
        yield return new WaitForSeconds(2);                             // Holds the random generation for the given number of seconds
        creatingFloorChunk = false;

    }


}
