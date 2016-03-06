using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class moveCrosshair : MonoBehaviour {


    //moving the crosshair to tank
    public Vector3 levelPosition;
    public float verticalOffset;
    Ray ray;
    float xScreen;
    float yScreen;
    Camera mainCamera;
    bool moveGranted;
    Transform CrosshairLocation;
    private static moveCrosshair instance;
    private static Object instance_lock = new Object();
    private GameManagerScript gameManager;
    float start = 0;
    float expectedTime = 0;
    float counter = 0;

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
        gameManager = GameManagerScript.Instance();
        start = Time.time;
        expectedTime = Time.time + 1;
    }
	
	// Update is called once per frame
	void Update () {
       
        //Debug.Log(mainCamera.transform.position.y);
        ray = mainCamera.ScreenPointToRay(new Vector3(xScreen, yScreen, 0));
        RaycastHit Hit;

        if (Physics.Raycast(ray, out Hit))
        {
            //Debug.Log(Hit.collider + " " + Hit.rigidbody);
            Debug.DrawRay(ray.origin, ray.direction * 10000, Color.yellow);
            levelPosition = new Vector3(Hit.point.x, Hit.point.y + verticalOffset, Hit.point.z);
            transform.position = levelPosition;
            moveGranted = true;
            if (Input.GetMouseButtonDown(0))
            {
                Button button = Hit.collider.gameObject.transform.GetChild(0).GetComponent<Button>();
                //Debug.Log(Hit.collider.gameObject);
                //Debug.Log("!!!");
                //Debug.Log(Hit.collider.gameObject.transform.GetChild(0));
                if (button != null)
                {
                    button.onClick.Invoke();
                }
            }
        }
        else
            moveGranted = false;
        //yield WaitForSeconds(1);
    }


}
