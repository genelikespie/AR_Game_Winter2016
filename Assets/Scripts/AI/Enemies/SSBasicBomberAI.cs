using UnityEngine;
using System.Collections;

public class SSBasicBomberAI : SeekingWanderAI {
    public EnumDefaultTarget defaultTarget;

    Transform myTransform;
    void Start()
    {
        base.Start();
        isWandering = false;
        myTransform = transform;
        if (defaultTarget == EnumDefaultTarget.headquarter)
            mainTargetTransform = Headquarter.Instance().transform;
        else if (defaultTarget == EnumDefaultTarget.player)
            Debug.LogError("not implemented default target as player yet");

        // set bomber to wander on spawn
        SetWanderTransform();
        isWandering = true;
    }

    void FixedUpdate()
    {
        if (!mainTargetTransform)
            Debug.LogError("cannot find main target transform!");
        Vector3 distanceToTarget = myTransform.position - mainTargetTransform.position;
        distanceToTarget = new Vector3 (distanceToTarget.x, 0, distanceToTarget.z);
        //Debug.Log("distance to target: " + distanceToTarget);
        // If bomber is not wandering, and bomber is within range of target, set bomber to wander
        if (!isWandering && distanceToTarget.magnitude < rangeBeforeWander)
        {
            SetWanderTransform();
            isWandering = true;
        }
        else if (isWandering && (myTransform.position - wanderTargetTransform.position).magnitude < 40f)
        {
            isWandering = false;
        }
        base.FixedUpdate();
    }

}
