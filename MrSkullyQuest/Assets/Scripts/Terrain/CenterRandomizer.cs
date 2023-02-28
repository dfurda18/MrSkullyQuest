using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CenterRandomizer : MonoBehaviour
{

    [SerializeField] private GameObject[] centerPrefabsArray;                                                    // Array containing all large prefabs to be spawned
    [SerializeField] private GameObject centerSpawn;
    
    void Start()
    {
        PupulateCenterChunk(centerPrefabsArray, centerSpawn);
    }

    private void PupulateCenterChunk(GameObject[] prefabArray, GameObject spawnnLocation)
    // This method spawns a center prefab in the middle of 
    {
       
            if (centerSpawn.transform.childCount > 0)                                                           // Check is there is already a prefab spawned
            {
                Destroy(centerSpawn.transform.GetChild(0).gameObject);                                          // Deletes the first child of that spawn location                            

            }

            byte randomArrayIndex = (byte)Random.Range(0, prefabArray.Length);

            GameObject prefabToBeSpawned = Instantiate(prefabArray[randomArrayIndex],                           // Sets the prefab to be spawned at the specified location
                centerSpawn.transform.position,
               centerSpawn.transform.rotation);

            prefabToBeSpawned.transform.SetParent(centerSpawn.transform);                                       // Sets the spawn location as parent of the prefab being
                                                                                                                // spawned

            Quaternion target = Quaternion.Euler(0, -35, 0);

            // transform.rotation = Quaternion.RotateTowards(transform.rotation, target, 0);
        
    }
}
