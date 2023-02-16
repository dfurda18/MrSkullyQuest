using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

/**
 * Class that represents a sotry animation
 * @author Dario Urdapilleta
 * @since 02/14/2023
 * @version 1.0
 */
public class StoryAnimation
{
    /**
     * The animation image.
     */
    public Sprite image;
    /**
     * The animation speed.
     */
    public float speed;
    /**
     * The starting position.
     */
    public Vector3 startPosition;
    /**
     * The end position.
     */
    public Vector3 endPosition;
    /**
     * The animation id. This is the file url.
     */
    public string id;

    /**
     * Creates a new instance of the StoryAnimation class.
     * @param animation The JSON object with the animarion data.
     * @author Datio Urdapilleta
     * @since 02/14/2023
     */
    public StoryAnimation (JSONAnimations animation)
    {
        this.id = animation.image;
        this.image = AssetDatabase.LoadAssetAtPath<Sprite>(animation.image);
        this.speed = animation.speed;
        this.startPosition = new Vector3(animation.startPosition.x, animation.startPosition.y, 0.0f);
        this.endPosition = new Vector3(animation.endPosition.x, animation.endPosition.y, 0.0f);
    }
}
