using System.Collections;
using System.Collections.Generic;
using System.IO;
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
     * The animation type Enum
     */
    public enum AnimationType { TRASLATION = 0, FRAME_TO_FRAME = 1 }
    /**
     * The animation type.
     */
    public AnimationType type;
    /**
     * The animation image.
     */
    public Sprite image;
    /**
     * The animation images.
     */
    public List<Sprite> animationImages;
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
     * Whether the animation loops or not
     */
    public bool loop;
    /**
     * Elapsed time since the last time a dialogue was loaded
     */
    private float elapsedTime;

    /**
     * Creates a new instance of the StoryAnimation class.
     * @param animation The JSON object with the animarion data.
     * @author Datio Urdapilleta
     * @since 02/14/2023
     */
    public StoryAnimation (JSONAnimations animation)
    {
        this.id = animation.image;
        //this.image = AssetDatabase.LoadAssetAtPath<Sprite>(animation.image);
        this.image = GetSprites(animation.image);
        //Debug.LogError(animation.image);
        this.speed = animation.speed;
        this.startPosition = animation.startPosition != null ? new Vector3(animation.startPosition.x, animation.startPosition.y, 0.0f) : new Vector3();
        this.endPosition = animation.endPosition != null ?  new Vector3(animation.endPosition.x, animation.endPosition.y, 0.0f) : new Vector3();
        this.elapsedTime = 0;
        this.animationImages = new List<Sprite>();
        switch(animation.type)
        {
            case "Traslation":
                this.type = AnimationType.TRASLATION;
                break;
            case "FrameToFrame":
                this.type = AnimationType.FRAME_TO_FRAME;
                string extension;
                string[] nameSplit;
                // Get the file list
                string folder = animation.image.Substring(0, animation.image.LastIndexOf("/"));
                Sprite[] animationSprites = Resources.LoadAll<Sprite>(folder);
                for (int fileCounter = 0; fileCounter < animationSprites.Length; fileCounter++)
                {
                    animationImages.Add(animationSprites[fileCounter]);
                }
                break;
            default:
                this.type = AnimationType.TRASLATION;
                break;
        }
    }
    /**
     * Returns the current sprite for a frame-to-frame animation.
     * @param elapsedTime The elapsed time since the last update.
     * @return The current frame of the animation according to the elapsed time.
     * @author Datio Urdapilleta
     * @since 02/14/2023
     */
    public Sprite GetCurrentSprite(float elapsedTime)
    {
        this.elapsedTime += elapsedTime;
        int spriteNumber = (int)((this.elapsedTime / this.speed) * this.animationImages.Count);
        if(spriteNumber >= this.animationImages.Count)
        {
            spriteNumber = this.animationImages.Count - 1;
        }
        return this.animationImages[spriteNumber];
    }

    /**
     *  AssetDatabase  only works in the editor.
     *  This block loads elements using an Object load asset
     *  @param filename The path to the elements
     *  @author David Lopez
     *  @since third iteration delivery date
     */
    Sprite GetSprites(string fileName)
    {
        Sprite sprites = Resources.Load<Sprite>(fileName);
        return sprites;
    }
}
