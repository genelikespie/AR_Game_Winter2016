using UnityEngine;
using System.Collections;
using System.Collections.Generic;


// This class is DEPRECATED
public class SpaceShipAI : MonoBehaviour {
    public EnumDefaultTarget defaultTarget;
    //
    public Transform targetTransform;
    bool savePosition;
    bool overrideTarget;

    // Wandering target
    public Transform wanderTargetTransform;

    Vector3 acceleration;
    Vector3 velocity;
    public float baseSpeed = 10;
    public float turnSpeed = 2.5f;
    public bool includeYAxis = false;
    float currSpeed;
    float targetSpeed;

    Rigidbody rigidbody;
    List<Vector3> EscapeDirection = new List<Vector3>();

    // Timer before the ai reacquires target
    Timer ResetWanderTargetTimer;
    // Timer for abilities
    Timer AbilityTimer;
	void Start () {
        currSpeed = baseSpeed;
        targetSpeed = currSpeed;
        rigidbody = GetComponent<Rigidbody>();
        rigidbody.velocity = transform.forward * baseSpeed;
        velocity = rigidbody.velocity;

        if (defaultTarget == EnumDefaultTarget.headquarter)
            targetTransform = Headquarter.Instance().transform;
        else if (defaultTarget == EnumDefaultTarget.player)
            Debug.LogError("not implemented default currTarget as player yet");
	}

    void FixedUpdate()
    {
        if (targetTransform)
        {
            MoveTowardsTarget();
        }
    }

    // The behavior to move straight at the intended target
    void MoveTowardsTarget()
    {
        Debug.DrawLine(transform.position, targetTransform.position);
        Vector3 seekVelocity = GetSeekVector(targetTransform.position);
        velocity = rigidbody.velocity;
        velocity += seekVelocity * Time.deltaTime;
        velocity = velocity.normalized * baseSpeed;

        if (includeYAxis)
            rigidbody.velocity = velocity;
        else
            rigidbody.velocity = new Vector3 (velocity.x, 0.0f, velocity.z);

        Quaternion desiredRotation = Quaternion.LookRotation(rigidbody.velocity);
        transform.rotation = desiredRotation;
    }

    // Returns a vector from forward velocity to desired velocity
    Vector3 GetSeekVector(Vector3 target)
    {
        Vector3 distance = targetTransform.position - transform.position;
        distance = distance.normalized * turnSpeed;
        return distance - rigidbody.velocity;
    }

    void Wander()
    {

    }

	void Update () {
	
	}
}
