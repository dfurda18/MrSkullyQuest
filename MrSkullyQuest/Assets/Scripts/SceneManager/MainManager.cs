using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Rendering.Universal.Internal;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/**
 * This class manages the scene sequence.
 * @author Dario Urdapilleta
 * @since 02/16/2023
 * @version 1.0
 */
public class MainManager : MonoBehaviour
{
    /**
     * The type of levels enum
     */
    private enum LevelType { MAIN_MENU = 0, STORY = 1, GAME = 2 }
    /**
     * The location of the levels directory
     */
    private static string contentsURL = "Assets/Content/";
    /**
     * The instance of this object
     */
    public static MainManager Instance;
    /**
     * The loading page
     */
    public GameObject LoaderUI;
    /**
     * The progress Bar
     */
    public Slider progressSlider;
    /**
     * The level list of json file names.
     */
    private static List<string> levels;
    /**
     * The list of level types
     */
    private static List<int> levelTypes;
    /**
     * The current level to load
     */
    private static int currentLevel;
    /**
     * The player's Max Life
     */
    public static int MAX_LIFE = 6;
    /**
     * The player's Current Life
     */
    public static int CURRENT_LIFE = 3;

    /**
     * Mewhod calld at the begining of the load
     */
    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
        DontDestroyOnLoad(LoaderUI);
        levels = new List<string>();
        levelTypes = new List<int>();
        currentLevel = 0;
        LoadLevels();
    }
    /**
     * Starts a new Game
     * @author Dario Urdapilleta
     * @since 02/16/2023
     */
    public static void NewGame()
    {
        currentLevel = 0;
        Instance.LoadScene();
    }
    /**
     * Loads the next level
     * @author Dario Urdapilleta
     * @since 02/16/2023
     */
    public static void LoadNextLevel()
    {
        currentLevel++;
        Instance.LoadScene();
    }
    /**
     * Loads the levels list.
     * @author Dario Urdapilleta
     * @since 02/16/2023
     */
    public static void LoadLevels()
    {
        LevelType levelType;
        string levelTypeString, extension;
        string[] nameSplit;

        // Get the file list
        DirectoryInfo info = new DirectoryInfo(contentsURL);
        FileInfo[] files = info.GetFiles();
        for (int fileCounter = 0; fileCounter < files.Length; fileCounter++)
        {
            // Get the file extension
            nameSplit = files[fileCounter].Name.Split(".");
            extension = nameSplit[nameSplit.Length - 1];

            // Make sure to add only jsons
            if(extension == "json")
            {
                // Get the scene type from the file name
                nameSplit = files[fileCounter].Name.Split("_");
                if (nameSplit.Length > 1)
                {
                    levelTypeString = nameSplit[1];
                }
                else
                {
                    levelTypeString = "ERROR";
                }
                switch (levelTypeString)
                {
                    case "Story":
                        levelType = LevelType.STORY;
                        break;
                    case "Game":
                        levelType = LevelType.GAME;
                        break;
                    default:
                        levelType = LevelType.MAIN_MENU;
                        break;
                }

                // Add the level type and file name
                levelTypes.Add((int)levelType);
                levels.Add(contentsURL + files[fileCounter].Name);
            }
        }
    }
    /**
     * Returns the current level's JSON url
     * @return The current level's JSON url.
     * @author Dario Urdapilleta
     * @since 02/16/2023
     */
    public static string GetCurrentLevel()
    {
        return levels[currentLevel];
    }
    /**
     * Loads a scene using the loading scene while the scene is been loaded.
     * @author Dario Urdapilleta
     * @since 02/16/2023
     */
    public void LoadScene()
    {
        LoaderUI.SetActive(true);
        StartCoroutine(this.LoadScene_Coroutine(GetCurrentScene()));
    }
    /**
     * Returns the current scene to load.
     * @return The int that represents the current scene to load.
     * @author Dario Urdapilleta
     * @since 02/16/2023
     */
    public static int GetCurrentScene()
    {
        if(currentLevel >= levelTypes.Count)
        {
            return (int)LevelType.MAIN_MENU;
        }
        else
        {
            return levelTypes[currentLevel];
        }
    }
    /**
     * Coroutine to load the next scene
     * @param scene The scene to load,
     * @author Juan Giraldo
     */
    public IEnumerator LoadScene_Coroutine(int scene)
    {
        progressSlider.value = 0;

        // Load the level asynchronously
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(scene);
        asyncOperation.allowSceneActivation = false;
        float progress = 0;

        while (!asyncOperation.isDone)
        {
            progress = Mathf.MoveTowards(progress, asyncOperation.progress, Time.deltaTime);
            progressSlider.value = progress;
            if (progress >= 0.9f)
            {
                progressSlider.value = 1;
                asyncOperation.allowSceneActivation = true;
                
            }
            yield return null;
        }
    }
    /**
     * Hides the loading screen
     * @author Dario Urdapilleta
     * @since 02/16/2023
     */
    public static void HideLoadingScreen()
    {
        Instance.LoaderUI.SetActive(false);
    }
}
