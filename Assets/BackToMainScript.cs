using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class BackToMainScript : MonoBehaviour {

    public Button BTMButton;

	// Use this for initialization
	void Start () {
        BTMButton = BTMButton.GetComponent<Button>();
    }

    public void backToMain()
    {
        Application.LoadLevel(2);
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
