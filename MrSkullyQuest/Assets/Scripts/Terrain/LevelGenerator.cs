using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEditor.Rendering.LookDev;
using UnityEngine;
using Random = UnityEngine.Random;

public class LevelGenerator : MonoBehaviour
{
    [Header("Floor chunk generation Settings")]
    public int positionZ = 280;                                                  // Start position of first randomly generated floor chunk
    public int positionX = 0;
    public int positionY = 0;
    public byte direction = 2;
    public int floorChunkNumber;                                                // Floor chunk index in the floorChunk array
    public int numberOfSkips = 0;
    public bool creatingFloorChunk = false;
    public Quaternion myQuaternion;
    public Vector3 positionVector;
    int newIndex = 0;
    // private bool sideChunkCreated = false;

    [Tooltip("Add different floor chunks in the array below")]
    public GameObject[] floorChunksArray;                                       // Array that holds all posible floor chunks


    // Random floor chunk selection
    private GameObject spawnPoint;
    private int lastIndex = -1;                                                 // Initialize to an index that's not in the array

    private void Start()
    {
        spawnPoint = GameObject.FindGameObjectWithTag("ChunkSpawn");
        positionVector = new Vector3(positionX,positionY, positionZ);
    }


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

        #region OldCode
        // floorChunkNumber = Random.Range(0, floorChunksArray.Length);            // Generates a random index
        // numberOfSkips++;                                                        // Increases the number of skips

        /* if (numberOfSkips < 7)                                                      
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
         }*/
        #endregion

        floorChunkNumber = ChooseRandomIndex();
        #region SwitchCode
        /* switch (floorChunkNumber)
         {

             case 0:
                 positionX = 0;
                 positionY = 0;
                 positionZ += 280;
                 positionVector = new Vector3(positionX,positionY,positionZ);
                 myQuaternion = Quaternion.identity;
                 break;

             case 1:
                 positionX -= 280;
                 positionY = 0;
                 positionZ = 0;
                 positionVector = new(positionX, positionY, positionZ);
                 myQuaternion = Quaternion.Euler(0, -90, 0);
                 break;

                 case 2:
                 positionX += 280;
                 positionY = 0;
                 positionZ = 0;
                 positionVector = new Vector3(positionX, positionY, positionZ); ;
                 myQuaternion = Quaternion.Euler(0, 90, 0); ;
                 break;

         }*/
        #endregion

        Instantiate(floorChunksArray[floorChunkNumber],                         // Instantiates a random floor chunk
            new Vector3(positionX,positionY,positionZ), Quaternion.identity);

        

       positionZ += 280;                                                        // Moves the span poistion of the next floor chunk in the Z axis
       
        yield return new WaitForSeconds(1);                                     // Holds the random generation for the given number of seconds

        creatingFloorChunk = false;
    }

    private int ChooseRandomIndex()                                             // This method chooses a random index with out repeating it twice
    {                                                                           // in  a row unless its index 0 ( straight chunk)
        newIndex = Random.Range(0, floorChunksArray.Length);
        
        while (newIndex == lastIndex && newIndex != 0)
        {
            newIndex = Random.Range(0, floorChunksArray.Length);
        }
       
        lastIndex = newIndex;

        floorChunkNumber = newIndex;

        Debug.Log("Random floor chunk number: " + floorChunkNumber);

        return floorChunkNumber;
    }
}

