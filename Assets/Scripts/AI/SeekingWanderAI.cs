using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public enum EnumDefaultTarget
{
    player, headquarter, testship
}
public class SeekingWanderAI : SeekingAI {
    // Wandering target
    public Transform wanderTargetTransform;
    GameObject wanderNode;
    // Range of bomber to targe before bomber starts wandering
    public float rangeBeforeWander;
    // Range that bomber wanders
    public float maxRangeToWander;
    //public float minRangeToWander;

    public bool DEBUG_LINE_RENDERER = false;

    // Turn nodes (center of ellipse for turn radius)
    // -x for left, +x for right
    GameObject leftTurnNode;
    GameObject rightTurnNode;
    float turnRadius;

    protected void Start()
    {
        base.Start();

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

    void Wander()
    {
        MoveTowardsTarget(wanderTargetTransform);
    }

    void Update()
    {
        if (DEBUG_LINE_RENDERER)
        {
            leftTurnNode.GetComponent<Ellipse>().DrawCircle(turnRadius - rangeBeforeWander, leftTurnNode.transform.position.x, leftTurnNode.transform.position.z, leftTurnNode.transform.position.y);
            rightTurnNode.GetComponent<Ellipse>().DrawCircle(turnRadius - rangeBeforeWander, rightTurnNode.transform.position.x, rightTurnNode.transform.position.z, rightTurnNode.transform.position.y);
        }
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
        //Debug.Log("Set wander position to: " + wanderTargetTransform.position);
    }
}

