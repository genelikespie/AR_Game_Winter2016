using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameManagerScript : MonoBehaviour {

    GameObject PauseButton;
    private static GameManagerScript instance;
    private static Object instance_lock = new Object();

    public Button pauseButton;
    private bool paused;
    private bool scrollOver;

    public GameObject pauseCylinder;
    public GameObject BTMCylinder;

    public GameObject headquarters;
    public GameObject crosshair;
    public Collider hCollider;
    public Vector3 boundSize;
    public Vector3 boundExtent;

    public Vector3 center;

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
        pauseButton = GameObject.Find("Pause").GetComponent<Button>();
        if (!pauseButton)
        {
            Debug.LogError("Cannot find pause button!");
        }
    }
	
    void Start()
    {
        //paused = false;
        pauseGame();
        headquarters.SetActive(true);
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
        pauseButton.GetComponent<Text>().text = "Pause";
        pauseCylinder.transform.position = new Vector3(13, 117, -1);
        BTMCylinder.transform.position = new Vector3(-11, 117, -1);
    }

    public void pauseGame()
    {
        Time.timeScale = 0;
        paused = true;
        pauseButton.GetComponent<Text>().text = "Unpause";
    }

	// Update is called once per frame
	void Update () {
        //Debug.Log(pauseCylinder.transform.position.y);
        if (paused == true && pauseCylinder.transform.position.y > 3)
        {
            pauseCylinder.transform.Translate(Vector3.back * 5);
            BTMCylinder.transform.Translate(Vector3.back * 5);
        }
        Debug.Log(crosshair.transform.position.x + " " + (center.x + boundExtent.x) + " " + (center.x - boundExtent.x));
        //Debug.Log(center.x + " " + boundExtent.x + " " + boundSize.x);
        if ((crosshair.transform.position.x < (center.x + boundExtent.x)) && 
            (crosshair.transform.position.x > (center.x - boundExtent.x)) &&
            (crosshair.transform.position.z > (center.z - boundExtent.z)) &&
            (crosshair.transform.position.z > (center.z - boundExtent.z)))
        {
            pauseGame();
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
