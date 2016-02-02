using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SeekingAI : MonoBehaviour {
    // Main Target
    public Transform TargetTransform;
    bool SavePosition;
    bool OverrideTarget;

    // Wandering target
    public Transform wanderTargetTransform;

    public float BaseSpeed = 10;
    public float TurnSpeed = 2.5f;
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
    }

    public void Initialize(Transform targettransform, float basespeed, float turnspeed, bool includeY)
    {
        TargetTransform = targettransform;
        BaseSpeed = basespeed;
        TurnSpeed = turnspeed;
        IncludeYAxis = includeY;
    }

    protected void FixedUpdate()
    {
        if (TargetTransform)
        {
            MoveTowardsTarget();
        }
    }

    // The behavior to move straight at the intended target
    void MoveTowardsTarget()
    {
        Debug.DrawLine(transform.position, TargetTransform.position);
        Vector3 seekVelocity = GetSeekVector(TargetTransform.position);
        velocity = rigidbody.velocity;
        velocity += seekVelocity * Time.deltaTime;
        velocity = velocity.normalized * BaseSpeed;

        if (IncludeYAxis)
            rigidbody.velocity = velocity;
        else
            rigidbody.velocity = new Vector3(velocity.x, rigidbody.velocity.y, velocity.z);

        Quaternion desiredRotation = Quaternion.LookRotation(rigidbody.velocity);
        transform.rotation = desiredRotation;
    }

    // Returns a vector from forward velocity to desired velocity
    Vector3 GetSeekVector(Vector3 target)
    {
        Vector3 distance = TargetTransform.position - transform.position;
        distance = distance.normalized * TurnSpeed;
        return distance - rigidbody.velocity;
    }

    void Wander()
    {

    }

    void Update()
    {

    }
}
