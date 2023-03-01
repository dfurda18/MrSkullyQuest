using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Class that represents a dialogue from Json file.
 * @author Dario Urdapilleta
 * @since 02/14/2023
 * @version 1.0
 */
[System.Serializable]
public class JSONDialogue
{
    /**
     * The character's name
     */
    public string character;
    /**
     * The dialogue
     */
    public string dialogue;
    /**
     * The avatar image.
     */
    public string avatar;
    /**
     * The array of animations to make for the dialogue.
     */
    public JSONAnimations[] animations;
}
