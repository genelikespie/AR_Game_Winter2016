using UnityEngine;
using UnityEngine.Assertions;
using System.Collections;
using System.Collections.Generic;

public class AISpawner : MonoBehaviour {

    public AISpawnerLocation [] AISpawnerLocations;
    Bounds spawnBounds;
    Vector3 boundSize;
    Vector3 boundExtent;

    /*
     * old code used to autospawn AI
    float timeBetweenSpawns = 1f;
    float currTime = 0f;
    public int maxSpawns = 15;
    public bool doneSpawning;
    public int currSpawns = 0; // number of ai currently active
    public int numSpawned = 0; // number spawned
     * */

	// Use this for initialization
	void Start () {
        spawnBounds = GetComponent<Collider>().bounds;
        boundSize = spawnBounds.size;
        boundExtent = spawnBounds.extents;
        //doneSpawning = false;
	}
	
    /*
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
     * */

    /// <summary>
    /// Called by a StageWave to Instantiate AI
    /// </summary>
    /// <returns> A List of spawned space ships</returns>
    public GameObject[] InstantiateSpawn( GameObject AIShip, int numToSpawn)
    {
        if (numToSpawn < 0)
        {
            Debug.LogError("AI number to spawn < 0!!!");
        }
        GameObject[] spawnedSpaceShips = new GameObject[numToSpawn];

        for (int i = 0; i < numToSpawn; i++)
        {
            int spawnerLocationIndex = (int)(Random.value * 3);
            if (!AISpawnerLocations[spawnerLocationIndex])
                Debug.LogError("no ai spawner location!  " + spawnerLocationIndex);
            spawnedSpaceShips[i] = AISpawnerLocations[spawnerLocationIndex].Spawn(AIShip);
        }
        return spawnedSpaceShips;
    }

    public void RelocateSpawn(Transform[][] AIShips)
    {
        if (AIShips.Length < 0)
            Debug.LogError("AIShips to spawn is too small!");
        // Number of dimensions of the array
        for (int dim = 0; dim < AIShips.Rank; dim++)
        {
            for (int length = 0; length < AIShips.GetLength(dim); length++)
            {
                int spawnerLocationIndex = (int)(Random.value * 3);
                if (!AISpawnerLocations[spawnerLocationIndex])
                    Debug.LogError("no ai spawner location!  " + spawnerLocationIndex);
                Transform ship = AISpawnerLocations[spawnerLocationIndex].Relocate(AIShips[dim][length]);
                if (!ship.GetComponent<SpaceShip>())
                    Debug.LogError("Space ship component not found!");
                ship.GetComponent<SpaceShip>().SetAlive();
                ship.gameObject.SetActive(true);
            }
        }
    }
}
