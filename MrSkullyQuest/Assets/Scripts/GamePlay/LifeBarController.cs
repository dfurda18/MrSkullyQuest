using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * This class handles Skully's life bar
 * @author Dario Urdapilleta
 * @version 1.0
 * @since 03/15/2023
 */
public class LifeBarController : MonoBehaviour
{
    /**
     * Pointer to the Life
     */
    public GameObject lifeBar;
    private MainManager mainManager;
    // Start is called before the first frame update
    void Start()
    {
        float scale = (float)(MainManager.Instance != null ? MainManager.CURRENT_LIFE : 3) / (float)(MainManager.Instance != null ? MainManager.MAX_LIFE : 6);
        lifeBar.transform.localScale = new Vector3(
            scale,
            0.9f, 
            1.0f
        );
    }

    // Update is called once per frame
    void Update()
    {
        float scale = (float)(MainManager.Instance != null ? MainManager.CURRENT_LIFE : 3) / (float)(MainManager.Instance != null ? MainManager.MAX_LIFE : 6);
        lifeBar.transform.localScale = new Vector3(
            scale,
            0.9f, 
            1.0f
        );
    }
}
