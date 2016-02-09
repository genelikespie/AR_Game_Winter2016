using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SeekingAI : MonoBehaviour {
    // Main Target
    public Transform mainTargetTransform;
    bool SavePosition;
    bool OverrideTarget;

    // Wandering target
    public Transform wanderTargetTransform;
    GameObject wanderNode;
    // Range of bomber to targe before bomber starts wandering
    public float rangeBeforeWander;
    // Range that bomber wanders
    public float maxRangeToWander;
    //public float minRangeToWander;


    // Turn nodes (center of ellipse for turn radius)
    // -x for left, +x for right
    GameObject leftTurnNode;
    GameObject rightTurnNode;
    float turnRadius;

    public float BaseSpeed = 10;
    public float TurnSpeed = 2.5f;
    public bool isWandering = false;
    public bool IncludeYAxis = false;
    float CurrSpeed;
    float TargetSpeed;
    Vector3 acceleration;
    Vector3 velocity;

    Rigidbody rigidbody;
    List<Vector3> EscapeDirection = new List<Vector3>();

    // Timer before the ai reacquires target
    Timer ResetWanderTargetTimer;
    // Timer for abilities
    Timer AbilityTimer;
    protected void Start()
    {
        CurrSpeed = BaseSpeed;
        TargetSpeed = CurrSpeed;
        rigidbody = GetComponent<Rigidbody>();
        rigidbody.velocity = transform.forward * BaseSpeed;
        velocity = rigidbody.velocity;
        GetComponent<GameObject>();

        // Set the wander node
        wanderNode = HelperMethods.FindChildWithName(gameObject, "WanderNode");
        if (!wanderNode)
            Debug.LogError("Wander node not found!");
        wanderTargetTransform = wanderNode.transform;
        wanderTargetTransform.parent = null;

        leftTurnNode = HelperMethods.FindChildWithName(gameObject, "LeftTurnNode");
        rightTurnNode = HelperMethods.FindChildWithName(gameObject, "RightTurnNode");
        if (!leftTurnNode || !rightTurnNode)
            Debug.LogError("Left or Right turn node not found!");
        turnRadius = Mathf.Pow(BaseSpeed / 7.5f, 2) / TurnSpeed;
        // 160 is the scale of our x axis for our spaceship

        leftTurnNode.transform.localPosition = new Vector3(-turnRadius, 0, 0);
        rightTurnNode.transform.localPosition = new Vector3(turnRadius, 0, 0);
        turnRadius *= transform.lossyScale.x;
        Debug.Log("wander range:" + maxRangeToWander + "  turndiameter: " + turnRadius * 2);
        if (maxRangeToWander < 2 * turnRadius)
            Debug.LogWarning("Range to wander is smaller than turn turnDiameter! Object may not be able to reach target! " + turnRadius * 2);

    }

    public void Initialize(Transform targettransform, float basespeed, float turnspeed, bool includeY)
    {
        mainTargetTransform = targettransform;
        BaseSpeed = basespeed;
        TurnSpeed = turnspeed;
        IncludeYAxis = includeY;
    }

    protected void FixedUpdate()
    {
        if (isWandering)
        {
            //Debug.Log("wander target");
            Debug.DrawLine(transform.position, wanderTargetTransform.position, Color.red);
            if (!IsTargetReachable(wanderTargetTransform))
                Debug.LogWarning("Wander target is not reachable!");
            Wander();
        }
        else if (mainTargetTransform)
        {
            //Debug.Log("main target");
            Debug.DrawLine(transform.position, mainTargetTransform.position, Color.blue);
            if (!IsTargetReachable(mainTargetTransform))
            {
                Debug.LogWarning("Main target is not reachable!");

                // Have our plane wander away
                SetWanderTransform();
                isWandering = true;
            }
            MoveTowardsTarget(mainTargetTransform);
        }
    }

    // The behavior to move straight at the intended target
    void MoveTowardsTarget(Transform targetTransform)
    {
        Vector3 seekVelocity = GetSeekVector(targetTransform.position);
        velocity = rigidbody.velocity;
        velocity += 1.5f * seekVelocity * Time.deltaTime;
        velocity = velocity.normalized * BaseSpeed;

        if (IncludeYAxis)
            rigidbody.velocity = velocity;
        else
            rigidbody.velocity = new Vector3(velocity.x, 0, velocity.z);

        Quaternion desiredRotation = Quaternion.LookRotation(rigidbody.velocity);
        transform.rotation = desiredRotation;
    }

    // Returns a vector from forward velocity to desired velocity
    Vector3 GetSeekVector(Vector3 target)
    {
        Vector3 distance = target - transform.position;
        distance = distance.normalized * TurnSpeed;
        return distance - rigidbody.velocity;
    }

    void Wander()
    {
        MoveTowardsTarget(wanderTargetTransform);
    }

    void Update()
    {
        leftTurnNode.GetComponent<Ellipse>().DrawCircle(turnRadius - rangeBeforeWander, leftTurnNode.transform.position.x, leftTurnNode.transform.position.z, leftTurnNode.transform.position.y);
        rightTurnNode.GetComponent<Ellipse>().DrawCircle(turnRadius - rangeBeforeWander, rightTurnNode.transform.position.x, rightTurnNode.transform.position.z, rightTurnNode.transform.position.y);
    }

    ///// NOTOETOEOEEOEE:::::   X is left and right Z is front and back
    // Only factors in the x and z axis
    // Returns false if target is unreachable
    bool IsTargetReachable(Transform targetTransform)
    {
        Vector3 myPosition = transform.position;
        Vector3 targetPosition = targetTransform.position;
        Vector3 distanceToTarget = myPosition - targetPosition;
        // If the target is sufficiently far, then we don't have to check further
        if (distanceToTarget.magnitude > turnRadius * 2)
            return true;
        Vector3 centerOfLeftEllipse = leftTurnNode.transform.position;
        Vector3 centerOfRightEllipse = rightTurnNode.transform.position;

        // Checks if the target is inside the two ellipse (where our AI cannot reach)
        if (Mathf.Pow((targetPosition.x - centerOfLeftEllipse.x), 2) +
            Mathf.Pow((targetPosition.z - centerOfLeftEllipse.z), 2) <= Mathf.Pow(turnRadius - rangeBeforeWander, 2) ||
            Mathf.Pow((targetPosition.x - centerOfRightEllipse.x), 2) +
            Mathf.Pow((targetPosition.z - centerOfRightEllipse.z), 2) <= Mathf.Pow(turnRadius - rangeBeforeWander, 2))
            return false;
        else
            return true;
    }

    protected virtual void SetWanderTransform()
    {
        float randomPositionAlongCircle = (Random.value * 360);
        float xPos = maxRangeToWander * Mathf.Cos(randomPositionAlongCircle);
        float zPos = maxRangeToWander * Mathf.Sin(randomPositionAlongCircle);
        wanderTargetTransform.position = new Vector3(transform.position.x + xPos, transform.position.y, transform.position.z + zPos);
        Debug.Log("Set wander position to: " + wanderTargetTransform.position);
    }
}

