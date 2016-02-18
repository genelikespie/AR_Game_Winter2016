using UnityEngine;
using System.Collections;

public class moveCrosshair : MonoBehaviour {


    //moving the crosshair to tank
    public Vector3 levelPosition;
    Ray ray;
    float xScreen;
    float yScreen;
    Camera mainCamera;
    bool moveGranted;
    Transform CrosshairLocation;
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
    void Start () {
        mainCamera = Camera.main;
        xScreen = Screen.width / 2;
        yScreen = Screen.height / 2;
    }
	
	// Update is called once per frame
	void Update () {
        //Debug.Log(mainCamera.transform.position.y);
        ray = mainCamera.ScreenPointToRay(new Vector3(xScreen, yScreen, 0));
        RaycastHit Hit;

        if (Physics.Raycast(ray, out Hit))
        {

            Debug.DrawRay(ray.origin, ray.direction * 10000, Color.yellow);
            levelPosition = new Vector3(Hit.point.x, 0, Hit.point.z);
            transform.position = Hit.point;
            moveGranted = true;
        }
        else
            moveGranted = false;
    }


}
