using UnityEngine;
using UnityEngine.Assertions;
using System.Collections;

public class AISpawner : MonoBehaviour {

    public GameObject AIShip;
    Bounds spawnBounds;
    Vector3 boundSize;
    Vector3 boundExtent;

    float timeBetweenSpawns = 1f;
    float currTime = 0f;

    public int maxSpawns = 15;
    public int currSpawns = 0;

	// Use this for initialization
	void Start () {
        spawnBounds = GetComponent<Collider>().bounds;
        boundSize = spawnBounds.size;
        boundExtent = spawnBounds.extents;
	}
	
	// Update is called once per frame
	void Update () {
        currTime += Time.deltaTime;
        if (currTime > timeBetweenSpawns && currSpawns < maxSpawns) {
            currTime = 0;
            Spawn();
        }
        
	}

    void Spawn()
    {
        Vector3 spawnLocation = spawnBounds.center;
        Quaternion spawnRotation = Quaternion.LookRotation(spawnLocation.normalized);
        spawnLocation.x = spawnLocation.x + (Random.value * boundSize.x - boundExtent.x);
        spawnLocation.y = spawnLocation.y + (Random.value * boundSize.y - boundExtent.y);
        spawnLocation.z = spawnLocation.z + (Random.value * boundSize.z - boundExtent.z);
        //Debug.Log(spawnLocation + " center: " + spawnBounds.center);
        Assert.IsNotNull(AIShip);
        Instantiate(AIShip, spawnLocation, spawnRotation);
        currSpawns++;
    }
}
