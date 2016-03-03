using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UnpauseButton : MonoBehaviour {

    GameManagerScript gameManager;
    Ray ray;
    Camera mainCamera;
    float xScreen;
    float yScreen;

    // Use this for initialization
    void Start () {
        gameManager = GameManagerScript.Instance();
        mainCamera = Camera.main;
        xScreen = Screen.width / 2;
        yScreen = Screen.height / 2;
    }
	
	// Update is called once per frame
	public void unpause () {
        Debug.Log("pressed!");
	    if (gameManager.paused)
        {
            RaycastHit Hit;
            ray = mainCamera.ScreenPointToRay(new Vector3(xScreen, yScreen, 0));
            if (Physics.Raycast(ray, out Hit))
            {
                Button button = Hit.collider.gameObject.GetComponent<Button>();
                if (button != null && button.tag == "PauseButton")
                {
                    Debug.Log("paused!");
                    gameManager.togglePause();
                }
            }
        }
    }

    void Update()
    {

    }
}
