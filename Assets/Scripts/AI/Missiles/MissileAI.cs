using UnityEngine;
using System.Collections;

public class MissileAI : SeekingAI {

    public GameObject ExplosionAnimation;
    public float explosionRadius = 4f;
    public float maxTimeToLive = 30f;
    public float damage = 0f;
    private Collider missileCollider;

    void Awake()
    {
        if (!ExplosionAnimation)
            Debug.LogError("Cannot find explosion animation");
        missileCollider = GetComponent<Collider>();
        if (!missileCollider)
            Debug.LogError("Cannot find collider!");
    }

    public void Initialize(Transform targettransform, float basespeed, float turnspeed, bool includeY, float maxTTL, float expRadius, float dmg)
    {
        mainTargetTransform = targettransform;
        BaseSpeed = basespeed;
        TurnSpeed = turnspeed;
        IncludeYAxis = includeY;
        maxTimeToLive = maxTTL;
        explosionRadius = expRadius;
        damage = dmg;
    }

    void Explode()
    {
        Debug.Log(this.name + " Blew up!");
        // Play animation
        if (ExplosionAnimation)
        {
            GameObject explosion = Instantiate(ExplosionAnimation, transform.position, transform.rotation) as GameObject;
            explosion.GetComponentInChildren<SpriteAnimator>().play = true;
            //explosion.GetComponentInChildren<Animator>().Play();
        }
        else
            Debug.LogError("There's no explosion prefab referenced!");
        // Kill object
        Destroy(this.gameObject);
    }
    void OnTriggerEnter(Collider c)
    {
        Debug.Log("Missile hit " + c.name + " !!!");
        if (c.transform == mainTargetTransform)
        {
            c.gameObject.SendMessage("Hit", damage, SendMessageOptions.DontRequireReceiver);
            Explode();
        }
    }

}
