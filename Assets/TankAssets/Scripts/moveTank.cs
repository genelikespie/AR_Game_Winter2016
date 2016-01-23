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
    float xScreen;
    float yScreen;
    float velocity;
    float smoothTime;
    Vector3 targetPosition;
    Vector3 spawnerPosition;
    Vector3 levelPosition;

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
        yScreen = Screen.width / 2;
        startTankLocation = Tank.transform;
        Vector3 targetPosition = new Vector3(xScreen, yScreen, 0);
        Spawner = GameObject.Find("spawnTankPad").transform;
        spawnerPosition = Spawner.position;
    }

    //Frame Rate check for tank
    void FixedUpdate()
    {
        /*
        //  targetPosition = mainCamera.ScreenToWorldPoint(new Vector3(100, 100, mainCamera.nearClipPlane));
        Vector3 fixedPosition = new Vector3(mainCamera.transform.position.x, spawnerPosition.y , mainCamera.transform.position.z);
        targetPosition = fixedPosition + mainCamera.transform.rotation * Vector3.forward;
        */


        Vector3 moveA = startTankLocation.position;
        RaycastHit Hit;
        // if (Physics.Raycast(new Ray(mainCamera.transform.position, mainCamera.transform.rotation * Vector3.forward), out Hit))
        Ray ray = mainCamera.ScreenPointToRay(new Vector3(xScreen, yScreen, 0));
         if (Physics.Raycast(ray, out Hit))
        {
            Debug.DrawRay(ray.origin, ray.direction * 10000, Color.yellow);
            levelPosition = new Vector3(Hit.point.x, 0, Hit.point.z);
            Tank.transform.position = Vector3.Slerp(moveA, levelPosition, velocity * Time.deltaTime / 100);
            CrosshairLocation.position = Hit.point;
            Debug.Log(Hit.transform.gameObject);
            Debug.Log(Hit.point);
            Debug.Log(moveA);
        }



    }
        

}
