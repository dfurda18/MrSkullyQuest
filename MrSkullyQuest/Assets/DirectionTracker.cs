using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirectionTracker : MonoBehaviour
{
    public bool goingForward;
    public bool goingRight;
    public bool goingLeft;
    // Start is called before the first frame update
    void Start()
    {
        goingForward= true;
        goingRight= false;
        goingLeft= false;
    }

   
}
