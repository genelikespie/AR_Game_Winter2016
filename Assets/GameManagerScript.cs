using UnityEngine;
using System.Collections;

public class GameManagerScript : MonoBehaviour {

    GameObject PauseButton;
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
    void Awake () {
        PauseButton = GameObject.Find("PauseMenu");
        if (PauseButton == null)
            Debug.LogError("NO PAUSEY");
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void PauseTrackableLost()
    {
        //pause game
        if (!PauseButton)
            Debug.LogError("cant find pause button");
        if (!PauseButton.GetComponent<InteractionScript>())
            Debug.LogError("no interaction script in pause button");
        PauseButton.GetComponent<InteractionScript>().paused = false;
        PauseButton.gameObject.SendMessage("pauseGame", SendMessageOptions.DontRequireReceiver);
        
    }
}
