﻿using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Assertions;
using System.Collections;

public class GameManagerScript : MonoBehaviour {

    public bool paused;
    private bool scrollOver;

    // Use these deltaTime variables for when the game is paused
    private float timeOfLastFrame;
    public float timeDeltaTime;

    public AudioSource scene1;
    public AudioSource scene2;
    public AudioSource scene3;
    public AudioSource winmusic;
    public AudioSource lossmusic;

    public AudioSource buttonPress;

    int counter = 0;
    MessageBoard messageBoard;

    public static bool applicationIsQuiting = false;
    private static GameManagerScript instance;
    private static Object instance_lock = new Object();
    public static GameManagerScript Instance()
    {
        if (applicationIsQuiting)
            return null;
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
    void OnApplicationQuit()
    {
        applicationIsQuiting = true;
    }
    // Use this for initialization
    void Awake()
    {
        Debug.Log(PlayerPrefs.GetInt("Select") + " Selected!");
        messageBoard = MessageBoard.Instance();
        Assert.IsTrue(messageBoard);
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
        int scene = PlayerPrefs.GetInt("Scene");
        if (scene == 1)
        {
            scene1.UnPause();
        }
        else if (scene == 2)
        {
            scene2.UnPause();
        }
        else if (scene == 3)
        {
            scene3.UnPause();
        }
        else
        {
            scene1.UnPause();
        }
        Time.timeScale = 1;
        paused = false;
    }

    public void pauseGame()
    {
        //Debug.Log("paused music");
        int scene = PlayerPrefs.GetInt("Scene");
        if ((scene1 && scene2 && scene3))
        {
            if (scene == 1)
            {
                scene1.Pause();
            }
            else if (scene == 2)
            {
                scene2.Pause();
            }
            else if (scene == 3)
            {
                scene3.Pause();
            }
            else
            {
                scene1.Pause();
            }
        }
        Time.timeScale = 0;
        paused = true;
    }

    public void switchMusic(int scene)
    {
        if (scene == 1)
        {
            ;
        }
        else if (scene == 2)
        {
            scene1.Stop();
            scene2.Play();
        }
        else if (scene == 3)
        {
            scene2.Stop();
            scene3.Play();
        }
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

    public void PlayerLost() {
        scene1.Pause();
        scene2.Pause();
        scene3.Pause();
        lossmusic.Play();

        Debug.Log("PLAYER LOST");
        pauseGame();
        messageBoard.setTitle("You Lost!");
        messageBoard.setBody("Try again next time!");
        messageBoard.setBack("Main Menu", LoadMainMenu);
        messageBoard.ToWorldSpace();
        messageBoard.activateBoard(true);
    }
    public void PlayerWon()
    {
        scene1.Pause();
        scene2.Pause();
        scene3.Pause();
        winmusic.Play();
        Debug.Log("PLAYER WON");
        pauseGame();
        messageBoard.setTitle("You Won!");
        messageBoard.setBody("You defeated the Martians! Matt Damon would be proud!");
        messageBoard.setBack("Main Menu", LoadMainMenu);
        messageBoard.ToWorldSpace();
        messageBoard.activateBoard(true);
    }
    public void ChangeScenes(int index)
    {
        buttonPress.Play();
        Application.LoadLevel(index);
    }
    public void LoadMainMenu()
    {
        Application.LoadLevel(0);
    }
}
