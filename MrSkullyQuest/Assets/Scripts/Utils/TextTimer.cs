using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

/**
 * Class that handles the text display of a dialogue.
 * @author Dario Urdapilleta
 * @since 02/14/2023
 * @version 1.0
 */
public class TextTimer : MonoBehaviour
{
    /**
     * The time between displaying the letters
     */
    public float timeFrame;

    /**
     * True if the letter is being displayed and false if it is already displayed.
     */
    private bool showing;
    /**
     * The TextMesh object
     */
    private TextMeshProUGUI content;
    /**
     * The time elapsed since it began to show
     */
    private float timeElapsed;
    /**
     * The text to display
     */
    private string text;

    /**
     * Method called at the start of the scene.
     */
    public void Start()
    {
        this.showing = true;
        this.content = GetComponent<TextMeshProUGUI>();
    }
    /**
     * Method called during execution.
     */
    public void Update()
    {
        if(this.showing)
        {
            // Calculate the time elapsed and display as many characters accordingly.
            int charactersToShow = 0;
            this.timeElapsed += Time.deltaTime;
            charactersToShow = (int)(this.timeElapsed / this.timeFrame);
            if(charactersToShow > this.text.Length)
            {
                charactersToShow = this.text.Length;
                this.showing = false;
            }
            this.content.text = this.text.Substring(0, charactersToShow);
        }
        else
        {
            this.content.text = this.text;
        }
    }
    /**
     * Shows the text in a timed sequence.
     * @param text The text to show.
     * @author Dario Urdapilleta
     * @since 02/14/2023
     */
    public void ShowText(string text)
    {
        this.timeElapsed = 0;
        this.text = text;
        this.showing = true;
    }
    /**
     * Shows all the text.
     * @return True if the text is still displaying, false if it was already completely shown.
     * @author Dario Urdapilleta
     * @since 02/14/2023
     */
    public bool ShowAll()
    {
        if(this.showing)
        {
            // Set the elapsed time to the maximum amount
            this.showing = false;
            this.timeElapsed = this.text.Length * this.timeElapsed;
            return false;
        }
        else
        {
            return true;
        }
        
    }
}
