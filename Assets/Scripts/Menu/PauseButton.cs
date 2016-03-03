﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PauseButton : MonoBehaviour {

    private GameManagerScript gameManager;
    Ray ray;
    Camera mainCamera;
    float xScreen;
    float yScreen;
    //private moveCrosshair crosshair;
    void Awake()
    {
        gameManager = GameManagerScript.Instance();
        mainCamera = Camera.main;
        xScreen = Screen.width / 2;
        yScreen = Screen.height / 2;
        //crosshair = moveCrosshair.Instance();
    }
    
    public void TogglePause()
    {
        gameManager.togglePause();
        if (gameManager.paused)
        {
            Debug.Log("pressed!");
            /*RaycastHit Hit;
            ray = mainCamera.ScreenPointToRay(new Vector3(xScreen, yScreen, 0));
            if (Physics.Raycast(ray, out Hit))
            {
                Button button = Hit.collider.gameObject.GetComponent<Button>();
                Debug.Log(button);
                if (button != null && button.tag == "PauseButton")
                {
                    Debug.Log("paused!");
                    gameManager.togglePause();
                }
            }*/
            GetComponent<Text>().text = "Unpause";
        }
        else
        {
            //gameManager.togglePause();
            GetComponent<Text>().text = "Pause";
        }
    }

    void Update()
    {
        /*if (gameManager.paused && Input.GetMouseButtonDown(0))
        {
            TogglePause();
        }*/
    }
}
