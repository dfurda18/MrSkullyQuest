using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

using DG.Tweening;
using UnityEngine.Rendering.Universal.Internal;
using Unity.VisualScripting;
using System.IO;
using System.Runtime.CompilerServices;

/**
 * This class represents a story scene manager.
 * @author Dario Urdapilleta
 * @since 02/14/2023
 * @version 1.0
 */
public class StorySceneManager : MonoBehaviour
{
    /**
     * The TAG used to identify the temporary elements.
     */
    private static string TAG = "StoryTemporaryImage";
    /**
     * The background image container
     */
    public GameObject backgroundContainer;
    /**
     * The backgrouns image
     */
    public Image background;
    /**
     * The avatar image.
     */
    public Image avatar;
    /**
     * The character name text
     */
    public TextMeshProUGUI character;
    /**
     * The fialogue text.
     */
    public TextMeshProUGUI dialogue;
    /**
     * The url to read the JSON
     */
    private string jsonURL = "";
    /**
     * The loaded pages.
     */
    private StorySequence[] pages;
    /**
     * The current page.
     */
    private int currentPage = 0;
    /**
     * The current dialogue.
     */
    private int currentDialogue = 0;
    /**
     * The temporary images.
     */
    private Dictionary<string, GameObject> images;
    /**
     * The animation sequences.
     */
    private List<DG.Tweening.Sequence> sequences;

    /**
     * Method called at the start.
     */
    public void Start()
    {
        // Initiate all values.
        this.jsonURL = MainManager.GetCurrentLevel();
        StreamReader reader = new StreamReader(jsonURL);
        this.images = new Dictionary<string, GameObject>();
        this.sequences = new List<DG.Tweening.Sequence>();


        // Read the file and create all the pages
        string json = reader.ReadToEnd();
        JSON_StorySequence[] jsonPages = JsonUtility.FromJson<JSONStories>(json).stories;
        reader.Close();
        this.pages = new StorySequence[jsonPages.Length];
        for (int counter = 0; counter < jsonPages.Length; counter++)
        {
            this.pages[counter] = new StorySequence(jsonPages[counter].background, jsonPages[counter].dialogues);
        }

        // Load the first page
        
        this.LoadFirstPage();
    }
    /**
     * Method called at runtime.
     */
    public void Update()
    {
        if (Input.GetButtonDown("Submit"))
        {
            if(this.dialogue.GetComponent<TextTimer>().ShowAll())
            {
                if(this.NextPage())
                {
                    // It has more pages
                } else
                {
                    // no more pages
                    MainManager.LoadNextLevel();
                }
            }
        }
    }
    /**
     * Loads the first page.
     * @author Dario Urdapilleta
     * @since 02/14/2023
     */
    public void LoadFirstPage ()
    {
        this.currentPage = 0;
        this.LoadPage();
    }
    /**
     * Loads the the current dialogue and plays its animations.
     * @author Dario Urdapilleta
     * @since 02/14/2023
     */
    public void LoadDialogue()
    {
        // Set the texts
        this.avatar.sprite = this.pages[this.currentPage].dialogues[this.currentDialogue].avatar;
        this.character.text = this.pages[this.currentPage].dialogues[this.currentDialogue].character + ":";

        // Show the dialogue in a timed manner
        this.dialogue.GetComponent<TextTimer>().ShowText(this.pages[this.currentPage].dialogues[this.currentDialogue].dialogue);

        // Play all the animations
        foreach (StoryAnimation animation in this.pages[this.currentPage].dialogues[this.currentDialogue].animations)
        {
            this.PlayAnimation(animation);
        }
    }
    /**
     * Loads the current page.
     * @author Dario Urdapilleta
     * @since 02/14/2023
     */
    public void LoadPage ()
    {
        // Kill all the animations
        foreach (DG.Tweening.Sequence sequence in this.sequences)
        {
            sequence.Kill();
        }
        this.sequences.Clear();

        // Destroy all the animation objects
        foreach (Transform t in this.backgroundContainer.transform) {
            if(t.gameObject.tag == StorySceneManager.TAG)
            {
                GameObject.Destroy(t.gameObject);
            }
            
        }
        foreach (KeyValuePair<string, GameObject> image in this.images)
        {
            GameObject.Destroy(image.Value);
        }
        this.images.Clear();

        // Change the background image
        this.background.sprite = this.pages[this.currentPage].background;

        // Load the first dialogue
        this.currentDialogue = 0;
        this.LoadDialogue();
    }
    /**
     * Loads the next page or dialogue.
     * @return True if another page was loaded, and false if there's no pages or dialogues to show.
     * @author Dario Urdapilleta
     * @since 02/14/2023
     */
    public bool NextPage()
    {
        // check if there's another dialogue in the same page
        this.currentDialogue++;
        if(this.currentDialogue >= this.pages[this.currentPage].dialogues.Length) // No more dialogues
        {
            // Check if theres another page 
            this.currentPage++;
            if (this.currentPage >= this.pages.Length) // No more pages
            {
                return false;
            } else
            {
                // Load the next page
                this.LoadPage();
                return true;
            }
        }
        else
        {
            // Load the next dialogue
            this.LoadDialogue();
            return true;
        }
    }
    /**
     * Plays an animation
     * @param animarion The animation to play.
     * @author Dario Urdapilleta
     * @since 02/14/2023
     */
    public void PlayAnimation(StoryAnimation animation)
    {
        GameObject imgObject;
        // Check if the object has been animated previously
        if (this.images.ContainsKey(animation.id))
        {
            // get the prevoiusly animated object
            imgObject = this.images[animation.id];
        }
        else
        {
            // Create a new object
            imgObject = new GameObject("animation sprite");
            RectTransform trans = imgObject.AddComponent<RectTransform>();
            trans.anchorMin = new Vector2(0, 0);
            trans.anchorMax = new Vector2(1, 1);
            trans.pivot = new Vector2(0.0f, 0.0f);
            trans.transform.SetParent(backgroundContainer.transform);
            trans.localScale = Vector3.one;
            trans.sizeDelta = new Vector2(0.0f, 0.0f);
            trans.transform.position += animation.startPosition;

            Image image = imgObject.AddComponent<Image>();
            image.sprite = animation.image;
            imgObject.transform.SetParent(backgroundContainer.transform);
            imgObject.tag = StorySceneManager.TAG;
            this.images.Add(animation.id, imgObject);
        }
        // Create the sequence
        DG.Tweening.Sequence sequence = DOTween.Sequence();
        if(imgObject != null)
        {
            // Animate the object
            sequence.Append(imgObject.transform.DOMove(animation.endPosition, animation.speed).SetEase(Ease.OutQuint));
        }
    }
}
