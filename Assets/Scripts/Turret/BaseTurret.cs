using UnityEngine;
using System.Collections;

public class BaseTurret : MonoBehaviour {

    //Creating projectile objects
    public Transform myProjectile;
    public int arraySize = 100;
    public Transform[] projectileArray;


    //Tank Features
    public float reloadTime = .25f;
    public bool FireYes = true;
    public Vector3 dummyV;
    public Quaternion dummyQ;
    private GameObject BulletH;



    int currentProjectileIndex = 0; // number to keep track of the next projectile to fire in our array


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
	
	// Update is called once per frame
	void Update () {
	
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
        print("Hello");
        // Generic fire function (can also be abstract to require derived classes to implement this)              
    }

}

