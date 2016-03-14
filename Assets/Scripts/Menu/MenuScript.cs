using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MenuScript : MonoBehaviour {

    public Button startText;
    public Button exitText;

    public Camera cam1;
    public Camera cam1Launch;
    public Camera cam2Launch;
    public GameObject SpaceShipLaunch;
    public bool spaceScene = false;

    public Canvas startMenu;

    public AudioSource mainMenu;
    public AudioSource selectionMenu;


    IEnumerator WaitAR(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        Application.LoadLevel(1);
    }

    IEnumerator WaitVR(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        Application.LoadLevel(2);
    }

    // Use this for initialization
    void Start () {
        Time.timeScale = 1.0f;
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
        if (spaceScene == true)
        {
            cam1Launch.enabled = false;
            cam2Launch.enabled = true;
            cam2Launch.GetComponent<Animator>().enabled = true;
            SpaceShipLaunch.GetComponent<Animator>().enabled = true;
            StartCoroutine(WaitAR(6f));
        }
        else
            Application.LoadLevel(1);
    }

    public void selectVR()
    {
        if (spaceScene == true)
        {
            cam1Launch.enabled = false;
            cam2Launch.enabled = true;
            cam2Launch.GetComponent<Animator>().enabled = true;
            SpaceShipLaunch.GetComponent<Animator>().enabled = true;
            StartCoroutine(WaitVR(6f));
        }
        else
            Application.LoadLevel(2);
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
