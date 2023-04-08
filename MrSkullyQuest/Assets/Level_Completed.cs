using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level_Completed : MonoBehaviour
{
    public GameObject panelLevelCompleted;

    public void LevelCompleted()
    {
        panelLevelCompleted.SetActive(true);
        Time.timeScale = 0f;
    }

}
