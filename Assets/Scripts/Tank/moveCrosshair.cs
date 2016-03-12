using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class moveCrosshair : MonoBehaviour {


    //moving the crosshair to tank
    public Vector3 levelPosition;
    public float verticalOffset;
    Ray ray;
    RaycastHit Hit;
    float xScreen;
    float yScreen;
    Camera mainCamera;
    bool moveGranted;
    Transform CrosshairLocation;
    public AudioSource buttonPress;

    private GameManagerScript gameManager;
    float start = 0;
    float expectedTime = 0;
    float counter = 0;

    private static moveCrosshair instance;
    private static Object instance_lock = new Object();
    public static moveCrosshair Instance()
    {
        if (instance != null)
            return instance;
        lock (instance_lock)
        {
            instance = (moveCrosshair)FindObjectOfType(typeof(moveCrosshair));
            if (FindObjectsOfType(typeof(moveCrosshair)).Length > 1)
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
        buttonPress = GameObject.Find("ButtonPress").GetComponent<AudioSource>();
        mainCamera = Camera.main;
        xScreen = Screen.width / 2;
        yScreen = Screen.height / 2;
        gameManager = GameManagerScript.Instance();
        start = Time.time;
        expectedTime = Time.time + 1;
    }
	
	// Update is called once per frame
	void Update () {
       
        //Debug.Log(mainCamera.transform.position.y);
        ray = mainCamera.ScreenPointToRay(new Vector3(xScreen, yScreen, 0));
        if (Physics.Raycast(ray, out Hit))
        {
            //Debug.Log(Hit.collider + " " + Hit.rigidbody);
            Debug.DrawRay(ray.origin, ray.direction * 10000, Color.yellow);
            levelPosition = new Vector3(Hit.point.x, Hit.point.y + verticalOffset, Hit.point.z);
            transform.position = levelPosition;
            moveGranted = true;
            if (Input.GetMouseButtonDown(0))
            {
                VirtualButtonPress();
            }
        }
        else
            moveGranted = false;
    }
    public void VirtualButtonPress () {
        Button button = Hit.collider.gameObject.transform.GetComponentInChildren<Button>();
        if (button != null)
        {
            buttonPress.Play();
            button.onClick.Invoke();
        }
    }


}
