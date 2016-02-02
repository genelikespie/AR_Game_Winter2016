using UnityEngine;
using System.Collections;

public class MissileAI : SeekingAI {

    public GameObject ExplosionAnimation;
    public float DistanceToExplode = 4f;

    void Start()
    {
        base.Start();
        ExplosionAnimation = GameObject.Find("Big Explosion");
        if (!ExplosionAnimation)
            Debug.LogError("Cannot find explosion animation");
    }

    void FixedUpdate()
    {
        base.FixedUpdate();
        if (TargetTransform)
        {
            Vector3 distanceToTarget = TargetTransform.position - transform.position;
            //Debug.Log(distanceToTarget.y);
            if (distanceToTarget.y < DistanceToExplode)
            {
                Explode();
            }
        }
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

}
