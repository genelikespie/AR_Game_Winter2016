using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class InteractionScript : MonoBehaviour {

    public Button pauseButton;
    public bool paused;

    public GameObject pauseCylinder;
    public GameObject BTMCylinder;

	// Use this for initialization
	void Start () {
        pauseButton = pauseButton.GetComponent<Button>();
        paused = true;
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
            pauseCylinder.transform.position = new Vector3(13, 117, -1);
            BTMCylinder.transform.position = new Vector3(-11, 117, -1);
        }
    }
	
	// Update is called once per frame
	void Update () {
        Debug.Log(BTMCylinder.transform.position.x + " " + BTMCylinder.transform.position.y + " " + BTMCylinder.transform.position.z +
            " " + pauseCylinder.transform.position.x + " " + pauseCylinder.transform.position.y + " " + pauseCylinder.transform.position.z);
        if (paused == true && pauseCylinder.transform.position.y > 3)
        {
            pauseCylinder.transform.Translate(Vector3.back * 5);
            BTMCylinder.transform.Translate(Vector3.back * 5);
        }
	}
}
