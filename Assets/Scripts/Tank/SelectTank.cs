using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SelectTank : MonoBehaviour {

    public GameObject selector;
    public int selectionIndex;

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
        selectionIndex = 0;
        Application.LoadLevel(1);
    }

    public void selectCannonTank()
    {
        Debug.Log("Cannon!");
        selectionIndex = 1;
        Application.LoadLevel(1);
    }
	
	// Update is called once per frame
	void Update () {
	}
}
