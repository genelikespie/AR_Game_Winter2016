using UnityEngine;
using System.Collections;

    public class ProjectileBaseClass : MonoBehaviour
    {

        public float speed = 50;
        public float range = 50;
        public float damage = 1;
        private float currentDistance = 0;

        public virtual void Fire(Vector3 direction, Quaternion rotation)
        {
            // Generic fire function (can also be abstract to require derived classes to implement this)   
            this.transform.position = direction;
            this.transform.rotation = rotation;
            Debug.Log("WE ARE AT THE FIRE YOOOOOOOOOOOO!!!");

        }

        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            transform.Translate(Vector3.forward * Time.deltaTime * speed);
            currentDistance += Time.deltaTime * speed;

            if (currentDistance >= range)
            {
                if (transform.parent)
                {
                    //   Destroy(transform.parent.gameObject);
                }
                //  Destroy(gameObject);

            }
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


