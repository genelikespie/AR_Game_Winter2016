using UnityEngine;
using System.Collections;

public class moveTank : MonoBehaviour {

    //shooting functionality
    public BaseTurret BT1;
    public BaseTurret BT2;
    public BaseTurret BT3;
    public BaseTurret activeTurret;
    private Vector3 dummyV;
    private Quaternion dummyQ;
    public enum TurretChoice { Tur1, Tur2, Tur3};
    public TurretChoice defaultTurret;

    Transform startTankLocation;    

    //moving the crosshair to tank
    Vector3 levelPosition;

    //tank variables
    public float Speed;
    private float m_Speed;
    public float m_TurnSpeed;
    public float moveDistance;
    private Rigidbody m_Rigidbody;              // Reference used to move the tank.
    Vector3 difference;
    Quaternion neededRotation;
    Transform CrosshairTransform;
    bool moveGranted = false;
    public Vector3 moveIt;

    private static moveTank instance;
    private static Object instance_lock = new Object();
    public static moveTank Instance()
    {
        if (instance != null)
            return instance;
        lock (instance_lock)
        {
            instance = (moveTank)FindObjectOfType(typeof(moveTank));
            if (FindObjectsOfType(typeof(moveTank)).Length > 1)
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

    void Awake()
    {
        CrosshairTransform = moveCrosshair.Instance().transform;
        if (!CrosshairTransform) 
            Debug.Log("MOVECROSSHAIR NOT FOUND");
        m_Rigidbody = GetComponent<Rigidbody>();

        if (BT1 == null)
            Debug.Log("First Turret Missing");
        if (BT2 == null)
            Debug.Log("Second Turret Missing");
        if (BT3 == null)
            Debug.Log("Third Turret Missing");
        m_Speed = Speed;
        if (BT1 != null)
        {
            if (defaultTurret == TurretChoice.Tur1)
                activeTurret = BT1;

        }
        if (BT2 != null)
        {
            if (defaultTurret == TurretChoice.Tur2)
                activeTurret = BT2;

        }
        if (BT3 != null)
        {
            if (defaultTurret == TurretChoice.Tur3)
                activeTurret = BT3;
        }

        if (GameObject.Find("BlueCross") == null)
            Debug.LogError("No Crosshair");
        startTankLocation = this.transform;
    }

    //Frame Rate check for tank
    void FixedUpdate()
    {
        if (BT1 != null)
        {
            if (defaultTurret == TurretChoice.Tur1)
                activeTurret = BT1;

        }
        if (BT2 != null)
        {
            if (defaultTurret == TurretChoice.Tur2)
                activeTurret = BT2;

        }
        if (BT3 != null)
        {
            if (defaultTurret == TurretChoice.Tur3)
                activeTurret = BT3;
        }
        //move tank to crosshair
        levelPosition = CrosshairTransform.GetComponent<moveCrosshair>().levelPosition;
        {
            Move(levelPosition);
            Turn(levelPosition);
        }
        
        //Fire the turret
        if (Input.GetMouseButtonDown(0))
        {
            FireTurret();
        }
        
    }

    private void Move(Vector3 distance)
    {

        difference = distance - this.transform.position;

        if (difference.magnitude > moveDistance)
        {
            // Create a vector in the direction the tank is facing with a magnitude based on the input, speed and the time between frames.
            
            m_Speed = Mathf.Min(difference.magnitude, Speed);
            Vector3 movement = transform.forward * m_Speed * Time.deltaTime;

            // Apply this movement to the rigidbody's position.
            m_Rigidbody.MovePosition(this.transform.position + movement);
            //  m_Rigidbody.velocity = movement;


        }

    }

    public void FireTurret()
    {
        if (activeTurret.FireYes == true)
        {
            activeTurret.FireBullet(dummyV, dummyQ);
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
