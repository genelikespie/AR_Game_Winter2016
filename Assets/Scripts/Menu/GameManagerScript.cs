using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Assertions;
using System.Collections;

public class GameManagerScript : MonoBehaviour {

    public bool paused;
    private bool scrollOver;

    // Use these deltaTime variables for when the game is paused
    private float timeOfLastFrame;
    public float timeDeltaTime;

    public AudioSource backgroundMusic;
    public AudioSource buttonPress;

    int counter = 0;

    private static GameManagerScript instance;
    private static Object instance_lock = new Object();
    public static GameManagerScript Instance()
    {
        if (instance != null)
            return instance;
        lock (instance_lock)
        {
            instance = (GameManagerScript)FindObjectOfType(typeof(GameManagerScript));
            if (FindObjectsOfType(typeof(GameManagerScript)).Length > 1)
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

    // Use this for initialization
    void Awake()
    {
        scrollOver = false;
        pauseGame();
    }
	
    void Start()
    {
    }

    public void togglePause()
    {
        counter++;
        if (counter > 1)
        {
            if (paused == true)
            {
                unPauseGame();
            }
            else
            {
                pauseGame();
            }
            counter = 0;
        }
    }

    public void unPauseGame()
    {
        //Debug.Log("unpaused music");
        backgroundMusic.UnPause();
        Time.timeScale = 1;
        paused = false;
    }

    public void pauseGame()
    {
        //Debug.Log("paused music");
        backgroundMusic.Pause();
        Time.timeScale = 0;
        paused = true;
    }

	// Update is called once per frame
	void Update () {
        timeDeltaTime = Time.realtimeSinceStartup - timeOfLastFrame;
        timeOfLastFrame = Time.realtimeSinceStartup;
    }

    public void PauseTrackableLost()
    {
        //pause game
        pauseGame();
    }

    public void PlayerLost()
    {
        Debug.Log("PLAYER LOST");
        ChangeScenes(0);
    }

    public void ChangeScenes(int index)
    {
        buttonPress.Play();
        Application.LoadLevel(index);
    }
}
