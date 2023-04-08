using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

/**
 * This class handles the bone count interface elements and count.
 * @author Dario Urdapilleta
 * @version 1.0
 * @since 03/17/2023
 */
public class BoneCount : MonoBehaviour
{
    /**
     * Reference fo the count label
     */
    public TextMeshProUGUI countLabel;
    /**
     * The bone count
     */
    private int count;

    // Start is called before the first frame update
    void Start()
    {
        count = 0;
    }

    // Update is called once per frame
    void Update()
    {
        countLabel.text = count.ToString();
    }

    /**
     * This method adds one to the count.
     * @author Dario Urdapilleta
     */
    public void Collect()
    {
        count++;
    }

    /**
     * This method adds N number to the count.
     * @param amount The mount of elements to add to the count.
     * @author Dario Urdapilleta
     */
    public void Collect(int amount)
    {
        count += amount;
    }

    /**
     * Resets the count to 0
     * @author Dario Urdapilleta
     */
    public void ResetCount()
    {
        count = 0;
    }

    /**
     * Returns the count
     * @return The count.
     * @author Dario Urdapilleta
     */
    public int GetCount()
    {
        return count;
    }
}
