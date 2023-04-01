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
   
    public byte floorChunkNumber;                                                // Floor chunk index in the floorChunk array
    byte newIndex = 0;

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
    GameObject currentChunk;
    //private bool onStartChunk = true;

    private void Start()
    {
        GameObject go = GameObject.FindGameObjectWithTag("Directions");

        directionTracker = go.GetComponent<DirectionTracker>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (transform.parent != null)
            {
                parentTag = transform.gameObject.tag;
              //  Debug.Log("Current parent tag is: " + parentTag);
            }
            GenerateFloorChunk();
        }     
    }

    private void GenerateFloorChunk()
    // This method is used to endlessly generate randomly selected floor chunks from the floorChunksArray
    {
        floorChunkNumber = (byte)ChooseRandomIndex();
        prefabToBeSpawned = floorChunksArray[floorChunkNumber];
        Debug.Log("{REFAB NAME" + prefabToBeSpawned.ToString());

        #region SwitchCode
        switch (parentTag)
        {

            case "CenterCollider":
                {
     
                    Debug.Log("CENTER COLLIDER");
                    currentChunk =  Instantiate(prefabToBeSpawned,                         // Instantiates a random floor chunk
                       spawnPoints[0].transform.position,
                       Quaternion.identity);
                    StartCoroutine("DeactivateFloorChunk");
                    break;
                }

            case "LeftCollider":
                {
                    Debug.Log(" LEFT COLLIDER -90");
                    currentChunk = Instantiate(prefabToBeSpawned,                         // Instantiates a random floor chunk
                        spawnPoints[0].transform.position,
                        Quaternion.Euler(new Vector3(0, -90, 0)));

                    StartCoroutine("DeactivateFloorChunk");
                    break;
                }
               
            case "RightCollider":
                {
                    Debug.Log(" RIGHT COLLIDER +90"); 
                    currentChunk = Instantiate(prefabToBeSpawned, 
                        spawnPoints[0].transform.position,
                        Quaternion.Euler(new Vector3(0, 90, 0)));
                    StartCoroutine("DeactivateFloorChunk");
                    break;
                }

            default:
                Debug.Log("DEFAULT");
                currentChunk = Instantiate(prefabToBeSpawned,
                    spawnPoints[0].transform.position,
                    Quaternion.identity);
                StartCoroutine("DeactivateFloorChunk");
                break;
        }
        #endregion
        ArrayUtility.Clear(ref spawnPoints);
    }

    private int ChooseRandomIndex()                                             // This method chooses a random index with out repeating it twice
    {                                                                           // in  a row unless its index 0 ( straight chunk)
        newIndex = (byte)Random.Range(0, floorChunksArray.Length);

        while (newIndex == lastIndex && newIndex != 0)
        {
            newIndex = (byte)Random.Range(0, floorChunksArray.Length);
        }

        lastIndex = newIndex;
        floorChunkNumber = newIndex;
        return floorChunkNumber;
    }

    private IEnumerator DeactivateFloorChunk()
    {
        yield return new WaitForSeconds(16);
        Destroy(currentChunk);

    }
}
