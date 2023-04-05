using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IObstacles
{
    void OnHit();
    /**
     * This method is called when an obstacle hits the player
     * @author Fred
     * @since 05/04/2023
     */

    void Moving();
     /**
     * This method is called when an obstacle that has movement is called.
     * @author Fred
     * @since 05/04/2023
     */

    void IsActive();
     /**
     * This method is called to make active the carrots trap. If is active, the trap is called
     * @author Fred
     * @since 05/04/2023
     */
}
