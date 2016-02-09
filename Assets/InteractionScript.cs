using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class InteractionScript : MonoBehaviour {

    public Button pauseButton;
    bool paused;

	// Use this for initialization
	void Start () {
        pauseButton = pauseButton.GetComponent<Button>();
        paused = false;
    }

    public void pauseGame()
    {
        if (paused == false) {
            Time.timeScale = 0;
            paused = true;
            pauseButton.GetComponent<Text>().text = "Unpause";
        }
        else
        {
            Time.timeScale = 1;
            paused = false;
            pauseButton.GetComponent<Text>().text = "Pause";
        }
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
