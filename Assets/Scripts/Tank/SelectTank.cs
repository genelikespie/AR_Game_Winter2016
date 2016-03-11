using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SelectTank : MonoBehaviour {

    public Button Tank1;
    //public Canvas StartMenu;

	// Use this for initialization
	void Start () {
        Tank1 = Tank1.GetComponent<Button>();
    }

    public void selectTank()
    {
        Debug.Log("selected!");
    }
	
	// Update is called once per frame
	void Update () {
	}
}
