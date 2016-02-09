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


    //tank variables
    public float m_Speed;
    public float m_TurnSpeed;
    private Rigidbody m_Rigidbody;              // Reference used to move the tank.
    Vector3 goalVelocity;
    Vector3 velocityTank;
    Vector3 difference;
    Vector3 moveA;
    Quaternion neededRotation;
    Transform CrosshairTransform;
    bool moveGranted = false;

    void Awake()
    {
        Debug.Log("MOVECROSSHAIR NOT FOUND");
        CrosshairTransform = moveCrosshair.Instance().transform;
        m_Rigidbody = GetComponent<Rigidbody>();
    }

    // Use this for initialization
    void Start()
    {
        mainCamera = Camera.main;
        if (GameObject.Find("BlueCross") == null)
            Debug.LogError("No Crosshair");
        CrosshairLocation = GameObject.Find("BlueCross").transform;
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


    void Update()
    {
        /*
        ray = mainCamera.ScreenPointToRay(new Vector3(xScreen, yScreen, 0));
        moveA = startTankLocation.position;
        RaycastHit Hit;

        if (Physics.Raycast(ray, out Hit))
        {

            Debug.DrawRay(ray.origin, ray.direction * 10000, Color.yellow);
            levelPosition = new Vector3(Hit.point.x, 0, Hit.point.z);
            CrosshairLocation.position = Hit.point;
            moveGranted = true;
        }
        else
            moveGranted = false;
            */
        moveA = startTankLocation.position;
    }


    //Frame Rate check for tank
    void FixedUpdate()
    {
        levelPosition = CrosshairTransform.GetComponent<moveCrosshair>().levelPosition;
        {
            Move(levelPosition);
            Turn(levelPosition);
        }

    }

    private void Move(Vector3 distance)
    {

        difference = distance - this.transform.position;

        if (difference.magnitude > 75)
        {
            // Create a vector in the direction the tank is facing with a magnitude based on the input, speed and the time between frames.
            Vector3 movement = transform.forward * m_Speed * Time.deltaTime;

            // Apply this movement to the rigidbody's position.
            m_Rigidbody.MovePosition(m_Rigidbody.position + movement);
        }
    }


    private void Turn(Vector3 turning)
    {

        
        difference = turning - this.transform.position;
        if (difference.magnitude > 75)
        {
            //print(difference.magnitude);
            difference.y = 0;
            neededRotation = Quaternion.LookRotation(difference);
            this.transform.rotation = Quaternion.Slerp(this.transform.rotation, neededRotation, Time.deltaTime * m_TurnSpeed);
        }
    }



}
