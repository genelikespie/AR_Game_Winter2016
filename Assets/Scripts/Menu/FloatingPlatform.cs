using UnityEngine;
using UnityEngine.Assertions;
using System.Collections;

public class FloatingPlatform : MonoBehaviour {

    // For getting deltaTime
    private GameManagerScript gameManager;
    // Child target that represents to points we move to during ascent or descent
    Transform DescentTarget;
    Transform AscentTarget;
    Transform ARCameraTransform;

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
	void Awake () {
        // Initialize our references
        DescentTarget = HelperMethods.FindChildWithName(gameObject, "DescentTarget").transform;
        AscentTarget = HelperMethods.FindChildWithName(gameObject, "AscentTarget").transform;
        gameManager = GameManagerScript.Instance();
        ARCameraTransform = GameObject.Find("ARCamera").transform;
        Assert.IsTrue(DescentTarget && AscentTarget && gameManager);

        // Detach the targets so they are in world space
        AscentTarget.SetParent(null);
        DescentTarget.SetParent(null);
	}
	
	void Update () {

        if (isActive)
        {
            Vector3 myPosition = transform.position;
            Vector3 targetPosition;

            if (ascend)
                targetPosition = AscentTarget.position;
            else
                targetPosition = DescentTarget.position;

            // Get a vector pointing to our target
            Vector3 towards = targetPosition - myPosition;
            float distance = towards.magnitude;
            // If we are very close to the target, just move there
            if (distance < minimumDistanceToTarget)
            {
                transform.position = targetPosition;
                isActive = false;
            }
            // Calculate the effective speed to travel
            float effectiveSpeed = Mathf.Min(currentSpeed * gameManager.timeDeltaTime, distance);
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
}
