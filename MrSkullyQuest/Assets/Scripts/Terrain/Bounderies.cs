using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class Bounderies : MonoBehaviour
{

    [ Header ( "Bounderies Settings")]
    [Tooltip("Bounderies limits for the player movment")]
    
    public static float limitLeft = -10.5f;
    public static float limitRight = 10.5f;
    public float innerRight;
    public float innerLeft;
    
    void Update()
    {
        innerRight = limitRight;
        innerLeft = limitLeft; 
       
    }
}
