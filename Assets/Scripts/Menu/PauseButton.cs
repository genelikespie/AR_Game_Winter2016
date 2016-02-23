using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PauseButton : MonoBehaviour {

    private GameManagerScript gameManager;
    void Awake()
    {
        gameManager = GameManagerScript.Instance();
    }
    public void TogglePause()
    {
        gameManager.togglePause();
        if (gameManager.paused)
        {
            GetComponent<Text>().text = "Unpause";
        }
        else
        {
            GetComponent<Text>().text = "Pause";
        }
    }
}
