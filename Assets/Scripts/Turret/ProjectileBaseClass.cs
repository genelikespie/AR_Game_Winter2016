using UnityEngine;
using System.Collections;

    public class ProjectileBaseClass : MonoBehaviour
    {

        public float speed = 50;
        public float range = 50;
        public float damage = 1;
        protected float currentDistance = 0;
        protected bool amIFired = false;
        protected GameObject BulletHolder;
        protected Vector3 Stop = new Vector3(0, 0, 0);

        public virtual void Fire(Vector3 direction, Quaternion rotation)
        {

        if (this.GetComponent<Collider>().enabled == false)
            this.GetComponent<Collider>().enabled = true;

        this.transform.position = direction;
        this.transform.rotation = rotation;


        }

        void Awake()
    {
        if (this.GetComponent<Collider>() == null)
            Debug.LogError("Projectile needs Sphere Collider");

        if (GameObject.Find("BulletHolder") == null)
            Debug.Log("NEED BULLETHOLDER");
        else
            BulletHolder = GameObject.Find("BulletHolder");
    }

        void OnTriggerEnter(Collider c)
        {
            Debug.Log("Hit " + c.name + " !!!");
            if (c.gameObject.tag == "Enemy")
            {
                //  Destroy(gameObject);
                c.gameObject.SendMessage("Hit", damage, SendMessageOptions.DontRequireReceiver);
            }

        }

    }


