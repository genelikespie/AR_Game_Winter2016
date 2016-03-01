using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class BackButton : MonoBehaviour {

    public Button back;
    public GameObject messageBoard;
    public Text title;
    public Text body;

	// Use this for initialization
	void Start () {
	    back = back.GetComponent<Button>();
    }

    public void pressedBack()
    {
        Time.timeScale = 1;
        messageBoard.transform.position = new Vector3(messageBoard.transform.position.x, 5000, messageBoard.transform.position.z);
    }

    public void activateBoard()
    {
        Time.timeScale = 0;
        messageBoard.transform.position = new Vector3(messageBoard.transform.position.x, 10, messageBoard.transform.position.z);
    }

    public void setTitle(string t)
    {
        title.text = t;
    }

    public void setBody(string t)
    {
        body.text = t;
    }
	
	// Update is called once per frame
	void Update () {
        Debug.Log(messageBoard.transform.position.y);
	}
}
