using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchItems : MonoBehaviour
{
    public AudioSource itemSFX;
    //public GameObject myself;
    private void Start()
    {
        itemSFX = gameObject.GetComponent<AudioSource>();
    }

    void OnTriggerEnter(Collider other)
    {
        itemSFX.Play();
        Destroy(this.gameObject, 2f);
        //this.gameObject.SetActive(false);

    }

    //private void OnTriggerExit(Collider other)
    //{
    //    playEngineSound();
    //    //Destroy(this.gameObject, 2f);
    //}

    //IEnumerator playEngineSound()
    //{

    //    itemSFX.Play();
    //    yield return new WaitForSeconds(1.5f);
    //    Destroy(this.gameObject, 2f);
    //}

   
}
