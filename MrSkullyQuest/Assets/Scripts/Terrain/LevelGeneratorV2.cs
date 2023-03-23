using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class LevelGeneratorV2 : MonoBehaviour
{
    public string parentTag;

    [Header("Floor chunk generation Settings")]
   
    public int floorChunkNumber;                                                // Floor chunk index in the floorChunk array
    int newIndex = 0;

    [Header("Floor chunks array")]
    [Tooltip("Add different floor chunks in the array below")]
    public GameObject[] floorChunksArray;                                       // Array that holds all posible floor chunks

    [Header("Spawn points arrayarray")]
    public GameObject[] spawnPoints;

    // Random floor chunk selection
    private GameObject spawnPoint;
    private GameObject prefabToBeSpawned;
    private int lastIndex = -1;                                                 // Initialize to an index that's not in the array

    public DirectionTracker directionTracker;

    private void Start()
    {
        GameObject go = GameObject.FindGameObjectWithTag("Directions");

        directionTracker = go.GetComponent<DirectionTracker>();

        if (directionTracker != null)
        {
            Debug.Log("We have found a direction Tracker");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (transform.parent != null)
            {
                parentTag = transform.gameObject.tag;
                Debug.Log("Current parent tag is: " + parentTag);
            }
            #region old spawnPoints code
            //spawnPoints = new GameObject[0];

            // ArrayUtility.Clear(ref spawnPoints);

            /* foreach (Transform child in transform.parent)
             {


                 if (child.CompareTag("ChunkSpawn"))
                 {
                     ArrayUtility.Add(ref this.spawnPoints, child.gameObject);
                 }
             }*/
            #endregion
            GenerateFloorChunk();

        }

        
    }




    private void GenerateFloorChunk()
    // This method is used to endlessly generate randomly selected floor chunks from the floorChunksArray
    {
        Debug.Log("GEnerating One CHUnk");

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

        prefabToBeSpawned = floorChunksArray[floorChunkNumber];

        #region SwitchCode
        switch (parentTag)
         {

             case "CenterCollider":                                                     // Change this code so that we only modify the y value 
                                                                                        // and then instantiate
                if(directionTracker.goingLeft)
                {
                    Instantiate(prefabToBeSpawned,                         // Instantiates a random floor chunk
                    spawnPoints[0].transform.position, transform.rotation );
                    Debug.Log(" We are in going left chunk 0 from center collider ");
                   
                    StartCoroutine("DeactivateFloorChunk");
                    break;
                }
                else if(directionTracker.goingRight)
                {
                    Instantiate(prefabToBeSpawned,                         // Instantiates a random floor chunk
                   spawnPoints[0].transform.position, transform.rotation * Quaternion.Euler(new Vector3(0, 90, 0)));
                    Debug.Log(" We are in going right chunk 0 ");
                   
                    StartCoroutine("DeactivateFloorChunk");
                    break;
                }
                else {
                    Instantiate(prefabToBeSpawned,                         // Instantiates a random floor chunk
                    spawnPoints[0].transform.position, Quaternion.identity);
                    Debug.Log(" We are in going forth chunk 0 ");
                    
                    StartCoroutine("DeactivateFloorChunk");
                    break;
                }
                

             case "LeftCollider":

                if(floorChunkNumber == 0)                                                   // Change this code so that we only modify the y value 
                                                                                        // and then instantiate
                {
                    Instantiate(prefabToBeSpawned,                         // Instantiates a random floor chunk
                    spawnPoints[0].transform.position, transform.rotation * Quaternion.Euler(new Vector3(0, -90, 0)));
                   
                    StartCoroutine("DeactivateFloorChunk");
                    break;
                }
                else
                {
                    if(floorChunkNumber == 1)
                    Instantiate(prefabToBeSpawned,                         // Instantiates a random floor chunk
                    spawnPoints[0].transform.position, transform.parent.rotation * Quaternion.Euler(new Vector3(0, 90, 0)));
                   
                    StartCoroutine("DeactivateFloorChunk");
                    break;
                }
             

            case "RightCollider":
                {
                    Instantiate(prefabToBeSpawned,                         // Instantiates a random floor chunk
                    spawnPoints[0].transform.position, Quaternion.Euler(Vector3.left));
                   
                    StartCoroutine("DeactivateFloorChunk");
                    break;
                }
                

         }
        #endregion
        StartCoroutine("DeactivateFloorChunk");
        ArrayUtility.Clear(ref spawnPoints);

       
 
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

    private IEnumerator DeactivateFloorChunk()
    {
        yield return new WaitForSeconds(3);
        Destroy(transform.parent.gameObject);

    }
}
