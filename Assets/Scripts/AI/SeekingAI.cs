using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SeekingAI : MonoBehaviour {
    // Main Target
    public Transform mainTargetTransform;
    protected bool SavePosition;
    protected bool OverrideTarget;

    public float BaseSpeed = 10;
    public float TurnSpeed = 2.5f;
    public bool IncludeYAxis = false;
    protected float CurrSpeed;
    protected float TargetSpeed;
    protected Vector3 acceleration;
    protected Vector3 velocity;

    protected Rigidbody rigidbody;
    protected List<Vector3> EscapeDirection = new List<Vector3>();

    protected void Start()
    {
        CurrSpeed = BaseSpeed;
        TargetSpeed = CurrSpeed;
        rigidbody = GetComponent<Rigidbody>();
        rigidbody.velocity = transform.forward * BaseSpeed;
        velocity = rigidbody.velocity;
    }

    public virtual void Initialize(Transform targettransform, float basespeed, float turnspeed, bool includeY)
    {
        mainTargetTransform = targettransform;
        BaseSpeed = basespeed;
        TurnSpeed = turnspeed;
        IncludeYAxis = includeY;
    }

    protected void FixedUpdate()
    {
        if (mainTargetTransform)
        {
            Debug.DrawLine(transform.position, mainTargetTransform.position, Color.blue);
            MoveTowardsTarget(mainTargetTransform);
        }
    }

    // The behavior to move straight at the intended target
    protected virtual void MoveTowardsTarget(Transform targetTransform)
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
    protected Vector3 GetSeekVector(Vector3 target)
    {
        Vector3 distance = target - transform.position;
        distance = distance.normalized * TurnSpeed;
        return distance - rigidbody.velocity;
    }
}

