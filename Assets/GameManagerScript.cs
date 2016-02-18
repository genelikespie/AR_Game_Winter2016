using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameManagerScript : MonoBehaviour {

    GameObject PauseButton;
    private static GameManagerScript instance;
    private static Object instance_lock = new Object();

    public Button pauseButton;
    private bool paused;

    public GameObject pauseCylinder;
    public GameObject BTMCylinder;

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
        pauseButton = GameObject.Find("PauseButton").GetComponent<Button>();
        if (!pauseButton)
        {
            Debug.LogError("Cannot find pause button!");
        }
    }
	
    void Start()
    {
        paused = true;
    }

    public void pauseGame()
    {
        if (paused == false)
        {
            Time.timeScale = 0;
            paused = true;
            pauseButton.GetComponent<Text>().text = "Unpause";
        }
        else
        {
            Time.timeScale = 1;
            paused = false;
            pauseButton.GetComponent<Text>().text = "Pause";
            pauseCylinder.transform.position = new Vector3(13, 117, -1);
            BTMCylinder.transform.position = new Vector3(-11, 117, -1);
        }
    }


	// Update is called once per frame
	void Update () {
        if (paused == true && pauseCylinder.transform.position.y > 3)
        {
            pauseCylinder.transform.Translate(Vector3.back * 5);
            BTMCylinder.transform.Translate(Vector3.back * 5);
        }
    }

    public void PauseTrackableLost()
    {
        //pause game
        pauseGame();
    }

    public void PlayerLost()
    {
        Debug.Log("PLAYER LOST");
    }
}
