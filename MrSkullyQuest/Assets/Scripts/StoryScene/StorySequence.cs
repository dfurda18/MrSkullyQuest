using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

/**
 * This class represents a story sequence that chares the same background image.
 * @author Dario Urdapilleta
 * @since 02/14/2023
 * @version 1.0
 */
public class StorySequence
{
    /**
     * The background image.
     */
    public Sprite background;
    /**
     * The dialogue list.
     */
    public Dialogue[] dialogues;
    /**
     * Creates a new Story Sequence
     * @param background The path to the background image.
     * @param dialogues The Json object with the dialogues information.
     * @author Dario Urdapilleta
     * @since 02/14/2023
     */
    public StorySequence(string background, JSONDialogue[] dialogues)
    {
        Sprite avatar;
        this.background = AssetDatabase.LoadAssetAtPath<Sprite>(background);
        this.dialogues = new Dialogue[dialogues.Length];
        for(int counter = 0; counter < dialogues.Length; counter++)
        {
            avatar = AssetDatabase.LoadAssetAtPath<Sprite>(dialogues[counter].avatar);
            this.dialogues[counter] = new Dialogue(dialogues[counter]);
        }
    }

}
