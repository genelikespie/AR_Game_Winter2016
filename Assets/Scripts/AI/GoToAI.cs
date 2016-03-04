using UnityEngine;
using System.Collections;

public enum GoToTarget
{
    AU_MissileTracker, AU_StasisTracker
}
public class GoToAI : SeekingAI {
    public float minDisToTarget;
    public bool canMoveWithoutTarget;
    protected override void MoveTowardsTarget(Transform targetTransform)
    {
        if (!targetTransform && !canMoveWithoutTarget)
        {
            rigidbody.velocity = Vector3.zero;
            return;
        }
        Vector3 seekVelocity = GetSeekVector(targetTransform.position);
        velocity = rigidbody.velocity;
        velocity += 1.5f * seekVelocity * Time.deltaTime;
        velocity = velocity.normalized;

        if (IncludeYAxis)
            velocity *= BaseSpeed;
        else
            velocity = new Vector3(velocity.x, 0, velocity.z) * BaseSpeed;

        float distanceToTarget;
        Vector3 vectorToTarget = (targetTransform.position - transform.position);
        if (!IncludeYAxis)
            vectorToTarget = new Vector3(vectorToTarget.x, 0, vectorToTarget.z);
        distanceToTarget = vectorToTarget.magnitude;
        float realSpeed = Mathf.Min(velocity.magnitude, distanceToTarget);
        if (distanceToTarget <= minDisToTarget)
            rigidbody.velocity = Vector3.zero;
        else
        {
            rigidbody.velocity = velocity * realSpeed;
            Quaternion desiredRotation = Quaternion.LookRotation(rigidbody.velocity);
            transform.rotation = desiredRotation;
        }
    }
}
