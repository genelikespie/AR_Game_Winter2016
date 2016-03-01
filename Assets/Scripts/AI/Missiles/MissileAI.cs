using UnityEngine;
using System.Collections;

public class MissileAI : SeekingAI {

    public GameObject ExplosionAnimation;
    private Collider missileCollider;

    void Awake()
    {
        if (!ExplosionAnimation)
            Debug.LogError("Cannot find explosion animation");
        missileCollider = GetComponent<Collider>();
        if (!missileCollider)
            Debug.LogError("Cannot find collider!");
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
        this.gameObject.SetActive(false);
    }
    void OnTriggerEnter(Collider c)
    {
        //Debug.Log("Missile hit " + c.name + " !!!");
        if (c.transform == mainTargetTransform)
        {
            if (!this.GetComponent<MissileProjectile>())
                Debug.LogError("no projectile class for missile ai");
            c.gameObject.SendMessage("Hit", this.GetComponent<MissileProjectile>().damage, SendMessageOptions.RequireReceiver);
            Explode();
        }
    }

}
