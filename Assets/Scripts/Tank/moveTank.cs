using UnityEngine;
using System.Collections;

public class moveTank : MonoBehaviour {


    //shooting functionality
    public GameObject BT1;
    public GameObject BT2;
    public GameObject BT3;
    private GameObject activeTank;
    private Vector3 dummyV;
    private Quaternion dummyQ;
    public enum TurretChoice { Tur1, Tur2, Tur3};
    public TurretChoice defaultTurret;

    Transform startTankLocation;    

    //moving the crosshair to tank
    Vector3 levelPosition;

    //tank variables
    public float m_Speed;
    public float m_TurnSpeed;
    public int moveDistance;
    private Rigidbody m_Rigidbody;              // Reference used to move the tank.
    Vector3 difference;
    Quaternion neededRotation;
    Transform CrosshairTransform;
    bool moveGranted = false;


    void Awake()
    {
        Debug.Log("MOVECROSSHAIR NOT FOUND");
        CrosshairTransform = moveCrosshair.Instance().transform;
        m_Rigidbody = GetComponent<Rigidbody>();

        if (BT1 == null)
            Debug.Log("First Turret Missing");
        if (BT2 == null)
            Debug.Log("Second Turret Missing");
        if (BT3 == null)
            Debug.Log("Third Turret Missing");

    }

    // Use this for initialization
    void Start()
    {
        if (BT1 != null)
            if (defaultTurret == TurretChoice.Tur1)
                activeTank = BT1;
        else if (BT2 != null)
            if (defaultTurret == TurretChoice.Tur2)
                activeTank = BT2;
       else if (BT3 != null)
            if (defaultTurret == TurretChoice.Tur3)
                activeTank = BT3;

        if (GameObject.Find("BlueCross") == null)
            Debug.LogError("No Crosshair");
        startTankLocation = this.transform;
    }

    //Frame Rate check for tank
    void FixedUpdate()
    {
        //move tank to crosshair
        levelPosition = CrosshairTransform.GetComponent<moveCrosshair>().levelPosition;
        {
            Move(levelPosition);
            Turn(levelPosition);
        }

        //Fire the turret
        if (Input.GetMouseButtonDown(0) && activeTank.GetComponent<BaseTurret>().FireYes == true)
        {
            activeTank.GetComponent<BaseTurret>().FireBullet(dummyV,dummyQ);
        }

    }

    private void Move(Vector3 distance)
    {

        difference = distance - this.transform.position;

        if (difference.magnitude > moveDistance)
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
        if (difference.magnitude > moveDistance)
        {
            //print(difference.magnitude);
            difference.y = 0;
            neededRotation = Quaternion.LookRotation(difference);
            this.transform.rotation = Quaternion.Slerp(this.transform.rotation, neededRotation, Time.deltaTime * m_TurnSpeed);
        }
    }



}
