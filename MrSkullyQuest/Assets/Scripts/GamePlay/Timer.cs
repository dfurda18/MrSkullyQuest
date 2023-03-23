using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

/**
 * This class handles the game timer behaviour.
 * @author Dario Urdapilleta
 * @version 1.0
 * @since 03/17/2023
 */
public class Timer : MonoBehaviour
{
    /**
     * Reference fo the time label
     */
    public TextMeshProUGUI timeLabel;
    /**
     * The current timer time.
     */
    private float time;
    /**
     * Boolean that states if the timer is running or not
     */
    private bool isRunning;
    // Start is called before the first frame update
    void Start()
    {
        isRunning = false;
        time = 0.0f;
        // TODO: Start this in the gameplay controller
        this.StartTimer();
    }

    // Update is called once per frame
    void Update()
    {
        if(isRunning)
        {
            time += Time.deltaTime;
            timeLabel.text = this.GetTimeLabel();
        }
        
    }

    /**
     * Method that starts the timmer.
     * @author Dario Urdapilleta
     */
    public void StartTimer()
    {
        this.isRunning = true;
    }

    /**
     * Method that stops the timmer.
     * @author Dario Urdapilleta
     */
    public void StopTimer()
    {
        this.isRunning = false;
    }

    /**
     * Returns the time in seconds
     * @return The time in seconds.
     * @author Dario Urdapilleta
     */
    public float GetTime()
    {
        return this.time;
    }

    /**
     * Returns the time as a text
     * @return The time as a text.
     * @author Dario Urdapilleta
     */
    public string GetTimeLabel()
    {
        int minutes = 0;
        float seconds = 0;
        string minutesString, secondsString;

        // Calculate the minutes and seconds
        minutes = (int)time / 60;
        seconds = (int)time % 60;

        // convert to string and add a 0 if they are single-digit
        minutesString = (minutes < 10 ? "0" : "") + minutes.ToString();
        secondsString = (seconds < 10 ? "0" : "") + seconds.ToString();

        return minutesString + ":" + secondsString;
    }
}
