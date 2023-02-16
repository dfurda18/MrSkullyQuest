using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Experimental.AssetDatabaseExperimental.AssetDatabaseCounters;

/**
 * Class that represents a dialogue.
 * @author Dario Urdapilleta
 * @since 02/14/2023
 * @version 1.0
 */
public class Dialogue
{
    /**
     * The character's name
     */
    public string character { get; set; }
    /**
     * The dialogue
     */
    public string dialogue { get; set; }
    /**
     * The avatar image.
     */
    public Sprite avatar { get; set; }
    /**
     * The dialogue animations.
     */
    public StoryAnimation[] animations { get; set; }
    /**
     * Creates a new Dialogue
     * @author Dario Urdapilleta
     * @since 02/14/2023
     */
    public Dialogue (JSONDialogue dialogue)
    {
        this.character = dialogue.character;
        this.dialogue = dialogue.dialogue;
        this.avatar = AssetDatabase.LoadAssetAtPath<Sprite>(dialogue.avatar); ;
        this.animations = new StoryAnimation[dialogue.animations.Length];
        for(int counter = 0; counter < dialogue.animations.Length; counter++ )
        {
            this.animations[counter] = new StoryAnimation(dialogue.animations[counter]);
        }
    }
}
