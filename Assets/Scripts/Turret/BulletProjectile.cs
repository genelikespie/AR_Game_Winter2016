using UnityEngine;
using System.Collections;

public class BulletProjectile : ProjectileBaseClass{

    public override void Fire(Vector3 direction, Quaternion rotation)
    {
        Debug.Log("WOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOO");
            base.Fire(direction,rotation);     
    }
}
