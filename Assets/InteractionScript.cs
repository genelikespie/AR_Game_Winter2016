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
            pauseCylinder.transform.position = new Vector3(584, 1317, -131);
            BTMCylinder.transform.position = new Vector3(-1, 1317, -131);
        }
    }
	
	// Update is called once per frame
	void Update () {
        //Debug.Log(BTMCylinder.transform.position.x + " " + BTMCylinder.transform.position.y + " " + BTMCylinder.transform.position.z);
        if (paused == true && pauseCylinder.transform.position.y > 200)
        {
            pauseCylinder.transform.Translate(Vector3.back * 30);
            BTMCylinder.transform.Translate(Vector3.back * 30);
        }
	}
}
