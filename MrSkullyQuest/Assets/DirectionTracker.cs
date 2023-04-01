using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirectionTracker : MonoBehaviour
{
    public bool onStraightChunk;
    public bool onLeftChunk;
    public bool onRightChunk;
    // Start is called before the first frame update
    void Start()
    {
        onStraightChunk = false;
        onLeftChunk = false;
        onRightChunk = false;
    }

   
}
