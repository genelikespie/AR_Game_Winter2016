using UnityEngine;
using UnityEngine.Assertions;
using System.Collections;

public class AISpawner : MonoBehaviour {

    public GameObject AIShip;
    public AISpawnerLocation [] AISpawnerLocations;
    Bounds spawnBounds;
    Vector3 boundSize;
    Vector3 boundExtent;

    float timeBetweenSpawns = 1f;
    float currTime = 0f;

    public int maxSpawns = 15;
    public bool doneSpawning;
    public int currSpawns = 0; // number of ai currently active
    public int numSpawned = 0; // number spawned

	// Use this for initialization
	void Start () {
        spawnBounds = GetComponent<Collider>().bounds;
        boundSize = spawnBounds.size;
        boundExtent = spawnBounds.extents;
        doneSpawning = false;
	}
	
	// Update is called once per frame
	void Update () {
        currTime += Time.deltaTime;
        if (currTime > timeBetweenSpawns && numSpawned < maxSpawns) {
            currTime = 0;
            int i = (int) (Random.value * 3);
            if (!AISpawnerLocations[i])
                Debug.LogError("no ai spawner location!  " + i);
            SpaceShip spawnedSpaceShip = AISpawnerLocations[i].Spawn(AIShip).GetComponent<SpaceShip>();
            currSpawns++;
            numSpawned++;
            if (numSpawned == maxSpawns)
                doneSpawning = true;
            //Spawn();
        }
        
	}

    public bool AIShipDestroyed()
    {
        currSpawns--;
        if (doneSpawning && currSpawns <= 0)
            Debug.Log("Player wins!!!!!");
        return true;
    }
}
