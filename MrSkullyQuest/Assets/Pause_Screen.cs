using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause_Screen : MonoBehaviour
{
    public GameObject panelPause;
 
    public void Pause()
    {
        Time.timeScale = 0f;
        panelPause.SetActive(true);
    }


    public void UnPause()
    {
        Time.timeScale = 1f;
        panelPause.SetActive(false);
    }
}
