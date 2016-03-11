using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MenuScript : MonoBehaviour {

    public Button startText;
    public Button exitText;

    public Camera cam1;
    public Camera cam2;

    public Canvas startMenu;

	// Use this for initialization
	void Start () {
        startText = startText.GetComponent<Button>();
        exitText = exitText.GetComponent<Button>();
	}

    public void ExitPress()
    {
        Application.Quit();
    }

    public void StartLevel()
    {
        startMenu.enabled = false;
        cam2.enabled = true;
        cam1.enabled = false;
        //Application.LoadLevel(1);
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
