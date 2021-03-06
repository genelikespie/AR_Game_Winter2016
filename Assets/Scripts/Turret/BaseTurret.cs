﻿using UnityEngine;
using System.Collections;

public class BaseTurret : MonoBehaviour {

    //Creating projectile objects
    public GameObject myProjectile;
    public float turnSpeed = 5f;
    public float firePauseTime = 0.01f;  //turret .25f
    public float errorAmount = 0.001f;
    public int arraySize = 100;
    public Transform[] projectileArray;


    //Tank Features
    public float reloadTime = .25f;
    public float nextFireTime;
    public bool FireYes = true;
    public Vector3 dummyV;
    public Quaternion dummyQ;
    protected GameObject BulletH;



    protected int currentProjectileIndex = 0; // number to keep track of the next projectile to fire in our array


    // Use this for initialization
    protected void Start () {
        //TO DO~~!!!!!!!
        /* instantiate bulletholder */
        if (GameObject.Find("BulletHolder") == null)
            Debug.LogError("NEED BULLETHOLDER FOR BASETURRET");
        else
            BulletH = GameObject.Find("BulletHolder");


        projectileArray = new Transform[arraySize];
        for (int i = 0; i < arraySize; i++)
        {
            projectileArray[i] = (Instantiate(myProjectile, BulletH.transform.position, BulletH.transform.rotation) as GameObject).transform;
            projectileArray[i].name = myProjectile.name;
            projectileArray[i].gameObject.SetActive(false);
        }

    }
	
    public virtual void FireBullet(Vector3 direction, Quaternion rotation)
    {

        if (currentProjectileIndex <= arraySize)
        {
            projectileArray[currentProjectileIndex].gameObject.SetActive(true);
            projectileArray[currentProjectileIndex].gameObject.GetComponent<ProjectileBaseClass>().Fire(direction, rotation);
        }
        currentProjectileIndex++;
        if (currentProjectileIndex >= arraySize)
            currentProjectileIndex = 0;
    }
    
    public virtual void Fire()
    {
        // Generic fire function (can also be abstract to require derived classes to implement this)              
    }

}

