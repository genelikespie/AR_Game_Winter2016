using UnityEngine;

using System.Collections;

public class SpawningUnits : MonoBehaviour {

    // For getting deltaTime
    public bool createGo = false;
    public bool droppingNow = true;
    public float dropOffY = 5F;
    public float dropOffSpeed = .001F;
    private GameManagerScript gameManager;
    // Child target that represents to points we move to during ascent or descent
    Vector3 DescentTarget;
    Vector3 DescentTargetGround;
    Vector3 AscentTarget;
    float journeyLength;
    Transform ARCameraTransform;
    Transform TankStartLocation;
    Transform UnderPlatform;
    public GameObject createThisGameObject;
    Transform dropThis;
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

    // Use this for initialization
    void Awake()
    {
        // Initialize our references
        // DescentTarget = HelperMethods.FindChildWithName(gameObject, "DescentTarget").transform; //-5
        //AscentTarget = HelperMethods.FindChildWithName(gameObject, "AscentTarget").transform; //-100
        ARCameraTransform = GameObject.Find("ARCamera").transform;
        TankStartLocation = GameObject.Find("spawnTankZone").transform;
        UnderPlatform = GameObject.Find("CreateObjects").transform;
        // Detach the targets so they are in world space
    }
    
    void Start()
    {
        DescentTarget = new Vector3(TankStartLocation.position.x, dropOffY, TankStartLocation.position.z);
        AscentTarget = new Vector3(TankStartLocation.position.x, dropOffY+100F, TankStartLocation.position.z);

    }

    public void DropLocation(Vector3 bottomLocation)
    {
        DescentTarget = new Vector3(bottomLocation.x, dropOffY ,bottomLocation.z);
        AscentTarget = new Vector3(bottomLocation.x, dropOffY+100F, bottomLocation.z);
        DescentTargetGround = new Vector3(bottomLocation.x, 1F, bottomLocation.z);
        journeyLength = Vector3.Distance(DescentTarget, DescentTargetGround);
        startTime = Time.time;
        Drop();
    }

    void Update()
    {
        print(droppingNow + "....." + createGo);
        if(droppingNow == true)
        {
          //  print("phase 2 go!");
            float distCovered = (Time.time - startTime) * dropOffSpeed;
            float Journey = distCovered / journeyLength;
            dropThis.gameObject.transform.position = Vector3.Lerp(DescentTarget, DescentTargetGround, Journey);

            if (dropThis.position.y < .2f)
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
            print(myPosition);
            print(targetPosition);
            float distance = towards.magnitude;
            print(distance);
            // If we are very close to the target, just move there
            if (distance < minimumDistanceToTarget)
            {
                print("BREAKTHRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRR");
                transform.position = targetPosition;
                if (targetPosition == DescentTarget && isActive == true)
                {
                    createGo = true;
                    SpawnGameObject(createThisGameObject);

                }
                isActive = false;
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
        if (descendFromBehindARCamera)
        {
            transform.position = new Vector3(13, 125, -1);
            /*transform.position = new Vector3(ARCameraTransform.position.x,
                ARCameraTransform.position.y + 10f,
                ARCameraTransform.position.z);*/
        }
        transform.position = AscentTarget;
        isActive = true;
        ascend = false;
        currentSpeed = 0f;
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
            dropThis = GameObject.Instantiate(spawnThis, UnderPlatform.transform.position, createThisGameObject.transform.rotation) as Transform;
            createGo = false;
            droppingNow = true;
        }
    }

}