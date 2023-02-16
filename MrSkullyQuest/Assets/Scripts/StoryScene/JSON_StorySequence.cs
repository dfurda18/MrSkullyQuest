using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * This class represents a story sequence that shares the same background image from a Json file.
 * @author Dario Urdapilleta
 * @since 02/14/2023
 * @version 1.0
 */
[System.Serializable]
public class JSON_StorySequence
{
    /**
     * The background image.
     */
    public string background;
    /**
     * The dialogue list.
     */
    public JSONDialogue[] dialogues;
}
