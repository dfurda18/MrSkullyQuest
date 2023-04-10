using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorChunkRandomizer : MonoBehaviour
{
    [Header("Random Population Settings")]
    [Tooltip("Add Spawn locations and prefabs in the arrays below")]
    [SerializeField] private GameObject[] smallPrefabsArray;                                                    // Array containing all small prefabs to be spawned
    [SerializeField] private GameObject[] smallPrefabSpawnLocations;                                            // Array containing all small spawn locations
    [SerializeField] private GameObject[] largePrefabsArray;                                                    // Array containing all large prefabs to be spawned
    [SerializeField] private GameObject[] largePrefabSpawnnLocations;                                           // Array containing all large spawn locations
    private byte spawnLocationIndex = 0;

    [Header("Item spawn values")]
    [Range(0,10)]
    public float maximumXOffset = 5;
    public float yOffsetProposed = 1.5f;
    public int maxAmountOfItems = 5;
    private float yOffset;

    private void Start()
    {
        PupulateFloorChunk(smallPrefabsArray, smallPrefabSpawnLocations);
        PupulateFloorChunk(largePrefabsArray, largePrefabSpawnnLocations);
    }


    private void PupulateFloorChunk(GameObject[] prefabArray, GameObject[] spawnnLocations)
    // This method loops trough all possible spawn locations and populates them with with randomly selected prefabs
    {                                                    
        for(spawnLocationIndex = 0; spawnLocationIndex < spawnnLocations.Length; spawnLocationIndex++) // Loops trough all possible spawn locations
        {
            if (spawnnLocations[spawnLocationIndex].transform.childCount > 0)                          // Check is there is already a prefab spawned
            {
                Destroy(spawnnLocations[spawnLocationIndex].transform.GetChild(0).gameObject);         // Deletes the first child of that spawn location                            

            }

            byte randomArrayIndex = (byte)Random.Range(0, prefabArray.Length);
            if (randomArrayIndex < maxAmountOfItems && prefabArray.Length > 10) {
                if (randomArrayIndex < 3) yOffset = 1f;
                else yOffset = yOffsetProposed;
                GameObject prefabToBeSpawned = Instantiate(prefabArray[randomArrayIndex],
                    new Vector3(Random.Range(-maximumXOffset, maximumXOffset), yOffset, spawnnLocations[spawnLocationIndex].transform.position.z),
                     Quaternion.Euler(0, -35, 0));
            }
            else { 

                GameObject prefabToBeSpawned = Instantiate(prefabArray[randomArrayIndex],                      // Sets the prefab to be spawned at the specified location
                    spawnnLocations[spawnLocationIndex].transform.position,
                   spawnnLocations[spawnLocationIndex].transform.rotation);

                prefabToBeSpawned.transform.SetParent(spawnnLocations[spawnLocationIndex].transform);      // Sets the spawn location as parent of the prefab being
                                                                                                           // spawned
                //prefabToBeSpawned.transform.parent.localScale = new Vector3(1,1,1);

                Quaternion target = Quaternion.Euler(0, -35, 0);

            }

            // transform.rotation = Quaternion.RotateTowards(transform.rotation, target, 0);
        }
    }

}
