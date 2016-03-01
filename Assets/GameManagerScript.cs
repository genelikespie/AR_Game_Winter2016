using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Assertions;
using System.Collections;

public class GameManagerScript : MonoBehaviour {

    //GameObject PauseButton;


    //public Button pauseButton;
    public bool paused;
    private bool scrollOver;
    /*
    public GameObject pauseCylinder;
    public GameObject BTMCylinder;

     * */
    // Use these deltaTime variables for when the game is paused
    private float timeOfLastFrame;
    public float timeDeltaTime;

    private MenuManager menuManager;

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
        /*
        pauseButton = GameObject.Find("Pause").GetComponent<Button>();
        if (!pauseButton)
        {
            Debug.LogError("Cannot find pause button!");
        }
         * */
        menuManager = MenuManager.Instance();
        Assert.IsTrue(menuManager);
    }
	
    void Start()
    {
        //paused = false;
        pauseGame();
        //headquarters.SetActive(true);
        scrollOver = false;
       /* hCollider = headquarters.GetComponent<BoxCollider>();
        //Debug.Log("sdf ds" + headquarters.GetComponent<BoxCollider>().bounds.extents);
        boundSize = hCollider.bounds.size;
        boundExtent = hCollider.bounds.extents;
        Debug.Log(hCollider.bounds.max + " " + hCollider.bounds.min);*/
    }

    public void togglePause()
    {
        if (paused == true)
        {
            unPauseGame();
        }
        else
        {
            pauseGame();
        }
        /*if (paused == true)
        {
            Time.timeScale = 1;
            paused = false;
            pauseButton.GetComponent<Text>().text = "Pause";
            pauseCylinder.transform.position = new Vector3(13, 117, -1);
            BTMCylinder.transform.position = new Vector3(-11, 117, -1);
        }
        else
        {
            Time.timeScale = 0;
            paused = true;
            pauseButton.GetComponent<Text>().text = "Unpause";
        }*/
    }

    public void unPauseGame()
    {
        Time.timeScale = 1;
        paused = false;
        menuManager.LiftPlatforms();
        //pauseButton.GetComponent<Text>().text = "Pause";
        //pauseCylinder.transform.position = new Vector3(13, 117, -1);
        //BTMCylinder.transform.position = new Vector3(-11, 117, -1);
    }

    public void pauseGame()
    {
        Time.timeScale = 0;
        paused = true;
        menuManager.DropPlatforms();
        //pauseButton.GetComponent<Text>().text = "Unpause";
    }

	// Update is called once per frame
	void Update () {
        timeDeltaTime = Time.realtimeSinceStartup - timeOfLastFrame;
        timeOfLastFrame = Time.realtimeSinceStartup;
        /*
        //Debug.Log(pauseCylinder.transform.position.y);
        
        if (paused == true && pauseCylinder.transform.position.y > 3)
        {
            pauseCylinder.transform.Translate(Vector3.back * 5);
            BTMCylinder.transform.Translate(Vector3.back * 5);
        }
         * */
        //Debug.Log(crosshair.transform.position.x + " " + (center.x + boundExtent.x) + " " + (center.x - boundExtent.x));
        //Debug.Log(center.x + " " + boundExtent.x + " " + boundSize.x);

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
        Application.LoadLevel(index);
    }
}
