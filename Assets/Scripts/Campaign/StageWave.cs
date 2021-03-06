﻿using UnityEngine;
using System.Collections;

public class StageWave : MonoBehaviour {

    // List of enemies to spawn
    public GameObject[] enemyObjects;
    public int [] numberToSpawn;
    public float delayBeforeCurrStage;
    public float delayBeforeNextStage;

    private Transform[][] enemyTransforms;
    public int enemiesLeft;
    private int totalEnemies;
    private CampaignStage parentStage;
    public AISpawner aiSpawner;

    // all enemies must be defeated before next wave spawns
    public bool mustFinishWaveBeforeNext = true;
    void Start()
    {
        if (enemyObjects.Length < 0)
            Debug.LogError("enemyObjects array is empty! StageWave " + name);
        if (numberToSpawn.Length != enemyObjects.Length)
            Debug.LogError("Size of NTS list != num of enemy objects! StageWave " + name);

        // Create the Array to store all our pre-instantiated enemies
        enemyTransforms = new Transform[enemyObjects.Length][];
        for (int i = 0; i < enemyObjects.Length; i++)
        {
            enemyTransforms[i] = new Transform[numberToSpawn[i]];
            totalEnemies += numberToSpawn[i];

        }
        //Debug.Log("enemyTransforms (dims: " + enemyTransforms.Rank + ") (length: " + enemyTransforms.GetLength(0) + ")");

        // Instantiate all the enemies
        int numOfDiffEnemies = enemyObjects.Length;
        //Debug.Log("numOfDiffEnemies: " + numOfDiffEnemies + " StageWave: " + name);
        // For each different type of enemy
        for (int i = 0; i < numOfDiffEnemies; i++)
        {
            int numOfEnemyToSpawn = numberToSpawn[i];
            //Debug.Log("numToSpawn: " + numOfEnemyToSpawn + " enemy: " + enemyObjects[i].name + " StageWave: " + name);
            // Spawn the number of enemies
            for (int j = 0; j < numOfEnemyToSpawn; j++)
            {
                //Debug.Log( " " + i + " " + j + " "+ enemyTransforms[0][0]);
                enemyTransforms[i][j] = (Instantiate(enemyObjects[i], transform.position, transform.rotation) as GameObject).transform;
                enemyTransforms[i][j].GetComponent<SpaceShip>().SetOnDeathNotify(this);
                enemyTransforms[i][j].GetComponent<SpaceShip>().SetInactive();
            }
        }
        Debug.Log("ENEMYTRANSFORMS TypesOfEnemies:" + enemyTransforms.GetLength(0) + " length of [0]:" + enemyTransforms[0].Length);

    }
    public void Initialize (CampaignStage parent) {
        parentStage = parent;
    }
    public void NotifySpaceShipDied(SpaceShip ship)
    {
        enemiesLeft--;
        if (enemiesLeft < 0)
            Debug.LogError("enemies left is negative!");
        if (enemiesLeft == 0)
        {
            if (!mustFinishWaveBeforeNext)
                parentStage.NotifyWaveCompleted(this);
            else
                StartCoroutine(FinishCurrWave());
            return;
        }
    }
	// Update is called once per frame
	void Update () {
	
	}

    public IEnumerator BeginCurrWave()
    {
        if (!aiSpawner)
            Debug.LogError("No AISpawner referenced: StageWave " + name);
        yield return new WaitForSeconds(delayBeforeCurrStage);
        aiSpawner.RelocateSpawn(enemyTransforms);
        enemiesLeft = totalEnemies;

        Debug.Log("BEGINNING WAVE: " + name + " Enemies: " + totalEnemies);
        if (!mustFinishWaveBeforeNext)
            StartCoroutine(FinishCurrWave());
    }
    public IEnumerator FinishCurrWave()
    {
        yield return new WaitForSeconds(delayBeforeNextStage);
        if (!mustFinishWaveBeforeNext)
            parentStage.LoadNextWave(this);
        else
        {
            parentStage.NotifyWaveCompleted(this);
            parentStage.LoadNextWave(this);
        }

    }
}
