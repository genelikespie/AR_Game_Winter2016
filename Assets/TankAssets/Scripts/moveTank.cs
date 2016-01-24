using UnityEngine;
using System.Collections;

public class moveTank : MonoBehaviour {

    public GameObject Test;
    Transform Spawner;
    GameObject Tank;
    Transform CrosshairLocation;
    Camera mainCamera;
    Transform startTankLocation;
    Transform endTankLocation;
    float velocity;
    float smoothTime;

    //moving the crosshair to tank
    Vector3 levelPosition;
    Ray ray;
    float xScreen;
    float yScreen;

    // Use this for initialization
    void Start()
    {
        mainCamera = Camera.main;
        if (GameObject.Find("cross") == null)
            Debug.LogError("No Crosshair");
        CrosshairLocation = GameObject.Find("cross").transform;
        Tank = this.gameObject;
        smoothTime = 5F;
        velocity = 100F;
        xScreen = Screen.width / 2;
        yScreen = Screen.height / 2;
        startTankLocation = Tank.transform;
        Vector3 targetPosition = new Vector3(xScreen, yScreen, 0);
        if (GameObject.Find("spawnTankPad") == null)
            Debug.LogError("No spawnTankPad");
        Spawner = GameObject.Find("spawnTankPad").transform;
    }

    //Frame Rate check for tank
    void FixedUpdate()
    {
         ray = mainCamera.ScreenPointToRay(new Vector3(xScreen, yScreen, 0));

        Vector3 moveA = startTankLocation.position;
        RaycastHit Hit;
        
         if (Physics.Raycast(ray, out Hit))
        {
            Debug.DrawRay(ray.origin, ray.direction * 10000, Color.yellow);
            levelPosition = new Vector3(Hit.point.x, 0, Hit.point.z);
            Tank.transform.position = Vector3.Slerp(moveA, levelPosition, velocity * Time.deltaTime / 100);
            CrosshairLocation.position = Hit.point;
            print(Hit.transform.gameObject);
        }



    }
        

}
