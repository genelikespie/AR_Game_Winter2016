using UnityEngine;
using System.Collections;

public class BulletProjectile : ProjectileBaseClass{

    private Vector3 BulletSpeed = new Vector3(0, 10, 0);
    private float holderSpeed;


    public override void Fire(Vector3 direction, Quaternion rotation)
    {
        currentDistance = 0;
            base.Fire(direction,rotation);
                    BulletProjectileUp();
          amIFired = true;
    }


    new void Start()
    {
        holderSpeed = speed;
    }

    new void Update()
    {
        BulletSpeed = new Vector3(0, holderSpeed, 0);
        currentDistance += Time.deltaTime * speed;
        if (currentDistance >= range && amIFired == true)
        {

            /*if (transform.parent)
                {  }*/
            if (this.GetComponent<Collider>().enabled == true)
                this.GetComponent<Collider>().enabled = false;
            BulletProjectileStop();
            transform.position = BulletHolder.transform.position;
            this.gameObject.SetActive(false);
            amIFired = false;

        }

    }

    void BulletProjectileUp()
    {
        this.transform.GetComponent<Rigidbody>().velocity = BulletSpeed;
    }

    void BulletProjectileStop()
    {
        this.transform.GetComponent<Rigidbody>().velocity = Stop;
    }


}
