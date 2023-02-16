using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Class to read an animation object from a JSON
 * @author Dario Urdapilleta
 * @since 02/13/2023
 * @version 1.0
 */
[System.Serializable]
public class JSONAnimations
{
    /**
     * The image url
     */
    public string image;
    /**
     * The animation speed
     */
    public float speed;
    /**
     * The starting position
     */
    public JSON2DPosition startPosition;
    /**
     * The end position
     */
    public JSON2DPosition endPosition;
}
