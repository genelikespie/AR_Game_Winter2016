using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PauseButton : MonoBehaviour {

    private MenuManager menuManager;
    /*
    Ray ray;
    Camera mainCamera;
    float xScreen;
    float yScreen;
     * */
    void Awake()
    {
        menuManager = MenuManager.Instance();
        /*
        mainCamera = Camera.main;
        xScreen = Screen.width / 2;
        yScreen = Screen.height / 2;
         * */
    }
    
    public void TogglePause()
    {
        bool paused = menuManager.TogglePause();
        if (paused)
        {
            Debug.Log("Game Menu Paused!");
            GetComponent<Text>().text = "Unpause";
        }
        else
        {
            //gameManager.togglePause();
            GetComponent<Text>().text = "Pause";
        }
    }
}
