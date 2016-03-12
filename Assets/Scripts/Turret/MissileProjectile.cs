using UnityEngine;
using System.Collections;

public class MissileProjectile : ProjectileBaseClass
{
    public Transform target;
    public float maxTimeToLive = 10.0f;
    public float explosionRadius = 4.0f;
    public float timeToDie;

    void Update()
    {
        if (Time.time > timeToDie)
        {
            GetComponent<MissileAI>().Explode();
            gameObject.SetActive(false);
        }
    }
    public void Initialize (Transform targ, TargetType targType, float maxTTL, float expRadius, float dmg ) {
        target = targ;
        targetType = targType;
        maxTimeToLive = maxTTL;
        explosionRadius = expRadius;
        damage = dmg;
    }
    public override void Fire(Vector3 direction, Quaternion rotation)
    {
        //Debug.Log("fired missile at: " + target.name);
        GetComponent<MissileAI>().Initialize(target, speed, speed, true);
        timeToDie = Time.time + maxTimeToLive;
    }
}
