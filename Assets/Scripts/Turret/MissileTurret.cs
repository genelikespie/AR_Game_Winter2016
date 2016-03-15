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
    public bool AmIATank = false;
    public float maxTimeToLive = 10.0f;
    public float explosionRadius = 4.0f;
    public float damage = 10.0f;
    public int maxBarrageAmount = 10;
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

        if (currTarget && lookAtTarget && AmIATank)
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
            lookAtTarget = true;
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
            lookAtTarget = false;
            targets.Remove(other.transform);
            if (targets.First != null)
                currTarget = targets.First.Value;
            else
                currTarget = null;
        }
    }
    public virtual void Barrage()
    {
        int amountShot = maxBarrageAmount;
        while (amountShot > 0)
        {
            //Debug.Log(amountShot + "while ammo left");
            if (targets.Count <= 0)
            {
                //Debug.Log("Not enough targets for barrage");
                break;
            }
            foreach (Transform t in targets)
            {
                // if we still have ammo, shoot at our next target
                if (amountShot > 0)
                {
                    //Debug.Log(amountShot + "foreach ammo left");
                    if (t && t.gameObject.activeSelf)
                    {
                        //Debug.Log("fired barrage at: " + t.name + " " + amountShot + " left");
                        projectileArray[currentProjectileIndex].GetComponent<MissileProjectile>().Initialize(t, targetType, maxTimeToLive, explosionRadius, damage);
                        projectileArray[currentProjectileIndex].gameObject.SetActive(true);
                        projectileArray[currentProjectileIndex].position = transform.position;
                        projectileArray[currentProjectileIndex].gameObject.GetComponent<ProjectileBaseClass>().Fire(currTarget.position - transform.position, Quaternion.LookRotation(currTarget.position - transform.position));
                        currentProjectileIndex++;
                        amountShot--;
                        if (currentProjectileIndex >= arraySize)
                            currentProjectileIndex = 0;
                    }
                }
                else
                {
                    //Debug.Log("amount of ammo done (foreach)");
                    break; // we used all our ammo
                }
            }
        }
        //Debug.Log("amount of ammo done (while)");
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
            else if (currTarget && !currTarget.gameObject.activeSelf) {
                // remove it from our linked list and set currTarget to next tail
                targets.Remove(currTarget);
                if (targets.Last != null)
                    currTarget = targets.Last.Value;
                else
                    currTarget = null;
            }
        }
        currentProjectileIndex++;
        if (currentProjectileIndex >= arraySize)
            currentProjectileIndex = 0;
    }
}
