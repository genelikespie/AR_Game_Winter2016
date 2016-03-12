using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MenuScript : MonoBehaviour {

    public Button startText;
    public Button exitText;

    public Camera cam1;

    public Canvas startMenu;

    public AudioSource mainMenu;
    public AudioSource selectionMenu;

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
        //startMenu.enabled = false;
        cam1.GetComponent<Animator>().enabled = true;
        //cam2.enabled = true;
        //cam1.enabled = false;
        mainMenu.Pause();
        selectionMenu.Play();
        //Application.LoadLevel(1);
    }

    public void selectAR()
    {
        Application.LoadLevel(1);
    }

    public void selectVR()
    {
        Application.LoadLevel(2);
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
