using UnityEngine;
using System.Collections;

public class Headquarter : MonoBehaviour {

    private static Headquarter headquarter;
    public static Headquarter Instance()
    {
        if (headquarter == null)
        {
            headquarter = (Headquarter) FindObjectOfType(typeof(Headquarter));
            if (FindObjectsOfType(typeof(Headquarter)).Length > 1)
            {
                Debug.LogError("There can only be one headquarter!");
                return headquarter;
            }
            if (headquarter == null)
            {
                Debug.LogError("Could not find a headquarter!");
                return null;
            }
        }
        return headquarter;
    }
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
