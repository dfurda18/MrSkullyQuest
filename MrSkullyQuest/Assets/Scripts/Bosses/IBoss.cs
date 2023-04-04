using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * This interface defines what a Boss should do
 * @author Dario Urdapilleta
 * @since 04/04/2023
 */
public interface IBoss
{
    
    /**
     * This method is called whenever a boss is hit by the player
     * @author Dario Urdapilleta
     * @since 04/04/2023
     */
    void getHit();
    /**
     * This method is called when a boss attackes
     * @author Dario Urdapilleta
     * @since 04/04/2023
     */
    void attack();
    /**
     * This method is called when a boss dies
     * @author Dario Urdapilleta
     * @since 04/04/2023
     */
    void die();
}
