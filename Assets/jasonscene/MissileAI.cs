using UnityEngine;
using System.Collections;

public class MissileAI : SeekingAI {

    void FixedUpdate()
    {
        base.FixedUpdate();
        if (targetTransform)
        {
            Vector3 distanceToTarget = targetTransform.position - transform.position;
            if (distanceToTarget.magnitude < 2f)
            {
                Explode();
            }
        }
    }

    void Explode()
    {
        Debug.Log(this.name + " Blew up!");
        Destroy(this);
    }

}
