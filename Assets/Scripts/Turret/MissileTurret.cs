using UnityEngine;
using System.Collections;
using System.Collections.Generic;



public class MissileTurret : BaseTurret {
    // Our current target
    public Transform currTarget;
    // List of targets treated as a queue
    public float currTime;
    public LinkedList<Transform> targets;
    public TargetType targetType;
    public bool lookAtTarget = false;
    public float maxTimeToLive = 10.0f;
    public float explosionRadius = 4.0f;
    public float damage = 10.0f;
    public bool continuousFire = true;
    protected Collider collider;
    public string otherTag = "";
    public Transform game_tilt;
    public Transform game_pivot;
    public Transform aim_tilt;
    public Transform aim_pivot;

    protected void Awake()
    {
        targets = new LinkedList<Transform>();
        if (!continuousFire) FireYes = true;

        switch (targetType)
        {
            case TargetType.Enemy:
                otherTag = "Enemy";
                break;
            case TargetType.Headquarter:
                otherTag = "Headquarter";
                break;
            default:
                break;
        }
    }
    protected void Update()
    {
        currTime = Time.time;
        if (nextFireTime < Time.time && continuousFire)
        {
            FireYes = true;
            if (currTarget)
                FireBullet(currTarget.position - transform.position, Quaternion.LookRotation(currTarget.position - transform.position));
        }

        if (currTarget && lookAtTarget)
        {
            aim_pivot.LookAt(currTarget);
            Vector3 newRot = aim_pivot.eulerAngles;
            newRot.x = newRot.z = 0;
            aim_pivot.rotation = Quaternion.Euler(newRot);

            aim_tilt.LookAt(currTarget);
            Vector3 rot = aim_tilt.localEulerAngles;
            rot.y = rot.z = 0;
            aim_tilt.localRotation = Quaternion.Euler(rot);

            game_pivot.rotation = Quaternion.Lerp(game_pivot.rotation, aim_pivot.rotation, Time.deltaTime * turnSpeed);
            game_tilt.localRotation = Quaternion.Lerp(game_tilt.localRotation, aim_tilt.localRotation, Time.deltaTime * turnSpeed);
        }
    }

    protected virtual void OnTriggerEnter(Collider other)
    {
        
        if (other.tag == otherTag)
        {
            if (!targets.Contains(other.transform))
                targets.AddLast(other.transform);
            currTarget = targets.Last.Value;
            //Debug.Log("Added: " + other.name + " to the linkedlist");
            FireBullet(currTarget.position - transform.position, Quaternion.LookRotation(currTarget.position - transform.position));
        }
    }
    protected virtual void OnTriggerExit(Collider other)
    {
        if (targets.Contains(other.transform))
        {
            targets.Remove(other.transform);
            if (targets.First != null)
                currTarget = targets.First.Value;
            else
                currTarget = null;
        }
    }

    public override void FireBullet(Vector3 direction, Quaternion rotation)
    {
        if (!FireYes)
            return;
        if (currentProjectileIndex <= arraySize)
        {
            if (currTarget && currTarget.gameObject.activeSelf)
            {
                //Debug.LogWarning("FIRED MISSILE AT: " + currTarget.name);
                projectileArray[currentProjectileIndex].GetComponent<MissileProjectile>().Initialize(currTarget, targetType, maxTimeToLive, explosionRadius, damage);
                projectileArray[currentProjectileIndex].gameObject.SetActive(true);
                projectileArray[currentProjectileIndex].position = transform.position;
                projectileArray[currentProjectileIndex].gameObject.GetComponent<ProjectileBaseClass>().Fire(direction, rotation);
                if (continuousFire)
                {
                    FireYes = false;
                    nextFireTime = Time.time + reloadTime;
                }
            }
        }
        currentProjectileIndex++;
        if (currentProjectileIndex >= arraySize)
            currentProjectileIndex = 0;
    }
}
