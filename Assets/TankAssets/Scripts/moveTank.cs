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

    void Awake()
    {
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

    //Frame Rate check for tank
    void FixedUpdate()
    {
         ray = mainCamera.ScreenPointToRay(new Vector3(xScreen, yScreen, 0));

        moveA = startTankLocation.position;
        RaycastHit Hit;
        
         if (Physics.Raycast(ray, out Hit))
        {
            
            Debug.DrawRay(ray.origin, ray.direction * 10000, Color.yellow);
            levelPosition = new Vector3(Hit.point.x, 0, Hit.point.z);

            Move(levelPosition);
            Turn(levelPosition);
           // Tank.transform.position = Vector3.Slerp(moveA, levelPosition, velocity * Time.deltaTime / 100);
            CrosshairLocation.position = Hit.point;
        }
      
        //Vector3 chPos = mainCamera.ScreenToViewportPoint(new Vector3(xScreen / 2, yScreen / 2, 0));
        //CrosshairLocation.position = new Vector3(chPos.x, .01f, chPos.y);


    }

    /*
    // Returns a vector from forward velocity to desired velocity
    Vector3 DistanceTarget(Vector3 target)
    {
        Vector3 distance = target - transform.position;
        distance = distance.normalized * m_TurnSpeed;
        return distance - m_Rigidbody.velocity;
    }*/


    private void Move(Vector3 distance)
    {

        difference = distance - this.transform.position;
        if (difference.magnitude > 138)
        {
            // Create a vector in the direction the tank is facing with a magnitude based on the input, speed and the time between frames.
            Vector3 movement = transform.forward * m_Speed * Time.deltaTime;

            // Apply this movement to the rigidbody's position.
            m_Rigidbody.MovePosition(m_Rigidbody.position + movement);
        }
    }


    private void Turn(Vector3 turning)
    {
        /*
        // Determine the number of degrees to be turned based on the input, speed and time between frames.
        float turn =  m_TurnSpeed * Time.deltaTime;

        // Make this into a rotation in the y axis.
     //   Quaternion turnRotation = Quaternion.Euler(0f, turn, 0f);

       // Quaternion desiredRotation = Quaternion.LookRotation(turning);
      //  transform.rotation = desiredRotation;

        // Apply this rotation to the rigidbody's rotation.
        m_Rigidbody.MoveRotation(m_Rigidbody.rotation * desiredRotation);
       

  
        Vector3 targetPostition = new Vector3(levelPosition.x,
                                       this.transform.position.y,
                                       levelPosition.z);
        this.transform.LookAt(targetPostition);
        */

        
        difference = turning - this.transform.position;
        if (difference.magnitude > 142)
        {
            //print(difference.magnitude);
            difference.y = 0;
            neededRotation = Quaternion.LookRotation(difference);
            this.transform.rotation = Quaternion.Slerp(this.transform.rotation, neededRotation, Time.deltaTime * m_TurnSpeed);
        }
    }



}
