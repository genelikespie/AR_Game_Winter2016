﻿using UnityEngine;
using UnityEngine.Assertions;
using System.Collections;

public class SpawningUnits : MonoBehaviour {

    // For getting deltaTime
    public bool createGo = false;
    public bool droppingNow = false;

    // height to stop dropping at
    public float dropOffY = 5F;
    public int dropOffSpeed = 2;
    private GameManagerScript gameManager;
    // Child target that represents to points we move to during ascent or descent
    Vector3 DescentTarget;
    Vector3 DescentTargetGround;
    Vector3 AscentTarget;
    float journeyLength;
    Transform ARCameraTransform;
    //Transform TankStartLocation;
    Transform UnderPlatform;
    public GameObject createThisGameObject;
    public GameObject dropThis;
    float startTime;

    // The speed to ascend and descend
    public float acceleration;
    public float currentSpeed;

    // Our margin of error to get to the target
    public bool descendFromBehindARCamera;
    public float minimumDistancebeforeSlowdown = 5f;
    public float minimumDistanceToTarget = 0.1f;
    public bool isActive;
    public bool canMove = true;
    public bool ascend;
    private bool Busy = false;

    private ScaleUnitWrapper scaleUnitWrapper;
    // Use this for initialization
    void Awake()
    {
        // Initialize our references
        UnderPlatform = transform.Find("CreateObjects");
        ARCameraTransform = GameObject.Find("ARCamera").transform;
        scaleUnitWrapper = GetComponent<ScaleUnitWrapper>();
        Assert.IsTrue(scaleUnitWrapper && UnderPlatform && ARCameraTransform);
        //TankStartLocation = GameObject.Find("spawnTankZone").transform;
    }
    
    void Start()
    {
        //DescentTarget = new Vector3(TankStartLocation.position.x, dropOffY, TankStartLocation.position.z);
        //AscentTarget = new Vector3(TankStartLocation.position.x, dropOffY+100F, TankStartLocation.position.z);

    }

    public void DropLocation(Vector3 bottomLocation, GameObject spawnMe)
    {
        if (Busy == false)
        {
            Busy = true;
            createThisGameObject = spawnMe;
            DescentTarget = new Vector3(bottomLocation.x, dropOffY, bottomLocation.z);
            AscentTarget = new Vector3(bottomLocation.x, dropOffY + 100F, bottomLocation.z);
            DescentTargetGround = new Vector3(bottomLocation.x, 0.1F, bottomLocation.z);
            journeyLength = Vector3.Distance(DescentTarget, DescentTargetGround);
            Drop();
        }
    }

    // start dropping after the unit finishes scaling back to 1
    public void FinishedScalingUp()
    {
        if (descendFromBehindARCamera)
        {
            transform.position = new Vector3(13, 125, -1);
        }
        isActive = true;
        ascend = false;
        currentSpeed = 0f;
        startTime = Time.time;
    }
    public void FinishedScalingDown()
    {
        gameObject.SetActive(false);
    }
    void Update()
    {
        if(droppingNow == true)
        {
            float currentDuration = (Time.time - startTime) * dropOffSpeed;
            float journeyFraction = currentDuration / journeyLength;
            dropThis.transform.position = Vector3.Lerp(DescentTarget, DescentTargetGround,journeyFraction);
           // Debug.Log(string.Format("Duration: {0} -- Duration: {1}", currentDuration, journeyLength));
            if (dropThis.transform.position.y < .2f)
            {
                droppingNow = false;
                Lift();
            }
        }

        if (isActive)
        {
            Vector3 myPosition = transform.position;
            Vector3 targetPosition;

            if (ascend)
                targetPosition = AscentTarget;
            else
                targetPosition = DescentTarget;

            // Get a vector pointing to our target
            Vector3 towards = targetPosition - myPosition;
            float distance = towards.magnitude;
            // If we are very close to the target, just move there
            if (distance < minimumDistanceToTarget)
            {
                transform.position = targetPosition;
                if (targetPosition == DescentTarget && isActive == true)
                {
                    createGo = true;
                    SpawnGameObject(createThisGameObject);
                }
                if (targetPosition == AscentTarget && isActive == true)
                {
                    Busy = false;
                }
                isActive = false;
                if (ascend) // if we are ascending, scale the unit to 0 to hide it
                    scaleUnitWrapper.Scale(true);
                return;
            }
            // Calculate the effective speed to travel
            float effectiveSpeed = Mathf.Min(currentSpeed * Time.deltaTime, distance);
            //Debug.Log(towards + " distance: " + distance + " speed: " + effectiveSpeed);
            towards.Normalize();
            transform.position = (myPosition + towards * effectiveSpeed);

            // apply the acceleration
            currentSpeed += acceleration;
        }
    }

    public void Drop()
    {
        if (!canMove)
            return;
        transform.localScale = Vector3.zero;
        transform.position = AscentTarget;
        scaleUnitWrapper.Scale(false);
    }


    public void Lift()
    {
        if (!canMove)
            return;
        isActive = true;
        ascend = true;
        currentSpeed = 0f;
    }

    public void SetCanMove(bool value)
    {
        canMove = value;
    }

    void SpawnGameObject(GameObject spawnThis)
    {
        if (createGo == true)
        {
            dropThis = GameObject.Instantiate(spawnThis, UnderPlatform.transform.position, createThisGameObject.transform.rotation) as GameObject;
            createGo = false;
            droppingNow = true;
        }
    }

}