using UnityEngine;
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
            Debug.Log("PLAYER BEAT WAVE " + name);
            parentStage.NotifyWaveCompleted(this);
        }
    }
	// Update is called once per frame
	void Update () {
	
	}

    public void BeginCurrWave()
    {
        if (!aiSpawner)
            Debug.LogError("No AISpawner referenced: StageWave " + name);
        aiSpawner.RelocateSpawn(enemyTransforms);
        enemiesLeft = totalEnemies;
    }
}
