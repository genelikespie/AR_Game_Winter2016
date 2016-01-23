﻿using UnityEngine;
using System.Collections;

public class basicCrosshair : MonoBehaviour {

    public Texture2D crosshairImage;

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnGUI()
    {
        float xMin = (Screen.width / 2);
        float yMin = (Screen.height / 2);
        GUI.DrawTexture(new Rect(xMin, yMin, crosshairImage.width, crosshairImage.height), crosshairImage);
    }
}
