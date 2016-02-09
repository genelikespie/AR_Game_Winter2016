using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class InteractionScript : MonoBehaviour {

    public Button pauseButton;

	// Use this for initialization
	void Start () {
        pauseButton = pauseButton.GetComponent<Button>();
    }

    public void pauseGame()
    {
        Time.timeScale = 0;
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
