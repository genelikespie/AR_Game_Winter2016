using UnityEngine;
using System.Collections;

public class BulletProjectile : ProjectileBaseClass{

    protected Rigidbody Bullet_body;
    private Vector3 BulletSpeed;


    public override void Fire(Vector3 direction, Quaternion rotation)
    {
        currentDistance = 0;
            base.Fire(direction,rotation);
                    BulletProjectileUp();
                           amIFired = true;
    }


    new void Start()
    {
        if (this.gameObject.GetComponent<Rigidbody>() == null)
            Debug.LogError("Projectile Bullet needs rigidbody");
        Bullet_body = this.transform.GetComponent<Rigidbody>();

        BulletSpeed = new Vector3(0, speed, 0);
    }

    new void Update()
    {
        currentDistance += Time.deltaTime * speed;
        if (currentDistance >= range && amIFired == true)
        {

            /*if (transform.parent)
                {  }*/
            if (this.GetComponent<Collider>().enabled == true)
                this.GetComponent<Collider>().enabled = false;
            BulletProjectileStop();
            transform.position = BulletHolder.transform.position;
            amIFired = false;

        }

    }

    void BulletProjectileUp()
    {
        Bullet_body.velocity = BulletSpeed;
    }

    void BulletProjectileStop()
    {
        Bullet_body.velocity = Stop;
    }


}
