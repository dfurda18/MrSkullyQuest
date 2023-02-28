using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    [Header("Floor chunk generation Settings")]
    public int positionZ = 40;                                                  // Start position of first randomly generated floor chunk
    public int floorChunkNumber;                                                // Floor chunk index in the floorChunk array
    public int numberOfSkips = 0;
    public bool creatingFloorChunk = false;
    private bool sideChunkCreated = false;
    
    [Tooltip("Add different floor chunks in the array below")]
    public GameObject[] floorChunksArray;                                       // Array that holds all posible floor chunks

 
    // Update is called once per frame
    void Update()
    {
        if(creatingFloorChunk == false)                                         // We start generating chunks by switching creatingFloorChunks
        {                                                                       // to true
            creatingFloorChunk = true;
            StartCoroutine(GenerateFloorChunk());                               // Call to coroutine that generates floro chunks after a given 
                                                                                // number of seconds
        }
    }

    IEnumerator GenerateFloorChunk()
    // This method is used to endlessly generate randomly selected floor chunks from the floorChunksArray
    {
        Debug.Log("Number of skipds: " + numberOfSkips);
        floorChunkNumber = Random.Range(0, floorChunksArray.Length);            // Generates a random index
        numberOfSkips++;                                                        // Increases the number of skips

        if (numberOfSkips < 7)                                                      
        {
            floorChunkNumber = Random.Range(0, 2);                              // Generates random index between 0 and 2
        }                                                                       // if the number of skips is less than 7
                                                                                // This is done to ensure that if a side chunk
                                                                                // has spawned there will be enough room and we 
                                                                                // wont get 2 side chunks n the same side back to
                                                                                // back
        else
        {
            if (numberOfSkips >= 7)
            {    
                floorChunkNumber = Random.Range(4, floorChunksArray.Length);    // Sets the floor chunk to bve generated to a random index
                                                                                // Modified here to force a sidechunk spawn every certain
                                                                                // number of skips
                sideChunkCreated = true;
                numberOfSkips = 0;
            }
        }

        if(numberOfSkips== 5 && sideChunkCreated)
        {
            floorChunkNumber = 3 ;
            sideChunkCreated = false;
        }

        Instantiate(floorChunksArray[floorChunkNumber],                         // Instantiates a random floor chunk
            new Vector3(0,0,positionZ), Quaternion.identity);

        positionZ += 40;                                                        // Moves the span poistion of the next floor chunk in the Z axis
       
        yield return new WaitForSeconds(1);                                     // Holds the random generation for the given number of seconds

        creatingFloorChunk = false;
    }
}
