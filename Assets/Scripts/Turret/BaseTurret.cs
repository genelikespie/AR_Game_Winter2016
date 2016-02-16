using UnityEngine;
using System.Collections;

public class BaseTurret : MonoBehaviour {

    public GameObject myProjectile;
    public int arraySize = 100;
    public GameObject[] projectileArray;


    int currentProjectileIndex = 0; // number to keep track of the next projectile to fire in our array


    // Use this for initialization
    protected void Start () {

        projectileArray = new GameObject[arraySize];
        for (int i = 0; i < arraySize; i++)
        {
            projectileArray[i] = Instantiate(myProjectile) as GameObject;
            projectileArray[i].name = myProjectile.name;
        }

    }
	
	// Update is called once per frame
	void Update () {
	
	}

    
    protected void FireBullet(Vector3 direction, Quaternion rotation)
    {

        if (currentProjectileIndex <= arraySize)
                projectileArray[currentProjectileIndex].GetComponent<ProjectileBaseClass>().Fire(direction,rotation);
        Debug.Log(projectileArray[currentProjectileIndex].GetComponent<ProjectileBaseClass>());
        Debug.Log(projectileArray[currentProjectileIndex]);
        currentProjectileIndex++;
        if (currentProjectileIndex >= arraySize)
            currentProjectileIndex = 0;
    }
    /*
    public virtual void Fire()
    {
        print("Hello");
        // Generic fire function (can also be abstract to require derived classes to implement this)              
    }*/

}

