using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorChunkRandomizer : MonoBehaviour
{
    [Header("Random Population Settings")]
    [Tooltip("Add Spawn locations and prefabs in the arrays below")]
    [SerializeField] private GameObject[] prefabsArray;                         // Array containing all prefabs to be spawned
    [SerializeField] private GameObject[] prefabSpawnLocations;                  // Array containing all spawn locations


    private void Start()
    {
        PupulateMapChunk();
    }


    private void PupulateMapChunk()
    // This method loops trough all possible spawn locations and populates them with with randomly selected prefabs

    {
        byte spawnLocationIndex;                                                

        for(spawnLocationIndex = 0; spawnLocationIndex < prefabSpawnLocations.Length; spawnLocationIndex++) // Loops trough all possible spawn locations
        {
            if (prefabSpawnLocations[spawnLocationIndex].transform.childCount > 0)                          // Check is there is already a prefab spawned
            {
                Destroy(prefabSpawnLocations[spawnLocationIndex].transform.GetChild(0).gameObject);         // Deletes the first child of that spawn location                            

            }

            byte randomArrayIndex = (byte)Random.Range(0, prefabsArray.Length);

            GameObject prefabToBeSpawned = Instantiate(prefabsArray[randomArrayIndex],                          // Sets the prefab to be spawned at the specified location
                prefabSpawnLocations[spawnLocationIndex].transform.position,

               // Quaternion.Euler(0, 35, 0));

               prefabSpawnLocations[spawnLocationIndex].transform.rotation);



            prefabToBeSpawned.transform.SetParent(prefabSpawnLocations[spawnLocationIndex].transform);      // Sets the spawn location as parent of the prefab being
                                                                                                            // spawned

            Quaternion target = Quaternion.Euler(0, -35, 0);

           // transform.rotation = Quaternion.RotateTowards(transform.rotation, target, 0);

        }



    }

}
