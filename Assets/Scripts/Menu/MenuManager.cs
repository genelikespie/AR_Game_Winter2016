using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Assertions;
using System.Collections;
using System.Collections.Generic;

public class MenuManager : MonoBehaviour {

    private List<FloatingPlatform> platformsList;

    // Message Board
    public Transform messageBoard;

    // references to any pause buttons we might have
    private PauseButton pauseButton;
    private GameManagerScript gameManager;
    public float acceleration = 5f;
    public bool dropped; // if the menu manager is dropped or not

    private static MenuManager instance;
    private static Object instance_lock = new Object();

    public static MenuManager Instance()
    {
        if (instance != null)
            return instance;
        lock (instance_lock)
        {
            instance = (MenuManager)FindObjectOfType(typeof(MenuManager));
            if (FindObjectsOfType(typeof(MenuManager)).Length > 1)
            {
                Debug.LogError("There can only be one instance!");
                return instance;
            }
            if (instance != null)
                return instance;
            Debug.LogError("Could not find a instance!");
            return null;
        }
    }

    void Awake()
    {
        platformsList = new List<FloatingPlatform>();
        IEnumerable childPlatforms = GetComponentsInChildren<FloatingPlatform>();
        gameManager = GameManagerScript.Instance();

        foreach (FloatingPlatform platform in childPlatforms) {
            platformsList.Add(platform);
            platform.acceleration = acceleration;
        }
        pauseButton = GameObject.FindObjectOfType<PauseButton>();

        Assert.IsTrue(gameManager);
    }

    void Start()
    {
        PauseGame();
    }

    void Update()
    {
        
    }
    public void PauseGame()
    {
        gameManager.pauseGame();
        DropPlatforms();
    }
    public void UnpauseGame()
    {
        gameManager.unPauseGame();
        LiftPlatforms();
    }
    public bool TogglePause()
    {
        if (!gameManager.paused)
        {
            PauseGame();
            return true;
        }
        else
        {
            UnpauseGame();
            return false;
        }
    }

    public void DropPlatforms()
    {
        dropped = true;
        foreach (FloatingPlatform p in platformsList)
        {
            if (p.canMove)
                p.Drop();
        }
    }
    public void LiftPlatforms()
    {
        dropped = false;
        foreach (FloatingPlatform p in platformsList)
        {
            if (p.canMove)
                p.Lift();
        }
    }

}
