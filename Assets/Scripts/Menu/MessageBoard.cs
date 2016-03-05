﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;


// Modal panel that displays pop-up messages to the user
public class MessageBoard : MonoBehaviour {

    public Button back;
    public GameObject messageBoard;
    public Text title;
    public Text body;
    public float animationTime;

    public Vector3 cameraLocalPosition;
    public Transform ARCamera;
    private Vector3 startLocalPosition;
    private Transform startParent;

    private Vector2 startSizeDelta;
    private bool open;
    private bool animate;
    private float startAnimationTime;
    private float currAnimationTime;
    private RectTransform rectTransform;

    private GameManagerScript gameManager;
    private static MessageBoard instance;
    private static Object instance_lock = new Object();
    public static MessageBoard Instance()
    {
        if (instance != null)
            return instance;
        lock (instance_lock)
        {
            instance = (MessageBoard)FindObjectOfType(typeof(MessageBoard));
            if (FindObjectsOfType(typeof(MessageBoard)).Length > 1)
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
	void Awake () {
	    back = back.GetComponent<Button>();
        rectTransform = GetComponent<RectTransform>();
        gameManager = GameManagerScript.Instance();
        if (!ARCamera)
            Debug.LogError("Need to attach AR Camera!");
        startParent = GetComponentInParent<Transform>();
        startLocalPosition = transform.localPosition;
        open = false;
        animate = false;
        startSizeDelta = rectTransform.sizeDelta;
    }


    public void ToWorldSpace()
    {
        transform.SetParent(startParent);
        transform.localPosition = startLocalPosition;
        rectTransform.localScale = Vector3.one;
        rectTransform.sizeDelta = startSizeDelta;
        rectTransform.localPosition = Vector3.zero;

    }
    public void ToScreenSpace()
    {
        transform.SetParent(ARCamera.transform);
        transform.localPosition = cameraLocalPosition;
       // transform.rotation = Quaternion.identity;
        //transform.rotation = Quaternion.LookRotation(ARCamera.forward);

    }
    public void pressedBack()
    {
        Time.timeScale = 1;
        animate = true;
        open = false;
        currAnimationTime = 0;
        //messageBoard.transform.position = new Vector3(messageBoard.transform.position.x, 5000, messageBoard.transform.position.z);
    }

    public void activateBoard()
    {
        Time.timeScale = 0;
        animate = true;
        open = true;
        currAnimationTime = 0;
        //messageBoard.transform.position = new Vector3(messageBoard.transform.position.x, 10, messageBoard.transform.position.z);
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
        if (animate)
        {
            currAnimationTime += gameManager.timeDeltaTime;
            float scaleValue = 0.0f;
            float animateValue = 0.0f;
            if (!open)
                animateValue = currAnimationTime;
            else
                animateValue = animationTime - currAnimationTime;

            if (animateValue < animationTime / 2)
            {
                scaleValue = 1 - (Mathf.Pow(animateValue, 2));
            }
            else if (animateValue <= animationTime)
            {
                scaleValue = (Mathf.Pow(animateValue - 1, 2));
            }
            //Debug.Log(scaleValue);
            if (!open && animateValue >= animationTime)
            {
                scaleValue = 0;
                animate = false;
            }
            else if (open && animateValue <= 0.0)
            {
                scaleValue = 1;
                animate = false;
            }
            rectTransform.localScale = new Vector3(scaleValue, scaleValue, scaleValue);
        }
	}
}
