using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Headquarter : MonoBehaviour {

    private static Headquarter instance;
    private static Object instance_lock = new Object();
    private string name;
    public static Headquarter Instance()
    {
        if (instance != null)
            return instance;
        lock (instance_lock)
        {
            instance = (Headquarter)FindObjectOfType(typeof(Headquarter));
            if (FindObjectsOfType(typeof(Headquarter)).Length > 1)
            {
                Debug.LogError("There can only be one instance!");
                return instance;
            }
            if (instance != null)
                return instance;
            Debug.LogError("Could not find a instance!");
            return null;
        }
    }
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
