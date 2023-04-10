using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level_Completed : MonoBehaviour
{
    public GameObject panelLevelCompleted;
    public AudioSource audioSource;
    //public AudioClip audioClip;

    public void LevelCompleted()
    {
        panelLevelCompleted.SetActive(true);
        
        audioSource.Stop();
        //audioSource.loop = false;
        //audioSource.clip = audioClip;
        //audioSource.volume = 0.5f;
        ////audioSource.PlayOneShot(audioClip,1f);
        //audioSource.clip = audioClip;
        //audioSource.volume = 1f;
        //audioSource.playOnAwake = true;
        //audioSource.Play();
        Time.timeScale = 0f;
    }

}
