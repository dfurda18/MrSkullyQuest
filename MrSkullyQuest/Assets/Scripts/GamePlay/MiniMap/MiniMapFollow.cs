using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * This Class allows the minimap camera to follow the player without rotating.
 * @author Dario Urdapilleta
 * @version 1.0
 * @since 03/15/2023
 */
public class MiniMapFollow : MonoBehaviour
{
    /**
     * The object to follow
     */
    public GameObject follow;

    // Start is called before the first frame update
    void Start()
    {
        this.transform.position = new Vector3(follow.transform.position.x, 70.0f, follow.transform.position.z);
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position = new Vector3(follow.transform.position.x, 70.0f, follow.transform.position.z);
    }
}
