using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SelectTank : MonoBehaviour {

    public GameObject selector;
    //public int selectionIndex;
    public Camera cam1;
    public Camera cam2;

    void Awake()
    {
        DontDestroyOnLoad(selector);
    }

	// Use this for initialization
	void Start () {
        
    }

    public void selectMissileTank()
    {
       
        Debug.Log("Missile!");
        PlayerPrefs.SetInt("Select", 0);
        cam1.enabled = false;
        cam2.enabled = true;
        cam2.GetComponent<Animator>().enabled = true;
      //  this.GetComponentInParent<Canvas>().GetComponent<Camera>().
        //selectionIndex = 0;
        //Application.LoadLevel(1);
    }

    public void selectCannonTank()
    {
        Debug.Log("Cannon!");
        //selectionIndex = 1;
        PlayerPrefs.SetInt("Select", 1);
        cam1.enabled = false;
        cam2.enabled = true;
        cam2.GetComponent<Animator>().enabled = true;
        //Application.LoadLevel(1);
    }
	
	// Update is called once per frame
	void Update () {
	}
}
