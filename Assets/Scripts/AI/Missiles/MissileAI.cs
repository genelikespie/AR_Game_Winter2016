using UnityEngine;
using UnityEngine.Assertions;
using System.Collections;

public class MissileAI : SeekingAI {

    public GameObject ExplosionAnimation;
    private ParticleSystem particles;
    private Collider missileCollider;

    void Awake()
    {
        particles = GetComponentInChildren<ParticleSystem>();
        missileCollider = GetComponent<Collider>();
        Assert.IsTrue(particles && missileCollider && ExplosionAnimation);
    }

    void Explode()
    {
        //Debug.Log(this.name + " Blew up!");
        // Play animation
        Assert.IsNotNull<GameObject>(ExplosionAnimation);
            GameObject explosion = Instantiate(ExplosionAnimation, transform.position, transform.rotation) as GameObject;
            explosion.GetComponentInChildren<SpriteAnimator>().play = true;
            //explosion.GetComponentInChildren<Animator>().Play();
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
