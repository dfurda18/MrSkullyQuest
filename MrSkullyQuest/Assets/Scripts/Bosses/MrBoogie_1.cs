using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * This class implements Mr.Boogies mechanics
 * @author Dario Urdapilleta
 * @since 04/04/2023
 */
public class MrBoogie_1 : MonoBehaviour, IBoss
{
    /**
     * The character animator controller
     */
    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    /**
     * This method is called whenever a boss is hit by the player
     * @author Dario Urdapilleta
     * @since 04/04/2023
     */
    public void getHit()
    {
        animator.SetTrigger("Hit");
    }

    /**
     * This method is called when a boss attackes
     * @author Dario Urdapilleta
     * @since 04/04/2023
     */
    public void attack()
    {
        animator.SetTrigger("Throw");
    }

    /**
     * This method is called when a boss dies
     * @author Dario Urdapilleta
     * @since 04/04/2023
     */
    public void die()
    {
        throw new System.NotImplementedException();
    }
    
}
