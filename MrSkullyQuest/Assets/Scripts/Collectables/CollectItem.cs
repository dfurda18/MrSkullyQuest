using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectItem : MonoBehaviour
{
    public AudioSource itemSFX;

    void OnTriggerEnter(Collider other)
    {
        itemSFX.Play();
        this.gameObject.SetActive(false);
       // Destroy(this.gameObject);
    }
}
